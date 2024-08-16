using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt {get; set; }
}
