using Golang_Assestment_Text.Interface.Repository;
using Golang_Assestment_Text.Interface.Service;
using Org.BouncyCastle.Crypto.Generators;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");
        return user;
    }

    public async Task RegisterUserAsync(string email, string password, bool isAdmin = false)
    {
        if (await _userRepository.GetUserByEmailAsync(email) != null)
            throw new ArgumentException("Email is already registered.");

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            IsAdmin = isAdmin
        };

        await _userRepository.AddUserAsync(user);
    }



}