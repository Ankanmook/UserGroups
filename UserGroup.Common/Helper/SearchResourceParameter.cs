using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroup.Common.Enums;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Helper
{
    public class SearchResourceParameter : ResourceParameters
    {
        public SearchOption SearchOption { get; set; } = SearchOption.Dapper;//set to default

        public string Name { get; set; }
        public string Group { get; set; }
    }
}
