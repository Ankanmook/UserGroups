namespace UserGroup.Common.Enums
{
    public enum SortOrder
    {
        Asc = 0,
        Desc = 1
    }

    public enum SortColumn
    {
        Name,
        Group
    }

    public enum SearchOption
    {
        Dapper = 0, //Default 
        EF = 1,
    }
}
