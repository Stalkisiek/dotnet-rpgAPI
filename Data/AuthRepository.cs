using System.Data.Entity;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Data;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;
    public AuthRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var serviceResponse = new ServiceResponse<int>();
        if (!await UserExists(user.Username))
        {
            CreatePassowrdHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = user.Id;
            return serviceResponse;
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User already exists!";
            return serviceResponse;
        }
    }

    public Task<ServiceResponse<string>> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(c => c.Username.ToLower() == username.ToLower()) != null;
    }

    private void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}