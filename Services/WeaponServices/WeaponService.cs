using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Dtos.Weapon;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.WeaponServices;

public class WeaponService : IWeaponService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character = await _context.Characters.Include(c => c.Weapon).FirstOrDefaultAsync(c =>
                c.Id == newWeapon.CharacterId && c.User!.Id ==
                int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            if (character is null)
            {
                response.Success = false;
                response.Message = "Not found";
                return response;
            }
            //Check if u can do that with mapper
            var weapon = _mapper.Map<Weapon>(newWeapon);
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterDto>(character);
        }
        catch(Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }
}