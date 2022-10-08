namespace Common.Data.Results
{
	public interface IQueryResult<T>
	{
		T Data { get; }
	}
}
