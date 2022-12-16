using System.Net;

namespace Laundry_Management.Common
{
    public class ResponseResult
    {
        public int StatusCode { get; set; }
        public dynamic Data { get; set; }
        public string User { get; set; }
        public string Message { get; set; }

        public ResponseResult ResponseSuccess(dynamic data = null,string msg = "")
        {
            return new ResponseResult
            {
                StatusCode = 200,
                Data = data,
                Message = string.IsNullOrWhiteSpace(msg) ? "Thành công" : msg
            };
        }

        public ResponseResult ResponsFailure(dynamic data = null, string msg = "")
        {
            return new ResponseResult
            {
                StatusCode = 400,
                Data = data,
                Message = string.IsNullOrWhiteSpace(msg) ? "Thất bại" : msg
            };
        }
    }
}
