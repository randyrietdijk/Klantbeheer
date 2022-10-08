using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
	public interface IQueryDispatcher
	{
		Task<IQueryResult<TExpectedResult>> DispatchAsync<TQuery, TExpectedResult>(TQuery query)
			 where TQuery : IQuery
			 where TExpectedResult : IResult;
	}
}
