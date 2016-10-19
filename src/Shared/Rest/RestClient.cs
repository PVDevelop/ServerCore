using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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

		public async Task PostAsync(string resource, object @object)
		{
			var bytes = GetBytes(@object);
			var byteArrayContent = new ByteArrayContent(bytes);
			var client = new HttpClient()
			{
				BaseAddress = new Uri(_uri)
			};
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await client.PostAsync(resource, byteArrayContent);
			response.EnsureSuccessStatusCode();
		}

		private byte[] GetBytes(object @object)
		{
			using (var memoryStream = new MemoryStream())
			using (var streamWriter = new StreamWriter(memoryStream))
			{
				new JsonSerializer().Serialize(streamWriter, @object);
				return memoryStream.ToArray();
			}
		}
	}
}
