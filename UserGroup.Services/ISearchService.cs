using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroup.Common.DTO;
using UserGroup.Common.Helper;

namespace UserGroup.Services
{
    public interface ISearchService
    {
        Task<List<SearchResultDto>> Get(SearchResourceParameter resourceParameter);
    }
}