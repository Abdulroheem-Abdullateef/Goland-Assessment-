using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    private readonly List<User> _users = new();

    public string Register(User user)
    {
        // Check if the user already exists
        if (_users.Any(u => u.Email == user.Email))
        {
            return "User already exists.";
        }

        // Hash the password
        user.PasswordHash = HashPassword(user.PasswordHash);
        _users.Add(user);

        return "User registered successfully.";
    }

    public string Login(string email, string password)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        if (user == null || user.PasswordHash != HashPassword(password))
        {
            return "Invalid email or password.";
        }

        // Generate a JWT (simplified for example)
        return "YourJWTToken";
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}