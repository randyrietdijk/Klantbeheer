using Common.ApiClient.Models;
using Common.ApiClient.Results;
using CustomerManagement.ApiClient.Models.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.ApiClient
{
    public interface ICustomerManagementApiClient
    {
        Task<IApiResult<List<CustomerListItem>>> ToListAsync();
        Task<IApiResult<CustomerVM>> SingleOrDefaultAsync(int id);

        Task<IApiResult<int>> CreateAsync(CustomerCreateForm model);
        Task<IApiResult<Empty>> UpdateAsync(int id, CustomerUpdateForm model);
        Task<IApiResult<Empty>> DeleteAsync(int id);
    }
}
