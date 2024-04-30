using Pocus.Identity.Dto;

namespace Pocus.Identity;

public interface IIdentityService
{
    Task LoginUser(LoginDto request);
    Task RegisterUser(RegisterDto request);
}
