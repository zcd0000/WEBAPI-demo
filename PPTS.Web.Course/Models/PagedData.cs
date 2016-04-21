using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models
{
    public class PagedData<T>
    {
        public PagedData(int page, int limit, int totalCount)
        {
            this.PagedParam = new PagedParam(page, limit, totalCount);
        }
        public PagedData(PagedParam pagedParam)
        {
            this.PagedParam = pagedParam;
        }
        public PagedParam PagedParam { get; set; }
        public List<T> Data { get; set; }
    }
}
