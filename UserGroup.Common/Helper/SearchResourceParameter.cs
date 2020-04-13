using UserGroup.Common.Enums;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Helper
{
    public class SearchResourceParameter : ResourceParameters
    {
        public SearchOption Option { get; set; } = SearchOption.Dapper;//set to default
        public string Name { get; set; }

        private string _group;
        public string Group 
        {
            get
            {
                return _group;
            }
            set
            {
                _group = string.IsNullOrWhiteSpace(value) 
                    || value == Constants.All ? 
                    string.Empty : value;
            }
        }
        public int GroupId { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}