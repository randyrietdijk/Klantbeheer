using Common.ApiClient;
using Common.ApiClient.Models;
using Common.ApiClient.Results;
using CustomerManagement.ApiClient.Constants;
using CustomerManagement.ApiClient.Models.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.ApiClient
{
    public class CustomerManagementApiClient : ICustomerManagementApiClient
	{
		private readonly IApiClient _apiClient;

		public CustomerManagementApiClient(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public async Task<IApiResult<CustomerVM>> SingleOrDefaultAsync(int id)
		{
			string url = string.Format(Endpoints.Customers.Get, id);
			return await _apiClient.GetAsync<CustomerVM>(url).ConfigureAwait(false);
		}

		public async Task<IApiResult<List<CustomerListItem>>> ToListAsync()
		{
			string url = Endpoints.Customers.List;
			return await _apiClient.GetAsync<List<CustomerListItem>>(url).ConfigureAwait(false);
		}

		public async Task<IApiResult<int>> CreateAsync(CustomerCreateForm model)
		{
			string url = Endpoints.Customers.Post;
			return await _apiClient.PostAsync<CustomerCreateForm, int>(url, model).ConfigureAwait(false);
		}

		public async Task<IApiResult<Empty>> UpdateAsync(int id, CustomerUpdateForm model)
		{
			string url = string.Format(Endpoints.Customers.Put, id);
			return await _apiClient.PutAsync(url, model).ConfigureAwait(false);
		}

		public async Task<IApiResult<Empty>> DeleteAsync(int id)
		{
			string url = string.Format(Endpoints.Customers.Delete, id);
			return await _apiClient.DeleteAsync(url).ConfigureAwait(false);
		}
	}
}
