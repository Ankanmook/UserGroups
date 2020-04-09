using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;
using UserGroup.DAL.Dapper;

namespace UserGroup.DAL
{
    public class SearchRepository : DapperRepository, ISearchRepository
    {
        
        private readonly PersonGroupsContext _context;

        public SearchRepository(IConfiguration configuration, PersonGroupsContext context, ILogger<SearchRepository> logger) 
            : base(configuration, logger) 
        {
            _context = context;
        }

        public override string ConnectionString => PersonGroupsDbConnectionString;


        public const string SearchPerson_StoreProc = "[dbo].[usp_Search]";

        /// <summary>
        /// Executes the search result using stored procedure usp_search and dapper
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<List<SearchResultDto>> GetSearchResultUsingDapper(string name,
            string group,
            int pageNumber = 1,
            int pageSize = 100,
            SortColumn sortColumn = SortColumn.Name,
            SortOrder sortOrder = SortOrder.Asc)
        {
            return await GetResultsAsync<SearchResultDto>(SearchPerson_StoreProc,
            new
            {
                name,
                group,
                pageNumber,
                pageSize,
                sortColumn = sortColumn.ToString(),
                sortOrder = sortOrder.ToString()
            });
        }


        /// <summary>
        /// Excecutes the search result using EF
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<SearchResultDto> GetSearchResultUsingEFCore(string name,
        string group,
        int pageNumber = 1,
        int pageSize = 100,
        SortColumn sortColumn = SortColumn.Name,
        SortOrder sortOrder = SortOrder.Asc)
        {
            var person = _context.Person.Include(p => p.Group);


            if (!string.IsNullOrWhiteSpace(name))
            {
                person.Where(p => p.Name.Contains(name));
            }

            if (string.IsNullOrWhiteSpace(group))//can restrict user to send group id only
            {
                person.Where(p => p.Group.Name.Contains(group));
            }

            if (sortColumn == SortColumn.Name && sortOrder == SortOrder.Asc)
            {
                person.OrderBy(p => p.Name).ThenBy(p => p.Group);
            }

            if (sortColumn == SortColumn.Group && sortOrder == SortOrder.Asc)
            {
                person.OrderBy(p => p.Group).ThenBy(p => p.Name);
            }

            if (sortColumn == SortColumn.Name && sortOrder == SortOrder.Desc)
            {
                person.OrderByDescending(p => p.Group).ThenBy(p => p.Name);
            }

            if (sortColumn == SortColumn.Group && sortOrder == SortOrder.Desc)
            {
                person.OrderByDescending(p => p.Group).ThenBy(p => p.Name);
            }

            return person.Skip(pageSize * (pageNumber - 1))
                .Select(p => new SearchResultDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    DateAdded = p.DateAdded,
                    GroupId = p.GroupId,
                    GroupName = p.Group.Name
                }).ToList();
        }
    }
}