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
        return Ok(await _characterService.GetAll());
    }

    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.GetById(id);
        if (serviceResponse.Success == false)
        {
            if (serviceResponse.Message == "No permissions")
            {
                return BadRequest(serviceResponse);
            }

            return NotFound(serviceResponse);
        }
        return Ok(serviceResponse);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddOne(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddOne(newCharacter));
    }

    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddSkillToCharacter(AddCharacterSkillDto newSkill)
    {
        return Ok(await _characterService.AddSkill(newSkill));
    }
    
    [HttpPut("UpdateCharacter")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateOne(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.UpdateOne(updatedCharacter);
        if (serviceResponse.Success == false)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }
    
    [HttpDelete("DeleteCharacter")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteById(int id)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.DeleteById(id);
        if (serviceResponse.Success == false)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }
}