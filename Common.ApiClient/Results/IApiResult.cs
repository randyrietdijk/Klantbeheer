using FluentValidation.Results;

namespace Common.ApiClient.Results
{
	public interface IApiResult<T>
	{
		bool IsOk { get; }

		T Data { get; }
		ValidationResult ValidationResult { get; }
	}
}
