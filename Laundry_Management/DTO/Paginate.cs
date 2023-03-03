using Laundry_Management.Common;

namespace Laundry_Management.DTO
{
    public class Paginate
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        //public Paginate(dynamic data ,int pageIndex, int pageSize)
        //{
        //    this.Data = data;
        //    this.PageIndex = pageIndex;
        //    this.PageSize = pageSize;
        //    this.StatusCode = 200;
        //}
    }

    
}
