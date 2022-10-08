using AutoMapper;
using Common.ApiClient.Models;
using Common.ApiClient.Results;
using CustomerManagement.ApiClient;
using CustomerManagement.ApiClient.Models.Customers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.UI.Controllers
{
    public class CustomersController : Controller
	{
		private readonly IMapper _mapper;
		private readonly ICustomerManagementApiClient _apiClient;

		public CustomersController(IMapper mapper, ICustomerManagementApiClient apiClient)
		{
			_mapper = mapper;
			_apiClient = apiClient;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IApiResult<List<CustomerListItem>> customers = await _apiClient.ToListAsync();

			if (customers.IsOk)
			{
				ViewBag.Customers = customers.Data;
				return View();
			}

			return View("Error");
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CustomerCreateForm model)
		{
			IApiResult<int> result = await _apiClient.CreateAsync(model);
			if(result.IsOk)
			{
				return RedirectToAction(nameof(Index));
			}

			result.ValidationResult.AddToModelState(ModelState);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit([FromRoute] int id)
		{
			IApiResult<CustomerVM> result = await _apiClient.SingleOrDefaultAsync(id);
			if (result.IsOk)
			{
				CustomerUpdateForm model = _mapper.Map<CustomerUpdateForm>(result.Data);
				return View(model);
			}

			return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] CustomerUpdateForm model)
		{
			IApiResult<Empty> result = await _apiClient.UpdateAsync(id, model);
			if (result.IsOk)
			{
				return RedirectToAction(nameof(Index));
			}

			result.ValidationResult.AddToModelState(ModelState);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			IApiResult<CustomerVM> result = await _apiClient.SingleOrDefaultAsync(id);
			if (result.IsOk)
			{
				ViewBag.Customer = result.Data;
				return View();
			}

			return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
		{
			IApiResult<Empty> result = await _apiClient.DeleteAsync(id);
			if (result.IsOk)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Delete), new
			{
				Id = id
			});
		}
	}
}
