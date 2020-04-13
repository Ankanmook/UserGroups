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
            SortColumn sortColumn = SortColumn.DateAdded,
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
        /// <param name="groupName"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<List<SearchResultDto>> GetSearchResultUsingEFCore(string name,
        string groupName,
        int pageNumber = 1,
        int pageSize = 100,
        SortColumn sortColumn = SortColumn.DateAdded,
        SortOrder sortOrder = SortOrder.Asc)
        {
            var person = from p in _context.Person.Include(p => p.Group)
                         select p;


            if (!string.IsNullOrWhiteSpace(name))
            {
                name = $"%{name.ToLower()}%";
                person = from p in person
                where EF.Functions.Like(p.Name.ToLower(), name)
                select p;
            }

            if (!string.IsNullOrWhiteSpace(groupName))//can restrict user to send group id only
            {
                groupName = $"%{groupName.ToLower()}%";
                person = from p in person
                where EF.Functions.Like(p.Group.Name.ToLower(), groupName)
                select p;
            }

            if (sortColumn == SortColumn.DateAdded && sortOrder == SortOrder.Asc)
            {
                person.OrderBy(p => p.Name).ThenBy(p => p.Group);
            }

            if (sortColumn == SortColumn.Group && sortOrder == SortOrder.Asc)
            {
                person.OrderBy(p => p.Group).ThenBy(p => p.Name);
            }

            if (sortColumn == SortColumn.DateAdded && sortOrder == SortOrder.Desc)
            {
                person.OrderByDescending(p => p.Group).ThenBy(p => p.Name);
            }

            if (sortColumn == SortColumn.Group && sortOrder == SortOrder.Desc)
            {
                person.OrderByDescending(p => p.Group).ThenBy(p => p.Name);
            }

            
            return await person.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(p => new SearchResultDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    DateAdded = p.DateAdded,
                    GroupId = p.GroupId,
                    GroupName = p.Group.Name
                }).ToListAsync();
        }

        public int Count(string name, string groupName)
        {
            var person = from p in _context.Person.Include(p => p.Group)
                         select p;


            if (!string.IsNullOrWhiteSpace(name))
            {
                name = $"%{name.ToLower()}%";
                person = from p in person
                         where EF.Functions.Like(p.Name.ToLower(), name)
                         select p;
            }

            if (!string.IsNullOrWhiteSpace(groupName))//can restrict user to send group id only
            {
                groupName = $"%{groupName.ToLower()}%";
                person = from p in person
                         where EF.Functions.Like(p.Group.Name.ToLower(), groupName)
                         select p;
            }
            return person.Any() ? person.Count(): 0;
        }
    }
}