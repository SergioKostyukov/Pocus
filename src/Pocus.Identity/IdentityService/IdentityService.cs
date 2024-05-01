using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pocus.Core.Entities;
using Pocus.Identity.Dto;
using Pocus.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pocus.Identity;

internal class IdentityService(ILogger<IdentityService> logger,
                               PocusDbContext context,
                               IMapper mapper,
                               IOptions<JwtSettings> jwtSettings,
                               IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    private readonly ILogger<IdentityService> _logger = logger;
    private readonly PocusDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task LoginUser(LoginDto request)
    {
        try
        {
            request.UserName = request.UserName.ToLower();

            var storedUser = await _context.Users
                        .Where(u => u.UserName == request.UserName)
                        .FirstOrDefaultAsync() ?? throw new Exception("User not found");

            if (BCrypt.Net.BCrypt.Verify(request.Password, storedUser.PasswordHash))
            {
                GenerateTokenKey(_mapper.Map<UserDto>(storedUser));

                //var settings = settingsService.GetSettings(storedUser.id);
                //var habits_id = tasksService.GetHabits(storedUser.id).id;

                _logger.LogInformation($"Login successful");
            }
            else
            {
                throw new Exception("Wrong password");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Login error: {ex.Message}");
        }
    }

    public async Task RegisterUser(RegisterDto request) // add model validation
    {
        try
        {
            // Hash the user's password
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            request.Email = request.Email.ToLower();
            request.UserName = request.UserName.ToLower();

            await _context.Users.AddAsync(_mapper.Map<User>(request));
            await _context.SaveChangesAsync();

            var storedUser = await _context.Users
                        .Where(u => u.UserName == request.UserName)
                        .FirstOrDefaultAsync() ?? throw new Exception("User not found");
            GenerateTokenKey(_mapper.Map<UserDto>(storedUser));

            // Set default params for new user
            //var currUserId = FindUserId(user.tag_name);
            //settingsService.SetDefaultSettings(currUserId);
            //tasksService.CreateDefaultTasks(currUserId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Authorization error: {ex.Message}");
            throw;
        }
    }

    private void GenerateTokenKey(UserDto user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.UserName),
            new ("notifications", user.Notifications.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var JWToken = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        var token = new JwtSecurityTokenHandler().WriteToken(JWToken);

        _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            IsEssential = true
        });

        _logger.LogInformation(token);
    }
}
