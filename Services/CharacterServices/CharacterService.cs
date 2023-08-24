using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterServices;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
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
        
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        serviceResponse.Data =
            await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateOne(UpdateCharacterDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        if (character == null)
        {
            serviceResponse.Message = "Character not found";
            serviceResponse.Success = false;
        }
        else
        {
            character.Name = updatedCharacter.Name;
            character.Profession = updatedCharacter.Profession;
            character.Hp = updatedCharacter.Hp;
            await _context.SaveChangesAsync();
        }

        serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteById(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        Character? character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        if (character == null)
        {
            serviceResponse.Message = "Character not found";
            serviceResponse.Success = false;
        }
        else
        {
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }
        serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }
}