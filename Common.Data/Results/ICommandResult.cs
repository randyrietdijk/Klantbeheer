using FluentValidation.Results;

namespace Common.Data.Results
{
	public interface ICommandResult<T>
	{
		ValidationResult ValidationResult { get; }
		T Data { get; }
	}
}
