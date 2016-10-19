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
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_uri);
				if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentException("Not set", nameof(resource));
				if (@object == null) throw new ArgumentNullException(nameof(@object));

				var jsonString = JsonConvert.SerializeObject(@object);
				var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
				var response = await client.PostAsync(resource, content);
				response.EnsureSuccessStatusCode();
			}
		}
	}
}
