namespace Golang_Assestment_Text.Interface.Service
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task RegisterUserAsync(string email, string password, bool isAdmin = false);
    }
}
