namespace LearningTogether.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaginatedList<T> : List<T>
        where T : class
    {
        public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            this.TotalCount = source.Count();
            this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

            this.PageIndex = pageIndex > this.TotalPages ? this.TotalPages : pageIndex;

            this.AddRange(source.Skip((this.PageIndex - 1) * this.PageSize).Take(this.PageSize));
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }
    }
}