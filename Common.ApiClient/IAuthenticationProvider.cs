using System.Threading.Tasks;

namespace Common.ApiClient
{
    public interface IAuthenticationProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}
