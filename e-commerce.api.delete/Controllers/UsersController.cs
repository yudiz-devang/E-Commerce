
using AutoMapper;

namespace e_commerce.api.Controllers
{
    public class UsersController : BaseController
    {
        #region Constructor 
        public IActionResult Index()
        {
            return View();
        }
        public UsersController(
            ICrypto Crypto,
            EcommerceContext Dbcontext,
            IStringLocalizer<BaseController> Localizer,
            IMapper Mapper

            ) : base(Crypto,Dbcontext,Localizer,Mapper)
        {
        }
        #endregion

        #region 1. User Sign in 

        [AllowAnonymous, HttpPost(ActionConsts.User.UserSignIn)]
        public async Task<IActionResult> UserLoginAsync([FromBody] UserSigninReqeust reqeust)
        {
            if (reqeust == null) new UserSigninReqeust();

            if (!ModelState.IsValid) return this.ErrorResponse(this.ModelState);

            using var helper = new UserHelper(this.context, this.Crypto, this.Mapper);

            reqeust.UniqueId = Guid.NewGuid().ToString();

            var helperresponse = await helper.Login(reqeust);

            if (helperresponse == null) return this.ErrorResponse();

            if (!helperresponse.IsSuccess) return this.ErrorResponse(helperresponse.ErrorMessage);

            var response = JsonConvert.DeserializeObject<UserSigninResponse>(helperresponse.Data);

            return OkResponse(response);
        }
        #endregion

    }
}
