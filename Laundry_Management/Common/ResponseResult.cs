using Laundry_Management.Controllers.Base;
using System.Net;

namespace Laundry_Management.Common
{
    public class ResponseResult
    {
        public int status { get; set; }
        public dynamic data { get; set; }
        public string User { get; set; }
        public string Message { get; set; }

        public ResponseResult ResponseSuccess(dynamic data = null,string msg = "")
        {
            return new ResponseResult
            {
                status = 200,
                data = data,
                
                Message = string.IsNullOrWhiteSpace(msg) ? "Thành công" : msg
            };
        }

        public ResponseResult ResponsFailure(dynamic data = null, string msg = "")
        {
            return new ResponseResult
            {
                status = 400,
                data = data,
                Message = string.IsNullOrWhiteSpace(msg) ? "Thất bại" : msg
            };
        }
    }
}
