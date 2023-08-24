
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
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
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

    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        var response = new ServiceResponse<string>();
        if (await UserExists(username))
        {
            User user = (await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower()))!;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Message = "Wrong password";
                response.Success = false;
            }
            else
            {
                response.Data = user.Id.ToString();
            }

        }
        else
        {
            response.Message = "Not found";
            response.Success = false;
        }

        return response;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(c => c.Username.ToLower() == username.ToLower());
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwardSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwardSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}