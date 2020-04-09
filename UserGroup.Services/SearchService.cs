using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroup.Common.Contracts;
using UserGroup.Common.DTO;
using UserGroup.Common.Enums;
using UserGroup.Common.Helper;

namespace UserGroup.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<List<SearchResultDto>> GetSearchResult(SearchResourceParameter resourceParameter)
        {
            if (resourceParameter.SearchOption == SearchOption.EF)
            {
                return await _searchRepository.GetSearchResultUsingDapper(resourceParameter.Name, resourceParameter.Group,
                resourceParameter.PageNumber, resourceParameter.PageSize);
            }
            else
            {
                return await _searchRepository.GetSearchResultUsingDapper(resourceParameter.Name, resourceParameter.Group,
                resourceParameter.PageNumber, resourceParameter.PageSize);
            }
        }
    }
}