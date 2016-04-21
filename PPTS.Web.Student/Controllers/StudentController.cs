using PPTS.Web.Student.Models;
using PPTS.Web.Student.Models.Students;
using PPTS.Web.Student.TempServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models = PPTS.Web.Student.Models;

namespace PPTS.Web.Student.Controllers
{
    public class StudentController : ApiController
    {
        //GET: api/Student/Index
        [HttpGet]
        public DataResult Index()
        {
            System.Threading.Thread.Sleep(500);

            DataResult result = new DataResult()
            {
                Data = new
                {
                    PagedList = StudentService.GetIndexStudentList()
                }
            };

            return result;
        }

    

        //POST: api/Student/SimpleSearch
        [HttpPost]
        public DataResult SimpleSearch(StudentSimpleSearchCriteria criteria)
        {
            System.Threading.Thread.Sleep(500);

            DataResult result = new DataResult()
            {
                Data = new
                {
                    PagedList = StudentService.SimpleSearchStudentList(criteria)
                }
            };

            return result;
        }

        //POST: api/Student/AdvanceSearch
        [HttpPost]
        public DataResult AdvanceSearch(StudentSimpleSearchCriteria criteria)
        {
            System.Threading.Thread.Sleep(500);

            DataResult result = new DataResult(1, "此功能还未实现")
            {
                Data = new
                {
                    PagedList = StudentService.AdvanceSearchStudentList(criteria)
                }
            };

            return result;
        }
    }
}