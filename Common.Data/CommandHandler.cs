using Autofac;
using Common.Data.Models;
using Common.Data.Results;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common.Data
{
	public abstract class CommandHandler<TCommand, TExpectedResult> : ICommandHandler<TCommand, TExpectedResult>
			where TCommand : ICommand
			where TExpectedResult : IResult
	{
		private readonly ILogger<ICommandHandler<TCommand, TExpectedResult>> _logger;

		private readonly IComponentContext _componentContext;
		private readonly IValidator<TCommand> _validator;

		protected CommandHandler(ILogger<ICommandHandler<TCommand, TExpectedResult>> logger, IComponentContext componentContext)
		{
			_logger = logger;
			_componentContext = componentContext;

			_validator = _componentContext.Resolve<IValidator<TCommand>>();
		}

		public async Task<ICommandResult<TExpectedResult>> ExecuteAsync(TCommand command)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			try
			{
				var validationResult = await _validator.ValidateAsync(command).ConfigureAwait(false);

				if (!validationResult.IsValid)
				{
					return new CommandResult<TExpectedResult>(validationResult);
				}

				return new CommandResult<TExpectedResult>(await HandleAsync(command).ConfigureAwait(false));
			}
			catch (Exception exception)
			{
				_logger.LogError("Error in {0} CommandHandler. {1}", typeof(TCommand).Name, exception.ToString());

				throw;
			}
			finally
			{
				stopWatch.Stop();
				_logger.LogDebug("Response for command {0} served (elapsed time: {1} msec)", typeof(TCommand).Name, stopWatch.ElapsedMilliseconds);
			}
		}

		protected abstract Task<TExpectedResult> HandleAsync(TCommand command);
	}
}
