using e_commerce.api.Helpers;
using ecommerce.models.Constants;
using ecommerce.models.Request.Admin;
using ecommerce.models.Request.User;
using ecommerce.models.Response;
using ecommerce.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace e_commerce.api.Controllers
{
    [Authorize]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly EcommerceContext _context;

        public AdminController(EcommerceContext context)
        {
            _context = context;
        }

        #region Admin SignIn
        [AllowAnonymous]
        [HttpPost(ActionConsts.Admin.UserSignIn)]
        public async Task<IActionResult> AdminSignIn([FromBody] UserSigninReqeust reqeust)
        {
            if (reqeust == null) { new UserSigninReqeust(); }
            using var helper = new AdminHelpers(this._context);
            reqeust.UniqueId = Guid.NewGuid().ToString();
            var helperresponse = await helper.Login(reqeust);
            var response = JsonConvert.DeserializeObject<UserSigninResponse>(helperresponse.Data);
            return Ok(response);
        }
        #endregion
    }
}
