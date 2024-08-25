using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
public static async Task SeedUsers(DataContext context){
    if (await context.Users.AnyAsync()) 
    {
        Console.WriteLine("Users table already contains data, skipping seeding.");
        return;
    }

    Console.WriteLine("Seeding users...");

    var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

    var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

    var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

    if(users == null) return;

    foreach (var user in users)
    {
        using var hmac = new HMACSHA512();

        user.UserName = user.UserName.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
        user.PasswordSalt = hmac.Key;
        user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);
        user.Created = DateTime.SpecifyKind(user.Created, DateTimeKind.Utc);
        user.LastActive = DateTime.SpecifyKind(user.LastActive, DateTimeKind.Utc);

        context.Users.Add(user);
    }

    await context.SaveChangesAsync();
}
}
