using BaseLibrary.DTOs;
using Newtonsoft.Json;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
    {
        private const string HeaderKey = "Authorization";

        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return client;

            // TODO : Refactor this conversion to a helper method or find a better way
            // Deserialize the JSON string to a dynamic object
            JsonReader reader = new JsonTextReader(new StringReader(stringToken));
            var jsonSerializer = JsonSerializer.Create();
            var jsonObject = jsonSerializer.Deserialize<dynamic>(reader);

            stringToken = jsonObject!.ToString();

            var desarializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (desarializedToken is null) return client;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", desarializedToken.Token);
            
            return client;
        }

        public HttpClient GetPublicHttpClient()
        { 
            var client = httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}
