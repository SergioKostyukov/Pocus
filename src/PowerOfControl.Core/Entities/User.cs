namespace Pocus.Core.Entities;

public class User
{
	public int Id { get; set; }
	public string Tag { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public bool Notifications { get; set; } = false;
}
