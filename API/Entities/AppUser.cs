using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace API.Entities;

public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public  byte[] PasswordHash { get; set; } = [];
    public  byte[] PasswordSalt {get; set; } = [];
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
    public List<Photo> Photos { get; set; } = [];

    public int GetAge(){
        return DateOfBirth.CalculateAge();
    }
}
