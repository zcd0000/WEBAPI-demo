using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models.Schedules
{
    public class Schedule
    {
        public Guid id { get; set; }
        //标题
        public string title { get; set; }
        //开始时间
        public DateTime start { get; set; }
        //结束时间
        public DateTime end { get; set; }
        public string startText { get; set; }
        public string endText { get; set; }
        //颜色
        public string color { get; set; }
        public string textColor { get; set; }
        //是否为全天事件
        public bool allDay { get; set; }
        public string status { get; set; }
        public double duration { get; set; }
    }
}
