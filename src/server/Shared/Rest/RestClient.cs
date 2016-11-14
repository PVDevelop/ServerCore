using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PVDevelop.UCoach.Rest
{
	public class RestClient
	{
		private readonly string _uri;

		public RestClient(string uri)
		{
			_uri = uri;
		}

		public async Task PostJsonAsync(string resource, object @object)
		{
			if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentException("Not set", nameof(resource));
			if (@object == null) throw new ArgumentNullException(nameof(@object));

			using (var client = PrepareHttpClient())
			{
				var content = CreateJsonContent(@object);
				var response = await client.PostAsync(resource, content);
				response.EnsureSuccessStatusCode();
			}
		}

		public async Task<TResult> PostJsonWithResultAsync<TResult>(string resource, object @object)
		{
			if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentException("Not set", nameof(resource));
			if (@object == null) throw new ArgumentNullException(nameof(@object));

			using (var client = PrepareHttpClient())
			{
				var content = CreateJsonContent(@object);
				var response = await client.PostAsync(resource, content);
				response.EnsureSuccessStatusCode();

				return await ReadJsonContentAsync<TResult>(response);
			}
		}

		private HttpClient PrepareHttpClient()
		{
			var client = new HttpClient { BaseAddress = new Uri(_uri) };
			return client;
		}

		private static StringContent CreateJsonContent(object @object)
		{
			var jsonString = JsonConvert.SerializeObject(@object);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			return content;
		}

		private static async Task<TResult> ReadJsonContentAsync<TResult>(HttpResponseMessage response)
		{
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TResult>(content);
		}
	}
}
