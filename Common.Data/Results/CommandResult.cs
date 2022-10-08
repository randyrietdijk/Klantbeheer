using FluentValidation.Results;

namespace Common.Data.Results
{
	public class CommandResult<T> : ICommandResult<T>
	{
		public T Data { get; set; }
		public ValidationResult ValidationResult { get; set; }

		public CommandResult(T data)
		{
			Data = data;
			ValidationResult = new ValidationResult();
		}

		public CommandResult(ValidationResult validationResult)
		{
			Data = default;
			ValidationResult = validationResult;
		}
	}
}
