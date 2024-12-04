


using Microsoft.AspNetCore.Mvc;
using CatstoneApi.Services;
using CatstoneApi.DTO;
namespace CatstoneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService){
        _tokenService = tokenService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO userDTO){
        return null;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(){
        return null;
    }

}