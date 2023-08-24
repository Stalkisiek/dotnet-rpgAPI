using System.Runtime.CompilerServices;
using System.Security.Claims;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;
using dotnet_rpg2.Services.CharacterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg2.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [AllowAnonymous]
    [HttpGet("All")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
        try
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            //var response = await _characterService.GetAll(userId);
            // Add logging or debugging statements to inspect 'response'
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            // You can also return an error response if needed
            return StatusCode(500, "An error occurred.");
        }
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