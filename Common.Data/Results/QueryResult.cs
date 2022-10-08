namespace Common.Data.Results
{
	public class QueryResult<T> : IQueryResult<T>
	{
		public T Data { get; set; }

		public QueryResult(T data)
		{
			Data = data;
		}
	}
}
