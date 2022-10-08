using Autofac;
using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly IComponentContext _componentContext;

		public CommandDispatcher(IComponentContext componentContext)
		{
			_componentContext = componentContext;
		}

		public async Task<ICommandResult<TExpectedResult>> DispatchAsync<TCommand, TExpectedResult>(TCommand command)
			where TCommand : ICommand
			where TExpectedResult : IResult
		{
			var handler = _componentContext.Resolve<ICommandHandler<TCommand, TExpectedResult>>();
			return await handler.ExecuteAsync(command).ConfigureAwait(false);
		}
	}
}
