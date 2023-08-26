using dotnet_rpg2.Dtos;
using dotnet_rpg2.Dtos.Weapon;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.WeaponServices;

public interface IWeaponService
{
    Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
}