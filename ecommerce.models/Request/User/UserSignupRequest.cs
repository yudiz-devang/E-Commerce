using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Request.User
{
    public class UserSignupRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailId { get; set; }
        
        public string? MobileNumber { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public string? Address { get; set; }
        
        public string? State { get; set; }
        
        public string? City { get; set; }
        
        public int? ZipCode { get; set; }
        
        public string? Image { get; set; }
        
        public string Password { get; set; }
        
        public string? Paymenttype { get; set; }
        
        public bool? IsAdmin { get; set; }
    }
}
