using CatstoneApi.DTO;
using CatstoneApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatstoneApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase{
    
    private readonly IUserService _userService;
    // private readonly ICatService _catService;

    public UserController(IUserService userService){
        _userService = userService;

    }

    //CRUD
    [HttpPost]
    public async Task<IActionResult> AddUser(UserDTO newUser){
        return Ok(await _userService.AddUser(newUser));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(){
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id){
        try{
            var user = await _userService.GetUserById(id);
            if(user is null) return NotFound("User does not exist!");
            return Ok(user);

        }catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updateUser){
        try{
            var user = await _userService.UpdateUser(id, updateUser);
            if(user is null) return NotFound("User does not exist!");
            return Ok(user);

        }catch(Exception e){
            return BadRequest(e.Message);

        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id){
       try{
            var user = await _userService.DeleteUser(id);
            if(user is null) return NotFound("User does not exist!");
            return Ok(user);

        }catch(Exception e){
            return BadRequest(e.Message);

        }
    }

    
}