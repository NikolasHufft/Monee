using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibrary.Helpers
{
    public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymmous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await localStorageService.GetToken();

            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonymmous));

            // Deserialize the JSON string to a dynamic object
            JsonReader reader = new JsonTextReader(new StringReader(stringToken));
            var jsonSerializer = JsonSerializer.Create();
            var jsonObject = jsonSerializer.Deserialize<dynamic>(reader);

            stringToken = jsonObject!.ToString();

            var deserializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserializedToken == null) return await Task.FromResult(new AuthenticationState(anonymmous));

            var getUserClaims = DecryptToken(deserializedToken.Token);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymmous));

            var claimsPrincipal = SetClaimsPrincipal(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (userSession.Token is not null || userSession.RefreshToken is not null)
            {
                var serializeSession = Serializations.SerializeObj(userSession);
                await localStorageService.SetToken(serializeSession);
                var getUserClaims = DecryptToken(userSession.Token);
                if (getUserClaims is not null)
                {
                    claimsPrincipal = SetClaimsPrincipal(getUserClaims);
                }
            }
            else 
            {
                await localStorageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private static CustomUserClaims DecryptToken(string? jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userId = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            var email = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            var name = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            var role = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            return new CustomUserClaims
            {
                Id = userId!.Value,
                Email = email!.Value,
                Name = name!.Value,
                Role = role!.Value
            };  
        }

        private static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claims.Id),
                new Claim(ClaimTypes.Email, claims.Email),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Role, claims.Role)
            }, "JwtAuth"));
        }
    }
}
