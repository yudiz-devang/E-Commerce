using AutoMapper;
using e_commerce.api.Helpers;
using ecommerce.models.config;
using ecommerce.models.Constants;
using ecommerce.models.Request.User;
using ecommerce.models.Response;
using ecommerce.repository;
using ecommerce.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace e_commerce.api.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly EcommerceContext _context;

        protected ICrypto Crypto { get; set; }


        public UserController
        (IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto, EcommerceContext context) : base(
            Localizer,
            Crypto,
            context)
        {
            _context = context;
        }

        #region User Sign In 
        [AllowAnonymous]
        [HttpPost(ActionConsts.User.UserSignIn)]
        public async Task<IActionResult> UserSignin([FromBody] UserSigninReqeust reqeust, [FromServices] IOptions<AuthConfigs> AuthConfigOptions)
        {
            if (reqeust == null) new UserSigninReqeust();
            if (!this.ModelState.IsValid)
                return this.ErrorResponse(this.ModelState);
            using var helper = new UserHelper(this._context,this.Crypto);
            
            var helperresponse = await helper.Login(reqeust);
            var response = JsonConvert.DeserializeObject<UserSigninResponse>(helperresponse.Data);
            response.Token = new UserTokenHelpers(this.Crypto).GetAccessToken(AuthConfigOptions.Value, response);

            return this.OkResponse(response);
        }
        #endregion

        #region User Signup
        [AllowAnonymous, HttpPost(ActionConsts.User.UserSignUp)]
        public async Task<IActionResult> UserSignUp([FromBody] UserSignupRequest request)
        {
            if (!this.ModelState.IsValid)
                return this.ErrorResponse(this.ModelState);

            using var helper = new UserHelper(this._context,this.Crypto);
            var helperresponse = await helper.SignUp(request);
            if (helperresponse == null) return BadRequest();
            return OkResponse(helperresponse);

        }
        #endregion

    }
}
