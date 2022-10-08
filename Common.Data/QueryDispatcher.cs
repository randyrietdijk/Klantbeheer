using Autofac;
using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
	public class QueryDispatcher : IQueryDispatcher
	{
		private readonly IComponentContext _componentContext;

		public QueryDispatcher(IComponentContext componentContext)
		{
			_componentContext = componentContext;
		}

		public async Task<IQueryResult<TExpectedResult>> DispatchAsync<TQuery, TExpectedResult>(TQuery query)
			where TQuery : IQuery
			where TExpectedResult : IResult
		{
			var handler = _componentContext.Resolve<IQueryHandler<TQuery, TExpectedResult>>();
			return await handler.RetrieveAsync(query).ConfigureAwait(false);
		}
	}
}
