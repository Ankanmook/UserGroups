using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;

namespace UserGroup.Common.Contracts
{
    public interface ISearchRepository
    {
        Task<List<SearchResultDto>> GetSearchResultUsingDapper(string name, string group, int pageNumber = 1, int pageSize = 20, SearchColumn sortColumn = SearchColumn.Name, SortOrder sortOrder = SortOrder.Asc);

        List<SearchResultDto> GetSearchResultUsingEFCore(string name, string group, int pageNumber = 1, int pageSize = 20, SearchColumn sortColumn = SearchColumn.Name, SortOrder sortOrder = SortOrder.Asc);
    }
}
