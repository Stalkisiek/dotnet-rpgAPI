using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Dtos.Weapon;
using dotnet_rpg2.Models;

namespace dotnet_rpg2;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<GetCharacterDto, Character>();
        CreateMap<Character, GetCharacterDto>();
        CreateMap<Character, AddCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<AddWeaponDto, Weapon>();
        CreateMap<Weapon, AddWeaponDto>();
        CreateMap<Weapon, GetWeaponDto>();
    }
}