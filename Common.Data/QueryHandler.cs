using Common.Data.Models;
using Common.Data.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common.Data
{
	public abstract class QueryHandler<TQuery, TExpectedResult> : IQueryHandler<TQuery, TExpectedResult>
			where TQuery : IQuery
			where TExpectedResult : IResult
	{
		private readonly ILogger<IQueryHandler<TQuery, TExpectedResult>> _logger;

		protected QueryHandler(ILogger<IQueryHandler<TQuery, TExpectedResult>> logger)
		{
			_logger = logger;
		}

		public async Task<IQueryResult<TExpectedResult>> RetrieveAsync(TQuery query)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			try
			{
				return new QueryResult<TExpectedResult>(await HandleAsync(query).ConfigureAwait(false));
			}
			catch (Exception exception)
			{
				_logger.LogError("Error in {0} QueryHandler. {1}", typeof(TQuery).Name, exception.ToString());

				throw;
			}
			finally
			{
				stopWatch.Stop();
				_logger.LogDebug("Response for query {0} served (elapsed time: {1} msec)", typeof(TQuery).Name, stopWatch.ElapsedMilliseconds);
			}
		}

		protected abstract Task<TExpectedResult> HandleAsync(TQuery query);
	}
}
