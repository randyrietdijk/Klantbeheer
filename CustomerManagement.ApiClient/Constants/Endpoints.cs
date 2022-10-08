namespace CustomerManagement.ApiClient.Constants
{
    public static class Endpoints
    {
        public static class Customers
        {
            public const string List = "https://localhost:4001/api/v1/customers";
            public const string Get = "https://localhost:4001/api/v1/customers/{0}";
            public const string Post = "https://localhost:4001/api/v1/customers";
            public const string Put = "https://localhost:4001/api/v1/customers/{0}";
			public const string Delete = "https://localhost:4001/api/v1/customers/{0}";
		}
    }
}
