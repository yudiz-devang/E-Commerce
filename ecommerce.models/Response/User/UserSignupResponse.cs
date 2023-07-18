using ecommerce.models.Response.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Response.User
{
    public class UserSignupResponse : BaseAuditResponse
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public int? ZipCode { get; set; }

        public string Image { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Paymenttype { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public bool? IsAdmin { get; set; }    
    }
}
