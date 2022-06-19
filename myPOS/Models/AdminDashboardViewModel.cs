namespace myPOS.Models
{
    public class AdminDashboardViewModel
    {
        public AdminDashboardViewModel()
        {
            this.Users = new List<UserDTO>();
        }

        public ICollection<UserDTO> Users { get; set; }
    }
}
