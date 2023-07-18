namespace e_commerce.api.Service
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);

    }
}
