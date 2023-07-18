namespace e_commerce.api.Service
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("Admin") && password.Equals("Password");
        }
    }
}
