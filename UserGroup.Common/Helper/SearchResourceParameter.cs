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

        public string SortOrder { get; set; }
        public SortOrder SortOrderOption { 
            get 
            {
                if(string.IsNullOrWhiteSpace(SortOrder))
                {
                    return Enums.SortOrder.Asc;
                }
                else
                {
                    if(SortOrder == "Asc")
                    {
                        return Enums.SortOrder.Asc;
                    }
                    if (SortOrder == "Desc")
                    {
                        return Enums.SortOrder.Desc;
                    }
                    else
                    {
                        return Enums.SortOrder.Asc;
                    }
                }

            } 
        }

        public string SortBy { get; set; }
        public SortColumn SortColumn
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SortBy))
                {
                    return Enums.SortColumn.DateAdded;
                }
                else
                {
                    if (SortBy == "Name")
                    {
                        return Enums.SortColumn.DateAdded;

                    }
                    else if (SortBy == "DateAdded")
                    {
                        return Enums.SortColumn.DateAdded;
                    }
                    else if (SortBy == "Group")
                    {
                        return Enums.SortColumn.Group;
                    }
                    else
                    {
                        return Enums.SortColumn.DateAdded;
                    }
                }
            }
        }
    }
}