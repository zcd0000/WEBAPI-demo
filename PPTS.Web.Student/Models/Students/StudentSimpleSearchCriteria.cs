using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Student.Models.Students
{
    public class StudentSimpleSearchCriteria
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string TeacherZX { get; set; }
        public string TeacherXG { get; set; }
        public string Contact { get; set; }
        public StudentSimpleSearchCriteria() { }
        public PagedParam PagedParam { get; set; }
    }
}
