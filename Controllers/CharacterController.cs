using System.Runtime.CompilerServices;
using System.Security.Claims;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;
using dotnet_rpg2.Services.CharacterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg2.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        return Ok(await _characterService.GetAll(userId));
    }

    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        serviceResponse = await _characterService.GetById(id);
        if (serviceResponse.Success == false)
        {
            return NotFound(serviceResponse);
        }
        return Ok(serviceResponse);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddOne(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddOne(newCharacter));
    }
    
    [HttpPut("UpdateCharacter")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateOne(UpdateCharacterDto updatedCharacter)
    {
        var servicerResponse = new ServiceResponse<List<GetCharacterDto>>();
        servicerResponse = await _characterService.UpdateOne(updatedCharacter);
        if (servicerResponse.Success == false)
        {
            return NotFound(servicerResponse);
        }

        return Ok(servicerResponse);
    }
    
    [HttpDelete("DeleteCharacter")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteById(int id)
    {
        var servicerResponse = new ServiceResponse<List<GetCharacterDto>>();
        servicerResponse = await _characterService.DeleteById(id);
        if (servicerResponse.Success == false)
        {
            return NotFound(servicerResponse);
        }

        return Ok(servicerResponse);
    }
}