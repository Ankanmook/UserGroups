﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;

namespace UserGroup.Common.Contracts
{
    public interface ISearchRepository
    {
        Task<List<SearchResultDto>> GetSearchResultUsingDapper(string name, string group, int pageNumber = 1, int pageSize = 20, SortColumn sortColumn = SortColumn.DateAdded, SortOrder sortOrder = SortOrder.Asc);

        Task<List<SearchResultDto>> GetSearchResultUsingEFCore(string name, string group, int pageNumber = 1, int pageSize = 20, SortColumn sortColumn = SortColumn.DateAdded, SortOrder sortOrder = SortOrder.Asc);

        int Count(string name, string groupName);
    }
}