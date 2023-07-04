using e_commerce.api.Helpers;
using ecommerce.models.Constants;
using ecommerce.models.Request.User;
using ecommerce.models.Response;
using ecommerce.repository;
using ecommerce.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace e_commerce.api.Controllers
{
    public class UserController : Controller
    {
        private readonly EcommerceContext _context;
        public UserController(EcommerceContext context)
        {
            _context = context;
        }

        #region User Sign In 
        [AllowAnonymous, HttpPost(ActionConsts.User.UserSignIn)]
        public async Task<IActionResult> UserSignin([FromBody] UserSigninReqeust reqeust)
        {
            if (reqeust == null) new UserSigninReqeust();
            using var helper = new UserHelper(this._context     );
            reqeust.UniqueId = Guid.NewGuid().ToString();
            var helperresponse = await helper.Login(reqeust);
            var response = JsonConvert.DeserializeObject<UserSigninResponse>(helperresponse.Data);
            return Ok(response);
        }
        #endregion
            
    }
}
