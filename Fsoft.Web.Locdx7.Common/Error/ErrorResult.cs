using Fsoft.Web.Locdx7.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsoft.Web.Locdx7.Common.Error
{
    public class ErrorResult : Exception
    {
        /// <summary>
        /// Kết quả trả về 1 object lỗi
        /// </summary>
        public FsoftErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Link viết chi tiết lỗi
        /// </summary>
        public dynamic? MoreInfo { get; set; }

        /// <summary>
        /// ID trace
        /// </summary>
        public string? TraceId { get; set; }

        public ErrorResult(FsoftErrorCode errorCode, string devMsg, string userMsg, dynamic? moreInfo, string? traceId = null)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }
    }
}
