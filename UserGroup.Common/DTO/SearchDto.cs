using System;

namespace UserGroup.Common.DTO
{
    public class SearchResultDto
    {
        public int TotalRows { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }
}