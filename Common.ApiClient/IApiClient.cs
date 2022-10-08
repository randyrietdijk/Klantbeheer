using Common.ApiClient.Models;
using Common.ApiClient.Results;
using System.Threading.Tasks;

namespace Common.ApiClient
{
    public interface IApiClient
    {
		Task<IApiResult<T>> GetAsync<T>(string url);
		Task<IApiResult<T>> PostAsync<TModel, T>(string url, TModel model);
		Task<IApiResult<Empty>> PutAsync<TModel>(string url, TModel model);
		Task<IApiResult<Empty>> DeleteAsync(string url);
	}
}
