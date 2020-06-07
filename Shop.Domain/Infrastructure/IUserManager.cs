using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task CreateManagerUser(string username, string password);
    }
}