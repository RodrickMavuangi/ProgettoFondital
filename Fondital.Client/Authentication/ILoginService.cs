using System.Threading.Tasks;

namespace Fondital.Client.Authentication
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}