using AutoMapper;
using ecommerce.repository;
using ecommerce.security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static e_commerce.api.Filter.ExceptionFilter;

namespace e_commerce.api.Controllers
{
    public class BaseController : Controller
    {
        public EcommerceContext DbContext { get; set; }
        //public IMapper Mapper { get; set; }

        protected IStringLocalizer<BaseController> Localizer { get; set; }

        protected ICrypto Crypto { get; set; }


        public BaseController(
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        EcommerceContext DbContext)
        {
            this.Localizer = Localizer;
            this.Crypto = Crypto;
            this.DbContext = DbContext;
            //this.Mapper = Mapper;
        }

        #region 1.Get current user's data using Token

        protected bool IsLoggedIn(ClaimsPrincipal User) => User.Identity.IsAuthenticated;

        protected Guid GetUserId(ClaimsPrincipal User)
        {
            var user_id = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault().Value;

            if (string.IsNullOrEmpty(user_id)) return Guid.Empty;  // Early return

            return Guid.Parse(this.Crypto.Decrypt(user_id));
        }

        protected string GetUserEmail(ClaimsPrincipal User)
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email)?.FirstOrDefault().Value;

            if (string.IsNullOrEmpty(email)) return string.Empty;  // Early return

            return this.Crypto.Decrypt(email);
        }

        protected string GetUserName(ClaimsPrincipal User)
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.GivenName)?.FirstOrDefault().Value;

            if (string.IsNullOrEmpty(email)) return string.Empty;  // Early return

            return this.Crypto.Decrypt(email);
        }

        protected string GetUniqueId(ClaimsPrincipal User)
        {
            var uniqueId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti)?.FirstOrDefault().Value;

            if (string.IsNullOrEmpty(uniqueId)) return string.Empty;  // Early return

            return this.Crypto.Decrypt(uniqueId);
        }

        protected string GetUserFullName(ClaimsPrincipal User)
        {
            var username = User.Claims.Where(x => x.Type.Equals(ClaimTypes.GivenName))?.FirstOrDefault().Value;

            return this.Crypto.Decrypt(username);
        }

        protected string GetUserRole(ClaimsPrincipal User)
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(role)) return string.Empty;  // Early return

            return this.Crypto.Decrypt(role);
        }

        #endregion 1.Get current user's data using Token

        #region 2. OkResponse

        protected IActionResult OkResponse() => this.Ok(new
        {
            meta = new
            {
                message = this.Localizer["ok_response_successful"].Value,
                statusCode = 200
            }
        });

        protected IActionResult OkResponse(string message) => this.Ok(new
        {
            meta = new { message = this.Localizer[message].Value, statusCode = 200 }
        });

        protected IActionResult OkResponse(string message, object data) => this.Ok(new
        {
            meta = new { message = this.Localizer[message].Value, statusCode = 200 },
            data = data
        });

        protected IActionResult OkResponse(object data) => this.Ok(new
        {
            meta = new { message = this.Localizer["ok_response_successful"].Value, statusCode = 200 },
            data = data
        });

        protected IActionResult OkResponse(IEnumerable<dynamic> details)
        {
            var data = new { details };
            return this.Ok(new
            {
                meta = new { message = this.Localizer["ok_response_successful"].Value, statusCode = 200 },
                data
            });
        }

        #endregion 2. OkResponse

        #region 3. Failed Response

        protected IActionResult ErrorResponse() => throw new ApiException("error_something_went_wrong", 400);

        protected IActionResult ErrorResponse(string message) => throw new ApiException(message, 400);

        protected IActionResult ErrorResponse(string message, int statusCode) => throw new ApiException(message, statusCode);

        protected IActionResult ErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
        {
            string message = string.Join("; ", ModelState.SelectMany(x => x.Value.Errors).Select(x => this.Localizer[x.ErrorMessage].Value));
            throw new ApiException(message, 400);
        }

        protected IActionResult ErrorResponse(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState, int statusCode)
        {
            string message = string.Join("; ", ModelState.SelectMany(x => x.Value.Errors).Select(x => this.Localizer[x.ErrorMessage].Value));
            throw new ApiException(message, statusCode);
        }

        #endregion 3. Failed Response
    }
}
