using PPTS.Web.Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedules = PPTS.Web.Course.Models.Schedules;

namespace PPTS.Web.Course.TempServices
{
    public class ScheduleService
    {
        private static List<Schedules.Schedule> AllScheduleData = new List<Schedules.Schedule>();

        private static List<Schedules.Teacher> AllTeacherData = new List<Schedules.Teacher>();

        private static List<Schedules.Department> AllDepartmentData = new List<Schedules.Department>();

        private static void CreateAllDepartmentData()
        {
            if (AllDepartmentData.Count == 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    Models.Schedules.Department department = new Models.Schedules.Department()
                    {
                        id = Guid.NewGuid(),
                        name = i.ToString(),
                        isParent = true,
                        children = new List<Schedules.Department>()
                    };
                    AllDepartmentData.Add(department);
                    for (int j = 1; j < 10; j++)
                    {
                        Models.Schedules.Department department1 = new Models.Schedules.Department()
                        {
                            id = Guid.NewGuid(),
                            name = i.ToString() + "." + j.ToString(),
                            isParent = true,
                            children = new List<Schedules.Department>()
                        };
                        department.children.Add(department1);

                        for (int k = 1; k < 10; k++)
                        {
                            Models.Schedules.Department department2 = new Models.Schedules.Department()
                            {
                                id = Guid.NewGuid(),
                                name = i.ToString() + "." + j.ToString() + "." + k.ToString(),
                                isParent = false,
                                children = new List<Schedules.Department>()
                            };
                            department1.children.Add(department2);
                        }
                    }
                }
            }
        }

        public static List<Schedules.Department> QueryChildDepartments(Guid? id)
        {
            CreateAllDepartmentData();

            List<Schedules.Department> list = new List<Models.Schedules.Department>();
            if (!id.HasValue)
            {
                foreach (Schedules.Department d in AllDepartmentData)
                {
                    list.Add(new Models.Schedules.Department()
                    {
                        id = d.id,
                        name = d.name,
                        isParent = d.isParent,
                        children = new List<Models.Schedules.Department>()
                    });
                }
            }
            else
            {
                Schedules.Department root = null;
                foreach (Schedules.Department d1 in AllDepartmentData)
                {
                    if (d1.id == id.Value)
                    {
                        root = d1;
                        break;
                    }
                    else
                    {
                        foreach (Schedules.Department d2 in d1.children)
                        {
                            if (d2.id == id.Value)
                            {
                                root = d2;
                                break;
                            }
                        }
                        if (root != null) break;
                    }
                }
                if (root == null) return new List<Schedules.Department>();
                else
                {
                    foreach (Schedules.Department d in root.children)
                    {
                        list.Add(new Models.Schedules.Department()
                        {
                            id = d.id,
                            name = d.name,
                            isParent = d.isParent,
                            children = new List<Models.Schedules.Department>()
                        });
                    }
                }
            }

            return list;
        }

        public static List<Schedules.Department> QueryAllDepartments()
        {
            CreateAllDepartmentData();
            return AllDepartmentData;
        }

        public static PagedData<Schedules.Schedule> GetIndexScheduleList()
        {
            //CreateAllStudentData();

            //PagedParam pagedParam = new PagedParam();
            //List<Students.Student> list = (from s in AllStudentData
            //                               select s)
            //                               .Skip((pagedParam.Page - 1) * pagedParam.Limit)
            //                               .Take(pagedParam.Limit)
            //                               .ToList();
            //pagedParam = new PagedParam(pagedParam.Page, pagedParam.Limit, AllStudentData.Count);
            //PagedData<Students.Student> pagedData = new PagedData<Models.Students.Student>(pagedParam);
            //pagedData.Data = list;

            //return pagedData;
            throw new NotImplementedException();
        }

        public static List<Schedules.Schedule> SimpleSearchScheduleList(Schedules.ScheduleSimpleSearchCriteria criteria)
        {
            CreateAllScheduleData();
            var query = AllScheduleData.Where(a => a.start >= criteria.Start && a.end <= criteria.End);
            if (criteria.Teachers != null && criteria.Teachers.Count > 0)
            {
                foreach (Schedules.Teacher teacher in criteria.Teachers)
                {
                    query = query.Where(a => a.title.Contains(teacher.TeacherName));
                }
            }
            return query.ToList();
        }

        public static List<Schedules.Teacher> QueryTeacherList(Schedules.TeacherQueryCriteria criteria)
        {
            CreateAllTeacherData();
            return AllTeacherData.Where(a => a.TeacherCode.StartsWith(criteria.Keyword, StringComparison.CurrentCultureIgnoreCase) || a.TeacherName.StartsWith(criteria.Keyword, StringComparison.CurrentCultureIgnoreCase)).Take(10).ToList();
        }

        private static void CreateAllTeacherData()
        {
            if (AllTeacherData.Count == 0)
            {
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "fangaijian",
                    text = "fangaijian",
                    TeacherName = "方艾健",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "物理"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "fangaijian1",
                    text = "fangaijian1",
                    TeacherName = "方艾健",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "化学"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "fengtiangui",
                    text = "fengtiangui",
                    TeacherName = "冯添桂",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "化学"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "wuzhaobin",
                    text = "wuzhaobin",
                    TeacherName = "伍兆斌",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "历史"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "mixiyu",
                    text = "mixiyu",
                    TeacherName = "米希雨",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "化学"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "wangyaoling",
                    text = "wangyaoling",
                    TeacherName = "王瑶伶",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "化学"
                });
                AllTeacherData.Add(new Models.Schedules.Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    TeacherCode = "chengpingxian",
                    text = "chengpingxian",
                    TeacherName = "成萍娴",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentName = "化学"
                });
            }
        }

        private static void CreateAllScheduleData()
        {
            if (AllScheduleData.Count == 0)
            {
                string BR = "\r\n";
                string SPACE = " ";

                string[] courses = new string[] { "语文", "数学", "英语", "物理", "生物", "历史" };
                string[] colors = new string[] { "#82af6f", "#d15b47", "#9585bf", "#fee188", "#d6487e", "#3a87ad" };
                string[] textColors = new string[] { "#fff", "#fff", "#fff", "rgb(153, 102, 51)", "#fff", "#fff" };
                string[] allNames = new string[] { "孔苇", "冯添桂", "伍兆斌", "方艾健", "米希雨", "王瑶伶", "成萍娴", "余卓超" };
                string[] allStatuss = new string[] { "排定", "已上" };

                int[][] times = new int[][]
                {
                    new int[] {6, 30, 7, 0},
                    new int[] {8, 0, 9, 0},
                    new int[] {11, 0, 12, 0},
                    new int[] {13, 0, 13, 30},
                    new int[] {16, 0, 16, 30},
                    new int[] {18, 0, 19, 0}
                };

                Random rnd = new Random();
                for (DateTime dtStart = DateTime.Now.AddMonths(-2); dtStart < DateTime.Now.AddMonths(3); dtStart = dtStart.AddDays(1))
                {
                    int count = rnd.Next(times.Length);
                    List<int> indexList = GetRandomIndexList(count, times.Length, rnd);
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int[] time = times[indexList[i]];
                        int courseIndex = rnd.Next(courses.Length);
                        Schedules.Schedule schedule = new Schedules.Schedule()
                        {
                            id = Guid.NewGuid(),
                            title = "高三" + courses[courseIndex] + SPACE + allNames[rnd.Next(allNames.Length)],// + BR + allStatuss[rnd.Next(allStatuss.Length)],
                            allDay = false,
                            start = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[0], time[1], 0),
                            end = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[2], time[3], 0)
                        };
                        schedule.startText = schedule.start.ToString("HH:mm");
                        schedule.endText = schedule.end.ToString("HH:mm");
                        schedule.duration = (schedule.start - DateTime.Now).TotalDays;
                        if (schedule.start < DateTime.Now)
                        {
                            schedule.status = "已上";
                        }
                        else
                        {
                            schedule.status = "排定";
                        }

                        AllScheduleData.Add(schedule);
                    }
                }
            }
        }

        private static void CreateAllScheduleData1()
        {
            if (AllScheduleData.Count == 0)
            {
                string BR = "\r\n";
                string SPACE = " ";

                string[] courses = new string[] { "语文", "数学", "英语", "物理", "生物", "历史" };
                string[] colors = new string[] { "#82af6f", "#d15b47", "#9585bf", "#fee188", "#d6487e", "#3a87ad" };
                string[] textColors = new string[] { "#fff", "#fff", "#fff", "rgb(153, 102, 51)", "#fff", "#fff" };
                string[] allNames = new string[] { "孔苇", "冯添桂", "伍兆斌", "方艾健", "米希雨", "王瑶伶", "成萍娴", "余卓超" };
                string[] allStatuss = new string[] { "排定", "已上", "异常", "取消", "已删除" };

                int[][] times = new int[][]
                {
                    new int[] {6, 7},
                    new int[] {8, 10},
                    new int[] {11, 12},
                    new int[] {13, 16},
                    new int[] {16, 18},
                    new int[] {18, 19}
                };

                Random rnd = new Random();
                for (DateTime dtStart = DateTime.Now.AddMonths(-2); dtStart < DateTime.Now.AddMonths(3); dtStart = dtStart.AddDays(1))
                {
                    int count = rnd.Next(times.Length);
                    List<int> indexList = GetRandomIndexList(count, times.Length, rnd);
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int[] time = times[indexList[i]];
                        int courseIndex = rnd.Next(courses.Length);
                        Schedules.Schedule schedule = new Schedules.Schedule()
                        {
                            id = Guid.NewGuid(),
                            title = "高中三年级" + BR + courses[courseIndex] + SPACE + allNames[rnd.Next(allNames.Length)] + BR + allStatuss[rnd.Next(allStatuss.Length)],
                            allDay = false,
                            start = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[0], 0, 0),
                            end = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[1], 0, 0)
                        };
                        if (schedule.start < DateTime.Now)
                        {
                            schedule.color = "#a0a0a0";
                            schedule.textColor = "#fff";
                        }
                        else
                        {
                            schedule.color = colors[courseIndex];
                            schedule.textColor = textColors[courseIndex];
                        }

                        AllScheduleData.Add(schedule);
                    }
                }
            }
        }

        private static List<int> GetRandomIndexList(int count, int maxCount, Random rnd)
        {
            List<int> list = new List<int>();
            int mycount = 0;
            while (mycount < count)
            {
                int number = rnd.Next(maxCount);
                if (!list.Contains(number))
                {
                    list.Add(number);
                    mycount++;
                }
            }
            return list;
        }
    }
}
