﻿using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SignInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        //Task<WeatherForecast[]> GetWeatherForecast();
        Task<List<ManageUser>> GetUsers();
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<GeneralResponse> DeleteUser(int id);
    }
}
