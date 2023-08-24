using dotnet_rpg2.Models;

namespace dotnet_rpg2.Data;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string username, string password); //Json Web token
    Task<bool> UserExists(string username);
}