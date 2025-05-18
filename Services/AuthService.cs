using Base_API.DTO;
using Base_API.Entities;
using Base_API.Infrastructure.Context;
using Base_API.Infrastructure.Exceptions;
using Base_API.Infrastructure.Extensions;
using Base_API.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Base_API.Services
{
    public class AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, MainDbContext db, IUserRepository userRepository)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly MainDbContext _db = db;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var userWithEmailExists = await _userRepository.ExistsByEmailAsync(dto.Email);

            if (userWithEmailExists)
            {
                throw new BusinessException($"The e-mail {dto.Email} is already in use.", HttpStatusCode.Conflict);
            }

            var user = User.FromRegisterDto(dto);

            await _db.ExecuteInTrasactionAsync(async () =>
            {
                await _userRepository.AddAsync(user);
            });

            return UserDto.FromUser(user);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email) ?? throw new BusinessException("Invalid e-mail/password.", HttpStatusCode.NotFound);

            var correctPassword = dto.Password.VerifyPassword(user.PasswordHash, user.PasswordSaltHash);

            if (!correctPassword)
            {
                throw new BusinessException("Invalid e-mail/password.", HttpStatusCode.NotFound);
            }

            var bearerToken = GenerateBearerToken(user);

            return bearerToken;
        }

        private string GenerateBearerToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:BearerToken"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }

        public string? GetLoggedUserEmail()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

            return email;
        }

        public async Task<User?> GetLoggedUser()
        {
            var email = GetLoggedUserEmail() ?? String.Empty;
            var user = await _userRepository.GetByEmailAsync(email);

            return user;
        }
    }
}
