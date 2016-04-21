using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models
{
    public class PagedParam
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int PageCount { get; set; }
        public string Message { get; set; }
        public PagedParam(int page, int limit, int totalCount)
        {
            this.Page = page;
            this.Limit = limit;
            this.TotalCount = totalCount;

            int a = totalCount / limit;
            int b = totalCount % limit;
            if (b == 0) this.PageCount = a;
            else this.PageCount = a + 1;

            this.Message = "共" + totalCount.ToString() + "条数据，当前显示" + ((this.Page - 1) * this.Limit + 1).ToString() + "到" + Math.Min(this.Page * this.Limit, this.TotalCount).ToString() + "条";
        }
        public PagedParam()
        {
            this.Page = 1;
            this.Limit = 10;
        }
    }
}
