using ecommerce.models.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Response
{
    public class UserSigninResponse : BaseResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Image { get; set; }

        public string? Token { get; set; }
    }
}
