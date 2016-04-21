using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models.Schedules
{
    public class ScheduleSimpleSearchCriteria
    {
        //上课状态
        public string Status { get; set; }
        //上课年级
        public string Grade { get; set; }
        //上课教师
        public List<Teacher> Teachers { get; set; }
        public PagedParam PagedParam { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
