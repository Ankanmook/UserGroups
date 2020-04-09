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

        public async Task<List<SearchResultDto>> Get(SearchResourceParameter resourceParameter)
        {
            if (resourceParameter.Option == SearchOption.Dapper)//default
            {
                return await _searchRepository.GetSearchResultUsingDapper(resourceParameter.Name, resourceParameter.Group,
                resourceParameter.PageNumber, resourceParameter.PageSize);
            }
            else
            {
                //may be bad practise
                return await Task.FromResult(_searchRepository.GetSearchResultUsingEFCore(resourceParameter.Name, resourceParameter.Group,
                resourceParameter.PageNumber, resourceParameter.PageSize));
            }
        }
    }
}