using UserGroup.Common.Enums;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Helper
{
    public class SearchResourceParameter : ResourceParameters
    {
        public SearchOption Option { get; set; } = SearchOption.Dapper;//set to default
        public string Name { get; set; }
        public string Group { get; set; }
    }
}