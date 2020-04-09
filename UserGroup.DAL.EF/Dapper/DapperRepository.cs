using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UserGroup.DAL.Dapper
{
    public abstract class DapperRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public abstract string ConnectionString { get; }

        public string PersonGroupsDbConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("PersonGroupsDbconnectionString"); ;
            }
        }

        /// <summary>
        /// Repository class to get data from database using Dapper
        /// To be used for performance check purposes when seeded with a lot of data
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logHandler"></param>
        public DapperRepository(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        //needs to be set to true for load testing in DEV
        //can be set false in live
        public bool LogTimes
        {
            get
            {
                var dataAccessLogTime = _configuration.GetSection("DataAccessLogTime").Value ?? string.Empty;
                bool.TryParse(dataAccessLogTime, out bool logTimes);
                return logTimes;
            }
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<T>> GetResultsAsync<T>(string procedureName, object parameters = null, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, int commandTimeout = 30)
        {
            return await WithConnectionAsync<T>(async c =>
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                var t = c.BeginTransaction(isolationLevel);

                IEnumerable<T> results;
                try
                {
                    results = await c.QueryAsync<T>(sql: procedureName,
                    param: parameters,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout,
                transaction: t);
                }
                catch (Exception ex)
                {
                    if (t != null)
                    {
                        t.Rollback();
                    }
                    if (c != null)
                    {
                        c.Close();
                    }
                    throw ex;
                }
                t.Commit();
                timer.Stop();
                if (LogTimes)
                {
                    _logger.LogInformation(this.GetType().Name, $"Time taken to run procedure took {timer.ElapsedMilliseconds}ms : {procedureName} ");
                }
                return results.ToList();
            });
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<T>> WithConnectionAsync<T>(Func<IDbConnection, Task<List<T>>> getData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    var data = await getData(connection); // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>

                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }

                    return data;
                }
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }

            catch (SqlException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSprocName()
        {
            StackTrace stackTrace = new StackTrace();

            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                var sprocAttr = method.GetCustomAttribute<SprocNameAttribute>();
                if (sprocAttr != null)
                {
                    return sprocAttr.SprocName;
                }
            }
            return "";
        }
    }
}