using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.Paging
{
    public class PagingFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        //public List<T> PagingData { get; set; }

        public PagingFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 5;
        }

        public PagingFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 5 ? 5 : pageSize;
        }
    }
}
