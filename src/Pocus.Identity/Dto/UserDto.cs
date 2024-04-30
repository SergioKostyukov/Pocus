namespace Pocus.Identity.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Notifications { get; set; }
}