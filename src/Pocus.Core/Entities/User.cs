using Microsoft.AspNetCore.Identity;

namespace Pocus.Core.Entities;

public class User : IdentityUser
{
    public bool Notifications { get; set; } = false;
}
