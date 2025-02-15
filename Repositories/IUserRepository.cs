using SPCManagementSystemAPI.Models;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> Login(string email, string password);
    }
}
