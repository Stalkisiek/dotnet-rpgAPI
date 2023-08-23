using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterServices;

public class CharacterService : ICharacterService
{
    

    private static List<Character> _characters = new List<Character>
    {
        new Character(),
        new Character(){Name="StarWars",Profession = Job.Medic, Id = 1}
    };
    
    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(_characters.FirstOrDefault(c => c.Id == id));
        if (serviceResponse.Data == null)
        {
            serviceResponse.Message = "Character not found\n";
            serviceResponse.Success = false;
        }
        return serviceResponse;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddOne(AddCharacterDto newCharacter)
    {
        Character character = _mapper.Map<Character>(newCharacter);
        character.Id = _characters.Max(c => c.Id) + 1;
        _characters.Add(character);
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateOne(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        if (_characters.FirstOrDefault(c => c.Id == updatedCharacter.Id) == null)
        {
            serviceResponse.Message = "Character not found";
            serviceResponse.Success = false;
        }
        _characters.FirstOrDefault(c => c.Id == updatedCharacter.Id).Name = updatedCharacter.Name;
        _characters.FirstOrDefault(c => c.Id == updatedCharacter.Id).Profession = updatedCharacter.Profession;
        
        serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_characters);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteById(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        if (_characters.FirstOrDefault(c => c.Id == id) == null)
        {
            serviceResponse.Message = "Character not found";
            serviceResponse.Success = false;
        }
        Character character = _characters.FirstOrDefault(c => c.Id == id);
        _characters.Remove(character);
        serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_characters);
        return serviceResponse;
    }
}