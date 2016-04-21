using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.Student.Models
{
    public class DataResult
    {
        /// <summary>
        /// 0为成功，其他为失败编码
        /// </summary>
        public int Code { get; set; }
    
        /// <summary>
        /// 成功时为成功信息，错误时为失败消息
        /// </summary>
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public DataResult() { }
        public DataResult(dynamic data)
        {
            this.Code = 0;
            this.Data = data;
        }
        public DataResult(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public DataResult(int code, string message, dynamic data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }
}
