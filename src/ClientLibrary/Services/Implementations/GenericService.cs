using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class GenericService<T>(GetHttpClient getHttpClient) : IGenericService<T>
    {
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var reponse = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await reponse.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
            return result!;
        }
        public async Task<T> GetById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return result!;
        }
        public async Task<GeneralResponse> Insert(T entity, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var reponse = await httpClient.PostAsJsonAsync($"{baseUrl}/add", entity);
            var result = await reponse.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
        public async Task<GeneralResponse> Update(T entity, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var reponse = await httpClient.PutAsJsonAsync($"{baseUrl}/update", entity);
            var result = await reponse.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
