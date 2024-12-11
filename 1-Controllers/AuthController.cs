


using Microsoft.AspNetCore.Mvc;
using CatstoneApi.Services;
using CatstoneApi.DTO;
namespace CatstoneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService){
        _authService = authService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO userDTO){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        try{
            var uRespDTO = await _authService.LoginAsync(userDTO);
            return Ok(uRespDTO);
        }catch(Exception e){
            return Unauthorized(new {Message = e.Message});
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(){
        try{
            await _authService.LogoutAsync();
            return Ok(new {Message = "Logged out successfully!"});
        }catch(Exception e){
            return BadRequest(new {Message = e.Message});
        }
    }

}