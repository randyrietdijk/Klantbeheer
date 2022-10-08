using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface ICommandHandler<in TCommand, TExpectedResult>
        where TCommand : ICommand
        where TExpectedResult : IResult
    {
        Task<ICommandResult<TExpectedResult>> ExecuteAsync(TCommand command);
    }
}
