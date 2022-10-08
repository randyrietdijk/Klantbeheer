using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Common.ApiClient.Results;
using FluentValidation.Results;
using System.Net.Mime;
using System.Text;
using Common.ApiClient.Models;

namespace Common.ApiClient
{
    public class ApiClient : IApiClient
	{
		private static readonly HttpClient HttpClient = new HttpClient();

		private readonly IAuthenticationProvider _authenticationProvider;

		public ApiClient(IAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		public async Task<IApiResult<T>> GetAsync<T>(string url)
		{
			string accessToken = await _authenticationProvider.GetAccessTokenAsync().ConfigureAwait(false);

			HttpRequestMessage message = BuildHttpRequestMessage(url, HttpMethod.Get, accessToken);
			HttpResponseMessage response = await HttpClient.SendAsync(message).ConfigureAwait(false);

			return await HandleResponseAsync<T>(response).ConfigureAwait(false);
		}

		public async Task<IApiResult<T>> PostAsync<TModel, T>(string url, TModel model)
		{
			string accessToken = await _authenticationProvider.GetAccessTokenAsync().ConfigureAwait(false);

			HttpRequestMessage message = BuildHttpRequestMessage(url, HttpMethod.Post, accessToken);
			StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);

			message.Content = content;

			HttpResponseMessage response = await HttpClient.SendAsync(message).ConfigureAwait(false);

			return await HandleResponseAsync<T>(response).ConfigureAwait(false);
		}

		public async Task<IApiResult<Empty>> PutAsync<TModel>(string url, TModel model)
		{
			string accessToken = await _authenticationProvider.GetAccessTokenAsync().ConfigureAwait(false);

			HttpRequestMessage message = BuildHttpRequestMessage(url, HttpMethod.Put, accessToken);
			StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);

			message.Content = content;

			HttpResponseMessage response = await HttpClient.SendAsync(message).ConfigureAwait(false);

			return await HandleResponseAsync<Empty>(response).ConfigureAwait(false);
		}

		public async Task<IApiResult<Empty>> DeleteAsync(string url)
		{
			string accessToken = await _authenticationProvider.GetAccessTokenAsync().ConfigureAwait(false);

			HttpRequestMessage message = BuildHttpRequestMessage(url, HttpMethod.Delete, accessToken);
			HttpResponseMessage response = await HttpClient.SendAsync(message).ConfigureAwait(false);

			return await HandleResponseAsync<Empty>(response).ConfigureAwait(false);
		}

		private async Task<IApiResult<T>> HandleResponseAsync<T>(HttpResponseMessage response)
		{
			string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				T okObject = JsonConvert.DeserializeObject<T>(responseContent);
				return new OkResult<T>(okObject);
			}
			else if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				ValidationResult validation = JsonConvert.DeserializeObject<ValidationResult>(responseContent);
				return new BadRequestResult<T>(validation);
			}

			return null;
		}

		private HttpRequestMessage BuildHttpRequestMessage(string url, HttpMethod method, string token)
		{
			var message = new HttpRequestMessage();

			message.RequestUri = new Uri(url);
			message.Method = method;

			// Add the bearer token
			message.Headers.Add("Authorization", $"Bearer {token}");

			return message;
		}
	}
}
