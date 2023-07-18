using ecommerce.models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Entities
{
    public class UsersEntity : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string EmailId { get; set; }

        public bool EmailVerified { get; set; } = false;

        [MaxLength(7)]
        public string? EmailOtp { get; set; }

        public string? MobileNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public int? ZipCode { get; set; }

        public string? Image { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public Guid ModifiedBy { get; set; }

        public string? Paymenttype { get; set; }

        public bool? IsAdmin { get; set; }

    }
}
