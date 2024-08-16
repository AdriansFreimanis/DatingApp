using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO_s;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext _context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
    {
        if (await UserExists(registerDto.UserName))
    {
        return BadRequest("Username is taken");
    }
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.UserName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid username");

        //Pass the Password salt to hash it 
        using var hmac = new HMACSHA512(user.PasswordSalt);
        // Compute the Password hash
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

    //Run a for loop to look if the computed hash(from PasswordSalt) mathes the Password Hash
        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return new UserDTO
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
