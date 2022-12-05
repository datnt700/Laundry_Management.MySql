namespace Laundry_Management.DTO.Request
{
    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Password
        {
            get; set;
        }
    }
}
