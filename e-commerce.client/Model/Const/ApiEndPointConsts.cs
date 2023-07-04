namespace e_commerce.client.Model.Const
{
    public class ApiEndPointConsts
    {
        const string Version = "v1/";

        public const string BaseRoute = "https://localhost:7119/" + Version;
        public const string BaseRouteWithoutVersion = "https://localhost:7119/";

        public class Account
        {
            public const string UserLogin = BaseRoute + "user/sign_in";

            public const string UserSignUp = BaseRoute + "user/sign_up";

        }
    }
}
