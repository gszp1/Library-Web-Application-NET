﻿namespace Library_Web_Application_NET.Server.src.util
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        
        public int TotalItems { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
