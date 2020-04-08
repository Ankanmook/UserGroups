using System;

namespace UserGroup.DAL.Dapper
{
    public class SprocNameAttribute : Attribute
    {

        public SprocNameAttribute(string sprocName)
        {
            this.SprocName = sprocName;
        }

        public string SprocName { get; set; }

    }
}
