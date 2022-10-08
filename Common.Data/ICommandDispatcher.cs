using Common.Data.Models;
using Common.Data.Results;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface ICommandDispatcher
    {
        Task<ICommandResult<TExpectedResult>> DispatchAsync<TCommand, TExpectedResult>(TCommand command)
            where TCommand : ICommand
            where TExpectedResult : IResult;
    }
}
