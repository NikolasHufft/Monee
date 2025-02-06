
using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using ClientLibrary.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace ClientLibrary.Helpers
{
    public class CustomHttpHandler(GetHttpClient getHttpClient, LocalStorageService localStorageService, 
        IUserAccountService userAccountService) : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        { 
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains("refresh-token");
            bool registerUrl = request.RequestUri.AbsoluteUri.Contains("register");

            if (loginUrl || refreshTokenUrl || registerUrl) return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Get token from local storage
                var stringToken = await localStorageService.GetToken();

                if (string.IsNullOrEmpty(stringToken)) return result;

                // Deserialize the JSON string to a dynamic object
                JsonReader reader = new JsonTextReader(new StringReader(stringToken));
                var jsonSerializer = JsonSerializer.Create();
                var jsonObject = jsonSerializer.Deserialize<dynamic>(reader);

                stringToken = jsonObject!.ToString();

                // Check if the header contains the token
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch (Exception ex)
                {
                    var error = ex;
                }

                var deserializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (deserializedToken is null) return result;

                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                // Call the refresh token endpoint
                var newJwtToken = await GetRefreshToken(deserializedToken.RefreshToken!);

                if (string.IsNullOrEmpty(newJwtToken))
                {
                    return result;
                }
                    
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);

                result = await base.SendAsync(request, cancellationToken);
            }

            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        { 
            LoginResponse result = await userAccountService.RefreshTokenAsync(new RefreshToken() { Token = refreshToken });
            string serializedToken = Serializations.SerializeObj(new UserSession() { Token = result.Token, RefreshToken = result.RefreshToken });
            await localStorageService.SetToken(serializedToken);

            return result.Token;
        }
    }
}
