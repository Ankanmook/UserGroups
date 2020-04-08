﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;

namespace UserGroup.DAL.Dapper
{
    public class SearchRepository : DapperRepository, ISearchRepository
    {

        public const string SearchPerson_StoreProc = "[dbo].[usp_Search]";

        public SearchRepository(IConfiguration configuration, Action<string, string> logHandler) : base(configuration, logHandler) {}
        
        public override string ConnectionString => PersonGroupsDbConnectionString;


        [SprocName(SearchPerson_StoreProc)]
        public async Task<List<SearchResultDto>> GetSearchResult(string name, 
            string group, 
            int pageNumber = 1, 
            int pageSize = 100, 
            SortColumn sortColumn = SortColumn.Name,
            SortOrder sortOrder = SortOrder.Asc)
        {
            return await GetResultsAsync<SearchResultDto>(GetSprocName(), 
            new
            {
                name, group, pageNumber, pageSize, 
                sortColumn = sortColumn.ToString(), 
                sortOrder = sortOrder.ToString()
            });
        }
    }
}
