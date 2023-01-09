using Laundry_Management.Common;

namespace Laundry_Management.DTO
{
    public class Paginate : ResponseResult
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        //public Paginate(dynamic data ,int pageIndex, int pageSize)
        //{
        //    this.Data = data;
        //    this.PageIndex = pageIndex;
        //    this.PageSize = pageSize;
        //    this.StatusCode = 200;
        //}
    }

    
}
