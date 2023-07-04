
using AutoMapper;

namespace e_commerce.api.Controllers.Base
{

[ServiceFilter(typeof(ExceptionFilter))]
[Route(ActionConsts.ApiVersion)]
[Produces("application/json")]
[EnableCors("CorsPolicy")]
[ApiController]

    public class BaseController : Controller
    {
        public EcommerceContext context { get; set; }

        protected ICrypto Crypto { get; set; }

        public IMapper Mapper { get; set; }

        protected IStringLocalizer<BaseController> Localizer { get; set; }


        public BaseController(
            ICrypto crypto,
            EcommerceContext context,
            IStringLocalizer<BaseController> Localizer,
            IMapper mapper
            )
        {
            this.Crypto = crypto;
            this.context = context;
            this.Localizer = Localizer; 
            this.Mapper = mapper;
        }


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
