using FluentValidation.Results;

namespace Common.ApiClient.Results
{
	public class BadRequestResult<T> : IApiResult<T>
	{
		public bool IsOk => false;
		public T Data => default;

		public ValidationResult ValidationResult { get; set; }

		public BadRequestResult(ValidationResult validationResult)
		{
			ValidationResult = validationResult;
		}
	}
}
