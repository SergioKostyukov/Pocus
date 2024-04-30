using System.ComponentModel.DataAnnotations;

namespace Pocus.WebUI.Models;

public class LoginModel
{
    [Required]
    [Display(Name = "Tag")]
    public string Tag { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
}
