using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user) 
        {
            if (user is null)
            {
                return new GeneralResponse(false, "User is null");
            }

            var checkUser = await FindUserByEmail(user.Email);

            if (checkUser is not null)
            {
                return new GeneralResponse(false, "User already registered");
            }

            // Save user to database
            var newUser = await AddToDatabase(new User
            {
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Name = user.FullName,
                CreatedAt = DateTime.Now
            });

            // check, ceate and assign role to user
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.ToLower()!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole
                {
                    Name = Helpers.Constants.Admin,
                    CreatedAt = DateTime.Now
                });

                await AddToDatabase(new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = createAdminRole.Id
                });
                return new GeneralResponse(true, "User created successfully");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.ToLower()!.Equals(Constants.User));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDatabase(new SystemRole
                {
                    Name = Helpers.Constants.User,
                    CreatedAt = DateTime.Now
                });
                await AddToDatabase(new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = response.Id
                });              
            }
            else
            { 
             await AddToDatabase(new UserRole
             {
                 UserId = newUser.Id,
                 RoleId = checkUserRole.Id
             });
            }
            return new GeneralResponse(true, "User created successfully");
        }

        private async Task<T> AddToDatabase<T>(T user)
        {
            var result = appDbContext.Add(user!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await appDbContext.Users.FirstOrDefaultAsync(x => x.Email!.ToLower()!.Equals(email!.ToLower()));
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null)
            {
                return new LoginResponse(false, "Model is empty");
            }

            var applicationUser = await FindUserByEmail(user.Email!);

            if (applicationUser is null)
            {
                return new LoginResponse(false, "User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
            {
                return new LoginResponse(false, "E-mail/Password not valid");
            }

            var getUserRole = await FindUserRole(applicationUser.Id);
            if (getUserRole is null)
            {
                return new LoginResponse(false, "User role not found");
            }

            var getRoleName = await FindRoleName(getUserRole.RoleId);
            if (getRoleName is null)
            {
                return new LoginResponse(false, "User role not found");
            }

            string jwtToken = GenerateToken(applicationUser, getRoleName.Name);
            string refreshToken = GenerateRefreshToken();

            // save the refresh token to the database
            var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);
            if (findUser is not null)
            {
                findUser!.Token = refreshToken;
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refreshToken, UserId = applicationUser.Id });            
            }

            return new LoginResponse(true, "Logging Sucessfully", jwtToken, refreshToken);
        }

        private async Task<UserRole> FindUserRole(int userId) => await appDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        private async Task<SystemRole> FindRoleName(int roleId) => await appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Id == roleId);

        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private string GenerateToken(User applicationUser, string role) 
        { 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()),
             new Claim(ClaimTypes.Name, applicationUser.Name),
             new Claim(ClaimTypes.Email, applicationUser.Email),
             new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null)
            {
                return new LoginResponse(false, "Model is empty");
            }

            var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Token == token.Token);
            if (findToken is null)
            {
                return new LoginResponse(false, "Refresh token is required!");
            }

            // get user details
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == findToken.UserId);
            if (user is null)
            {
                return new LoginResponse(false, "Refresh token could not be generated because user not found");
            }

            // get user role
            var userRole = await FindUserRole(user.Id);

            // get role name
            var roleName = await FindRoleName(userRole.RoleId);

            string jwtToken = GenerateToken(user, roleName.Name);
            string refreshToken = GenerateRefreshToken();

            var updateRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (updateRefreshToken is null)
            {
                return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");
            }

            updateRefreshToken.Token = refreshToken;

            await appDbContext.SaveChangesAsync();

            return new LoginResponse(true, "Token refreshed successfully", jwtToken, refreshToken);
        }
    }
}
