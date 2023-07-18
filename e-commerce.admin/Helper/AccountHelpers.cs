using e_commerce.admin.Model.Const;
using e_commerce.admin.Model.Request;
using e_commerce.admin.Service.Shared;
using Newtonsoft.Json;
using System.Text;


namespace e_commerce.admin.Helper
{
    public class AccountHelpers
    {
        private readonly HttpClient _client;

        public AccountHelpers(HttpClient client)
        {
            _client = client;
        }

        #region Admin Login
        internal static async Task<dynamic> LoginAPI(LoginRequest loginRequest)
        {
            var ApiRequest = new LoginRequest
            {
                EmailId = loginRequest.EmailId,
                Password = loginRequest.Password,
                UniqueId = loginRequest.UniqueId,
            };


            //var url = $"{ApiEndPointConsts.Account.UserLogin}";
            var url = "https://localhost:7119/user/sign_in";
            var stringContent = new StringContent(JsonConvert.SerializeObject(ApiRequest), Encoding.Default, "application/json");
            var response = await Service.Service.PostAPIWithoutToken(url, stringContent);
            //var response = await Service.Post

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new Model.Const.CallAPIList() { meta = response.meta }
                : new Model.Const.CallAPI() { meta = response.meta, data = response.data };

        }
        #endregion 
    }
}
