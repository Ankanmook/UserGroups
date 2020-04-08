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
        EF = 0,//Default
        Dapper = 1
    }
}
