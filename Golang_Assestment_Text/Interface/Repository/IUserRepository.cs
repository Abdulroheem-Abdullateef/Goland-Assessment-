namespace Golang_Assestment_Text.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
    }
}
