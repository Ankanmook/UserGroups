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
                resourceParameter.PageNumber, resourceParameter.PageSize,resourceParameter.SortColumn, resourceParameter.SortOrderOption);
            }
            else
            {
                //2 calls to the database makes this inefficient
                var result = await _searchRepository.GetSearchResultUsingEFCore(resourceParameter.Name, resourceParameter.Group,
                resourceParameter.PageNumber, resourceParameter.PageSize, resourceParameter.SortColumn, resourceParameter.SortOrderOption);
                
                var count = _searchRepository.Count(resourceParameter.Name, resourceParameter.Group);

                result.ForEach(r => r.TotalRows = count);
                return result;
            }
        }

       

        
    }
}