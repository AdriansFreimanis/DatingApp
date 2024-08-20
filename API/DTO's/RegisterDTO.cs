using System.ComponentModel.DataAnnotations;

namespace API.DTO_s;

public class RegisterDTO
{
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
