﻿namespace UserGroup.DataModel.Helpers
{
    public class ResourceParameters
    {
        private const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}