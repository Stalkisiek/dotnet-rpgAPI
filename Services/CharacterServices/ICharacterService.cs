using dotnet_rpg2.Dtos;
using dotnet_rpg2.Dtos.Weapon;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterServices;

public interface ICharacterService
{ 
    Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
    Task<ServiceResponse<GetCharacterDto>> GetById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddOne(AddCharacterDto newCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> UpdateOne(UpdateCharacterDto updatedCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteById(int id);

    Task<ServiceResponse<GetCharacterDto>> AddSkill(AddCharacterSkillDto newSkill);
}