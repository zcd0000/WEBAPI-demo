using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Course.Models.Schedules
{
    public class Department
    {
        public Guid id { get; set; }
        public List<Department> children { get; set; }
        public string name { get; set; }
        public bool isParent { get; set; }
    }
}
