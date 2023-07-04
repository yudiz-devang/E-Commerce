using e_commerce.client.Model.Const;
using e_commerce.client.Model.Request;
using e_commerce.client.Service.Shared;
using Newtonsoft.Json;
using System.Text;

namespace e_commerce.client.Service
{
    public class AccountService
    {
        #region Login API
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }
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
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };

        }
        #endregion Login API
    }
}
