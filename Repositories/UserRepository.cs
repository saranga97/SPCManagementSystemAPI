using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Data;
using SPCManagementSystemAPI.Models;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SPCContext _context;

        public UserRepository(SPCContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }
    }
}

