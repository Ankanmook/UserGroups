﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.Common.DTO
{
    public class SearchResultDto
    {
        public int TotalRows { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string GroupName { get; set; }        
    }

    public class SearchRequestDto
    {
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string GroupName { get; set; }

    }
}