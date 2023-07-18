using System.ComponentModel.DataAnnotations;

namespace e_commerce.admin.Model.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Please Enter Email Id!")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "error_password_required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain the Alphabet, Digits and Special charaters with atleast 8 characters.", ErrorMessageResourceName = "error_password_invalid")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        public string? UniqueId { get; set; }
    }
}
