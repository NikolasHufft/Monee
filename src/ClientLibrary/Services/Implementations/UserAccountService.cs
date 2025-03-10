﻿using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthUrl = "api/Authentication";
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);

            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occurred");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public async Task<LoginResponse> SignInAsync(Login user) 
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);

            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred");

            return await result.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token) 
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);

            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred");

            return await result.Content.ReadFromJsonAsync<LoginResponse>();
        }

        //public async Task<WeatherForecast[]> GetWeatherForecast() 
        //{
        //    var httpClient = await getHttpClient.GetPrivateHttpClient();
        //    var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast");
        //    return result!;
        //}

        public async Task<List<ManageUser>> GetUsers()
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ManageUser>>($"{AuthUrl}/users");
            
            return result!;
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
            return result!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/update-user", user);

            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occurred");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");

            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occurred");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
    }
}
