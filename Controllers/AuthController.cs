using AutoMapper;
using dotnet_rpg2.Dtos;
using dotnet_rpg2.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg2.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _auth;
    private readonly IMapper _mapper;

    public AuthController(IAuthRepository auth, IMapper mapper)
    {
        _auth = auth;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto user)
    {
        var response = await _auth.Register(
            new User { Username = user.Username }, user.Password
        );
        if (response.Data == 0)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}