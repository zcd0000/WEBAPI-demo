using PPTS.Web.Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students = PPTS.Web.Student.Models.Students;

namespace PPTS.Web.Student.TempServices
{
    public class StudentService
    {
        private static List<Students.Student> AllStudentData = new List<Students.Student>();

        public static PagedData<Students.Student> GetIndexStudentList()
        {
            CreateAllStudentData();

            PagedParam pagedParam = new PagedParam();
            List<Students.Student> list = (from s in AllStudentData
                                           select s)
                                           .Skip((pagedParam.Page - 1) * pagedParam.Limit)
                                           .Take(pagedParam.Limit)
                                           .ToList();
            pagedParam = new PagedParam(pagedParam.Page, pagedParam.Limit, AllStudentData.Count);
            PagedData<Students.Student> pagedData = new PagedData<Models.Students.Student>(pagedParam);
            pagedData.Data = list;

            return pagedData;
        }

        public static PagedData<Students.Student> SimpleSearchStudentList(Students.StudentSimpleSearchCriteria criteria)
        {
            CreateAllStudentData();

            if (criteria.PagedParam == null) criteria.PagedParam = new PagedParam();
            var query = AllStudentData.AsQueryable();
            if (!string.IsNullOrEmpty(criteria.Name)) query = query.Where(s => s.Name.Contains(criteria.Name));
            if (!string.IsNullOrEmpty(criteria.Code)) query = query.Where(s => s.Code.Contains(criteria.Code));
            if (!string.IsNullOrEmpty(criteria.TeacherXG)) query = query.Where(s => s.TeacherXG.Contains(criteria.TeacherXG));
            if (!string.IsNullOrEmpty(criteria.TeacherZX)) query = query.Where(s => s.TeacherZX.Contains(criteria.TeacherZX));
            if (!string.IsNullOrEmpty(criteria.Contact)) query = query.Where(s => s.Contact.Contains(criteria.Contact));

            int totalCount = query.Count();

            List<Students.Student> list = query.Skip((criteria.PagedParam.Page - 1) * criteria.PagedParam.Limit).Take(criteria.PagedParam.Limit).ToList();

            PagedData<Students.Student> pagedData = new PagedData<Models.Students.Student>(new PagedParam(criteria.PagedParam.Page, criteria.PagedParam.Limit, totalCount))
            {
                Data = list
            };

            return pagedData;
        }

        public static PagedData<Students.Student> AdvanceSearchStudentList(Students.StudentSimpleSearchCriteria criteria)
        {
            throw new NotImplementedException("未实现");
        }

        private static void CreateAllStudentData()
        {
            if (AllStudentData.Count == 0)
            {
                string[] allNames = new string[] { "丁聪华", "夏潇琦", "曾帛员", "韩松", "孙蝶妃", "江浩华", "田宇旺", "孔良超", "许娇翔", "庞妍", "陈莲眉", "冉迪振", "崔子希", "曹娅娴", "张红", "陈寿渊", "樊瑶芳", "唐亚升", "马桂蓓", "徐经岚", "阮恭琴", "任希", "孙花", "赵美珍", "姚道益", "汪冰蕴", "何彦", "米泽升", "朱咏娴", "陆银兴", "宋牡馨", "邓材民", "郭秀晶", "康天鹏", "钱亚凤", "龚蓝莹", "古璐", "严秋伶", "范坤", "阮龙", "袁庆轩", "曹欣丹", "田婕灵", "程杏倚", "戴敏", "顾良龙", "熊月辰", "许永岚", "邹雁", "程双民", "陆道根", "寇瑞生", "马庆炳", "龚元才", "黄敬甫", "魏怡玉", "孔睫薇", "崔喻晶", "阮菊斐", "康晓", "江斌", "曾勇" };
                /*, "孔苇 冯添桂 伍兆斌 方艾健 米希雨 王瑶伶 成萍娴 余卓超 严登武 宋安璨 江美婕 谢晓香 杨甚璨 郑前岚 樊革民 伍一雨 吴潇凤 周 益  寇 荔 毛倚娴 樊 颖 沈天根  吴均武 冉欣彦 罗 娟 孟家鹏 叶致玉 陆 琴 米 昆 蒋瑜馨 钱富武 钟苇娇 唐远先 钱嘉澜 赵 竣  孔静香  井 剑  蒋天振  夏楠蓓 彭雪薇 何星振 陈 茵  余少庆 韩 娅 石 穗 韩则龙  鲁艾弈 钟蝶辰 成 彤 樊 霜  陈 刚 庞 燕 毛安聚 韩惠宜  孙冰韵 邓飘雨 方富玄 朱 千 井焱丹 邓天贵 罗 娅 井 辰 邹 爽 刘 志 刘荣时 贾 蓉  张小瑜 李蝶红 高 莹 阮艾杰 蒋言柔 吕 超 马 瑗 张尉蓉 杨元雄 林进棉 唐亚奇 吕彦花 张滕龙 封 勇 汪牡蝶 顾锐萧  龚景萧 罗建卿 孙 晶 孔娅萱 万平洪 黄杏玉 范继聚 顾天姿 赵 翠 宋香柔 吴久坤 简蓝芸  姚静薇 陈 刚 庞 燕 毛安聚  韩惠宜 孙冰韵 邓飘雨 方富玄  朱 千 井焱丹 邓天贵 罗 娅  井 辰 邹 爽 刘 志 刘荣时 贾 蓉 张小瑜 李蝶红 高 莹 女
                阮艾杰 蒋言柔 吕 超 马 瑗 张尉蓉 杨元雄 林进棉 唐亚奇 吕彦花 张滕龙 封 勇 汪牡蝶  顾锐萧 龚景萧 罗建卿 孙 晶 杜秋菲  杨中贵  邹兼迪  高 云    王书棋 钱庆洪 唐 宾 张 琼  石关卿 罗喻欢 张金霏 顾 玲 宋 浒 罗 平 方旭红 赵同原 江庆惠 成加赋 蒋茹妍 封腾麟 贾仙盈 曹尚清 谢兴江 樊婕盈 阮 妍 宋牡妍 郭文权 杜富赋  谢 翠 刘恩帝 井苇仪 樊 昆  邹仕麟 龚蓝艳 钟安驰 曹千帝  杨 麟 万 兵 伍杏芮 许秉靖  孙继贵 樊亚荣 吕维蓝 李仙仪  庞加杰 米 君 米众光 吕毕丹 徐静蕾 曾农彤 康蓝露 孙庆材 胡巧惠 姚仕能 曾 秀 阮秋帛  伍 洪 伍一帛 方旭红 赵同原 江庆惠 成加赋 蒋茹妍 封腾麟 贾仙盈 曹尚清 谢兴江 樊婕盈 阮 妍 宋牡妍 郭文权 杜富赋  谢 翠 刘恩帝 井苇仪 樊 昆  邹仕麟 龚蓝艳 钟安驰 曹千帝  杨 麟  万 兵  伍杏芮  许秉靖    袁添君 赖益南 梁晓茜 古禹伟  范意蓝 万 苗 万必彤 梁美书 严婵鹃 鲁秋玫 封惜菊 袁森华 杨益强 庞啸剑 鲁 帅 周 吉 王众凯 徐滕建 夏 良 侯婵玫  郭泽珍 毛富琦 何 芹 叶江丰 冉恩晨 杜匀珍 叶 霞 阮 海 樊惠玫 余 秀 封 英 康少清 唐在川 李蝶蕴 崔致敏 江同能 方盈琦 孙寿泉 邹月蕴 汤则璨 刘汝丽 钱 兴 马文松 魏苇株  韩秉丰 高众渊 熊可渊 吕菊鹃  胡惠书 龚家晨 唐飘遥 杨萍丽  邓兆炳 成尚帅 严 华 许珍琴 冯心琪 丁 芷 方顺清 封仕毅  黄雁遥 侯映榕 阮 影 曾玉芳 叶 奇 罗关民 苏伟升 封齐兴  万 晋 戴求文 魏清鹏 许观林 韩悦菊 徐应珍 曹 洁 阮申远 男
                姚尉倚 袁玲琴 赵 云 吴 璐  唐彪远 何发升 冉光贤 夏禹健  康萍璇 庞颜帅 蔡海琦 江开璨  余致南 王吉 赖固建 邓莲吟 伍 千  孙 琦  夏丰兴  梁任璨  方若强 郑富蔓 梁苇倚 熊 剑  陆枝妃 成 华 方 晶 徐 强 陆齐榕 汤惠莹 孔啸升 陈必林 田婕遥 唐 麟 田遥琼 曾虹璇  方秀蓓 许 勇 孙友甫 姚润琴 黄 飞 李以波 蔡志帅 毛婕娜  杨汝辰 程雁薇 郭文兴 古小华  徐静玉 林铭龙 梁伯华 罗彩倚  周 莎 徐茂帅 黄菊莹 阮盈桂 赖利雨 王烈鹏 龚发城 杜申兴 吕枝莉 陆恭媛 夏丰兴 梁任璨 钟香蓉 任 希 曾欢娥 阮 棋 赖 英 戴庆明 罗娇琳 曹镇贤  万怀宜 杜桂怡 熊本才 陈玲洁  沈凤丹 郭 丹 夏业弈 孔畅国 李 宁 曾再升 汪莲琳 徐农平  蔡子强 谢 坤 杨益卿 吕彦眉 侯求武 古世瑜 马心玲 蒋景香 冉镇龙 余秋瑾 韩凤芬 钱文奇 徐孝姣  戴清江  方 琦  封尉馨    黄毕海 韩敬良 陆 结 孙秀玉 熊从发 张映棉 陈娇玉 赵 诗  袁怀斐 周 敬 高 君 周时雪  程汝蓉 戴庆明 罗娇琳 曹镇贤  万怀宜 杜桂怡 熊本才 陈玲洁  沈凤丹 郭 丹 夏业弈 孔畅国 李 宁 曾再升 汪莲琳 徐农平  蔡子强 谢 坤 杨益卿 吕彦眉 侯求武 古世瑜 马心玲 蒋景香 冉镇龙 余秋瑾 韩凤芬 钱文奇 徐孝姣 戴清江 方 琦 封尉馨  黄毕海 韩敬良 吕求玄 封齐炳  贾先信 樊参明 朱帛彤 邹盈璐  丁卓恒 戴蝶倩 余致斌 鲁均明  顾晓红 任固夫 汤敬棉 钱佳光  江雯鹃 江 康 简秋倩 方 海  林富颖 何孝荃 郑 瑾 程 凤  唐榕欢 冯森飞 程加明 黄登棉  汪平卿 沈 眉 陆恭瑾 熊 毅 男
                */
                string[] allSchoolNames = new string[] { "人大附中", "北京四中", "北师大实验中学", "清华附中", "北师大二附中", "北京八中", "北大附中", "十一学校", "北京101中", "北京二中", "首师大附中", "北京五中", "北师大附中", "八一中学" };
                string[] allGradeNames = new string[] { "初中一年级", "初中二年级", "初中三年级", "高中一年级", "高中二年级", "高中三年级" };
                string[] allXGTeacherNames = new string[] { "姚尉倚", "袁玲琴", "吴璐", "唐彪远", "何发升", "", "冉光贤", "" };
                string[] allZXTeacherNames = new string[] { "夏禹健", "", "庞颜帅", "", "袁玲琴", "余致南", "何发升", "邓莲吟" };

                Random rnd = new Random();
                int count = allNames.Length;
                for (int i = 0; i < count; i++)
                {
                    Students.Student student = new Students.Student();
                    student.Name = allNames[i];
                    student.Code = "S" + (151105000101 + i).ToString();
                    student.ParentsName = allNames[count - i - 1];
                    student.FirstContractDate = new DateTime(2016, 1, 8).AddHours(6 * i);
                    student.SchoolName = allSchoolNames[i % allSchoolNames.Length];
                    student.GradeName = allGradeNames[i % allGradeNames.Length];
                    student.TeacherXG = allXGTeacherNames[i % allXGTeacherNames.Length];
                    student.TeacherZX = allZXTeacherNames[i % allZXTeacherNames.Length];
                    student.ContractCount = rnd.Next(2, 11) * 10;
                    student.RemainCount = student.ContractCount - rnd.Next(0, 20);
                    student.AccountValue = student.ContractCount * 200 + rnd.Next(0, 100) * 100;
                    student.BalanceValue = student.RemainCount * 200;
                    student.AvalibleValue = student.AccountValue - student.ContractCount * 200;
                    student.HourFromLastClass = rnd.Next(0, 24);
                    student.Contact = student.SchoolName;
                    student.Status = student.Name.Length > 2 ? 1 : 0;

                    AllStudentData.Add(student);
                }
            }
        }
    }
}
