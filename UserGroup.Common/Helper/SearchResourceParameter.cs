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
                    return SortOrder.Asc;
                }
                else
                {
                    if(AscDesc == Constants.SortInAscendingOrder)
                    {
                        return SortOrder.Asc;
                    }
                    if (AscDesc == Constants.SortInDescendingOrder)
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
                    if (SortBy == Constants.SortByNameColumn)
                    {
                        return SortColumn.DateAdded;

                    }
                    else if (SortBy == Constants.SortByDateAddedColumn)
                    {
                        return SortColumn.DateAdded;
                    }
                    else if (SortBy == Constants.SortByGroupCoumn)
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