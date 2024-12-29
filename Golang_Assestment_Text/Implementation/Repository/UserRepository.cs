using Golang_Assestment_Text.Interface.Repository;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ECommerceDbContext _context;

    public UserRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}