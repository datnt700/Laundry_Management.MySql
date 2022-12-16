namespace Laundry_Management.DTO.Request
{
    public class FitlerUserModel 
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public FitlerUserModel()
        {
            PageIndex = 1;
            PageSize = 2;
        }
        public FitlerUserModel(int pageNumber, int pageSize)
        {
            PageIndex = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 2 ? 2 : pageSize;
        }
    }
}
