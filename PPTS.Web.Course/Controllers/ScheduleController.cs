using PPTS.Web.Course.Models;
using PPTS.Web.Course.Models.Schedules;
using PPTS.Web.Course.TempServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models = PPTS.Web.Course.Models;

namespace PPTS.Web.Course.Controllers
{
    public class ScheduleController : ApiController
    {
        //GET: api/Schedule/Index
        [HttpGet]
        public DataResult Index()
        {
            System.Threading.Thread.Sleep(500);

            DataResult result = new DataResult()
            {
                Data = new
                {
                    PagedList = ScheduleService.SimpleSearchScheduleList(new ScheduleSimpleSearchCriteria()
                    {
                        Start = DateTime.Now.AddMonths(-2),
                        End = DateTime.Now.AddMonths(2)
                    })
                }
            };

            return result;
        }

        //POST: api/Schedule/SimpleSearch
        [HttpPost]
        public DataResult SimpleSearch(ScheduleSimpleSearchCriteria criteria)
        {
            //System.Threading.Thread.Sleep(500);

            DataResult result = new DataResult()
            {
                Data = new
                {
                    List = ScheduleService.SimpleSearchScheduleList(criteria)
                }
            };

            return result;
        }

        //POST: api/Schedule/QueryChildDepartments
        [HttpPost]
        public DataResult QueryChildDepartments(QueryChildrenCriteria criteria)
        {
            DataResult result = new DataResult()
            {
                Data = new
                {
                    List = ScheduleService.QueryChildDepartments(criteria == null? null : criteria.id)
                }
            };

            return result;
        }

        [HttpPost]
        public DataResult QueryAllDepartments()
        {
            DataResult result = new DataResult()
            {
                Data = new
                {
                    List = ScheduleService.QueryAllDepartments()
                }
            };

            return result;
        }

        //GET: api/Schedule/QueryTeacher
        [HttpPost]
        public DataResult QueryTeacher(TeacherQueryCriteria criteria)
        {
            DataResult result = new DataResult()
            {
                Data = new
                {
                    List = ScheduleService.QueryTeacherList(criteria)
                }
            };
            return result;
        }

        ////POST: api/Student/AdvanceSearch
        //[HttpPost]
        //public DataResult AdvanceSearch(StudentSimpleSearchCriteria criteria)
        //{
        //    System.Threading.Thread.Sleep(500);

        //    DataResult result = new DataResult(1, "此功能还未实现")
        //    {
        //        Data = new
        //        {
        //            PagedList = StudentService.AdvanceSearchStudentList(criteria)
        //        }
        //    };

        //    return result;
        //}
    }
}