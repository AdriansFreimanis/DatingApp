using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Extensions;

namespace API.Entities;

public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public  byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public  byte[] PasswordSalt {get; set; } = Array.Empty<byte>();
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public string? KnownAs { get; set; } 
    public DateTime Created { get; set; }= DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = new List<Photo>();

    public int GetAge(){
        return DateOfBirth.CalculateAge();
    }
}
