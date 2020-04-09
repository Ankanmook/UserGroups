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

namespace UserGroup.DAL.EF
{
    public class SearchRepository : DapperRepository, ISearchRepository
    {
        public const string SearchPerson_StoreProc = "[dbo].[usp_Search]";

        private readonly PersonGroupsContext _context;

        public SearchRepository(IConfiguration configuration, PersonGroupsContext context, ILogger<SearchRepository> logger) 
            : base(configuration, logger) 
        {
            _context = context;
        }

        public override string ConnectionString => PersonGroupsDbConnectionString;

        [SprocName(SearchPerson_StoreProc)]
        public async Task<List<SearchResultDto>> GetSearchResultUsingDapper(string name,
            string group,
            int pageNumber = 1,
            int pageSize = 100,
            SortColumn sortColumn = SortColumn.Name,
            SortOrder sortOrder = SortOrder.Asc)
        {
            return await GetResultsAsync<SearchResultDto>(GetSprocName(),
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