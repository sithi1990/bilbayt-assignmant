using Assignment.Domain.Models;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Data.Contracts
{
    public interface IAccountsDbService
    {
        Task CreateUserAsync(AppUser user);
        Task<AppUser> GetUserAsync(string userName);
    }
}
