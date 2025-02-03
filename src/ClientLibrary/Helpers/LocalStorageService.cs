using Blazored.LocalStorage;

namespace ClientLibrary.Helpers
{
    public class LocalStorageService(ILocalStorageService localStorageService)
    {
        private const string Storagekey = "authentication-token";
        public async Task<string> GetToken() => await localStorageService.GetItemAsStringAsync(Storagekey);
        public async Task SetToken(string token) => await localStorageService.SetItemAsync(Storagekey, token);
        public async Task RemoveToken() => await localStorageService.RemoveItemAsync(Storagekey);
    }
}
