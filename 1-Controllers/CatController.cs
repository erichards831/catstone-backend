using CatstoneApi.DTO;
using CatstoneApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CatstoneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatController : ControllerBase{


    private readonly ICatService _catService;
    private readonly IUserService _userService;

    public CatController(ICatService catService, IUserService userService){
        _catService = catService;
        _userService = userService;
    }

    [HttpPost]
    public IActionResult AddCat(CatDTO catToStore){
        var newCat = _catService.AddCat(catToStore);
        return Ok(newCat);

    }

    [HttpPost("/User/{userId}")]
    public IActionResult AddCatToUser(int userId, CatDTO catToStore){
        try{
            var cat = _catService.AddCatToUser(userId, catToStore);
            if(cat is null) return NotFound("No such user!");
            return Ok(cat);
        }catch(Exception e){
            return BadRequest(e.Message);

        }
    }

    [HttpGet]
    public IActionResult GetAllCats(){
        return Ok(_catService.GetAllCats());
    }


    [HttpGet("{id}")]
    public IActionResult GetCatById(int id){
        var cat = _catService.GetCatById(id);
        if(cat is null) return NotFound("Cat does not exist!");
        return Ok(cat);
    }
}