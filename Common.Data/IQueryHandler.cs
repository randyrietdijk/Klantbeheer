using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface IQueryHandler<in TQuery, TExpectedResult>
        where TQuery : IQuery
        where TExpectedResult : IResult
    {
        Task<IQueryResult<TExpectedResult>> RetrieveAsync(TQuery query);
    }
}
