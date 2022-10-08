using AutoMapper;
using Common.Data;
using CustomerManagement.API.Domain.Models;
using CustomerManagement.API.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
	[Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
		private readonly IMapper _mapper;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;

		public CustomersController(IMapper mapper, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
		{
			_mapper = mapper;
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		[HttpGet]
		[Route("")]
		[ProducesResponseType(typeof(List<CustomerListItem>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Get()
		{
			var result = await _queryDispatcher.DispatchAsync<GetCustomerListQuery, GetCustomerListQueryResult>(new GetCustomerListQuery());

			return Ok(result.Data.Customers);
		}

		[HttpGet]
		[Route("{id:int}")]
		[ProducesResponseType(typeof(CustomerVM), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Get([FromRoute] int id)
		{
			var query = new GetCustomerQuery()
			{
				Id = id
			};

			var result = await _queryDispatcher.DispatchAsync<GetCustomerQuery, GetCustomerQueryResult>(query);

			return Ok(result.Data.Customer);
		}

		[HttpPost]
		[Route("")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Create([FromBody] CustomerCreateForm model)
		{
			if (model == null)
				return BadRequest();

			var command = _mapper.Map<CustomerCreateForm, CreateCustomerCommand>(model);

			var result = await _commandDispatcher.DispatchAsync<CreateCustomerCommand, CreateCustomerCommandResult>(command);

			if(!result.ValidationResult.IsValid)
			{
				return BadRequest(result.ValidationResult);
			}

			return Ok(result.Data.Id);
		}

		[HttpPut]
		[Route("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateForm model)
		{
			if (model == null)
				return BadRequest();

			var command = _mapper.Map<CustomerUpdateForm, UpdateCustomerCommand>(model);
			command.Id = id;

			var result = await _commandDispatcher.DispatchAsync<UpdateCustomerCommand, UpdateCustomerCommandResult>(command);

			if (!result.ValidationResult.IsValid)
			{
				return BadRequest(result.ValidationResult);
			}

			return NoContent();
		}

		[HttpDelete]
		[Route("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var command = new DeleteCustomerCommand
			{
				Id = id
			};

			var result = await _commandDispatcher.DispatchAsync<DeleteCustomerCommand, DeleteCustomerCommandResult>(command);

			if (!result.ValidationResult.IsValid)
			{
				return BadRequest(result.ValidationResult);
			}

			return NoContent();
		}
	}
}
