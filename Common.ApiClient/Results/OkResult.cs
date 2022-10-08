using FluentValidation.Results;

namespace Common.ApiClient.Results
{
    public class OkResult<T> : IApiResult<T>
    {
        public bool IsOk => true;
        public ValidationResult ValidationResult => null;

        public T Data { get; set; }

        public OkResult(T data)
        {
            Data = data;
        }
    }
}
