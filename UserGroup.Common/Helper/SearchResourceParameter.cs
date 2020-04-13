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

        public string AscDesc { get; set; }
        public SortOrder SortOrderOption { 
            get 
            {
                if(string.IsNullOrWhiteSpace(AscDesc))
                {
                    return Enums.SortOrder.Asc;
                }
                else
                {
                    if(AscDesc == "Asc")
                    {
                        return SortOrder.Asc;
                    }
                    if (AscDesc == "Desc")
                    {
                        return SortOrder.Desc;
                    }
                    else
                    {
                        return SortOrder.Asc;
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
                    return SortColumn.DateAdded;
                }
                else
                {
                    if (SortBy == "Name")
                    {
                        return SortColumn.DateAdded;

                    }
                    else if (SortBy == "DateAdded")
                    {
                        return SortColumn.DateAdded;
                    }
                    else if (SortBy == "Group")
                    {
                        return SortColumn.Group;
                    }
                    else
                    {
                        return SortColumn.DateAdded;
                    }
                }
            }
        }
    }
}