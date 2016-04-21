using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models.Schedules
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string TeacherCode { get; set; }
        public string TeacherName { get; set; }
        public string text { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
