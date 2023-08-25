using System.Security.Claims;
using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterServices;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        Character? dbCharacter = await _context.Characters.Include(character => character.User!).FirstOrDefaultAsync(c => c.Id == id);
        if (dbCharacter == null)
        {
            serviceResponse.Message = "Character not found\n";
            serviceResponse.Success = false;
            return serviceResponse;
        }
        if (dbCharacter.User!.Id != GetUserId())
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "No permissions";
            return serviceResponse;
        }
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddOne(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        Character character = _mapper.Map<Character>(newCharacter);
        character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
        
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        
        serviceResponse.Data =
            await _context.Characters.Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
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
        Character? character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
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
        serviceResponse.Data = await _context.Characters.Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }
}