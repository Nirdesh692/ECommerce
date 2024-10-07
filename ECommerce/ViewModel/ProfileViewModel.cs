using ECommerce.Models;

namespace ECommerce.ViewModel
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Shipping>? Shippings { get; set; }


    }
}
