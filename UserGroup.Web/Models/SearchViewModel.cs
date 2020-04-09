using System;
using System.Collections.Generic;
using UserGroup.Common.Enums;

namespace UserGroup.Web.Models
{
    public class SearchViewModel
    {
        public int Total { get; set; }
        public IEnumerable<SearchResultViewModel> Results { get; set; }
        public SearchOption SearchOption { get; set; }
        public long ResponseTime { get; set; }
    }

    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }
}