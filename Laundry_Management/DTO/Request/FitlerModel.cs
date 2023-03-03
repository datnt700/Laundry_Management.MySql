namespace Laundry_Management.DTO.Request
{
    public class FitlerModel 
    {
        
        public int PageIndex
        {
            get
            {
                return _PageIndex;

            }
            set
            {
                _PageIndex = value < 1 ? 1 : value;
            }
        }
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value < 0  ? 0 : value;
            }
        }

        private int _PageIndex { get; set; }
        private int _PageSize { get; set; }

        public string search { get; set; } = "";


    }

}
