using CatstoneApi.Data;
using CatstoneApi.DTO;
using cUtil = CatstoneApi.Utilities.CatUtility;

namespace CatstoneApi.Services;

public class CatService : ICatService{

    private readonly ICatRepo _catRepo;
    private readonly IUserService _userService;

    public CatService(ICatRepo catRepo, IUserService userService){
        _catRepo = catRepo;
        _userService = userService;
    }

    public Cat AddCat(CatDTO newCatDTO){
        
        Cat newCat = cUtil.DTOtoCat(newCatDTO);
        return _catRepo.AddCat(newCat);
    }

    public Cat? AddCatToUser(int userId, CatDTO catToStore){
        try{
            var user = _userService.GetUserById(userId).Result;
            if(user is not null){
                Cat newCat = cUtil.DTOtoCat(catToStore);
                newCat.Owner = user;
                return _catRepo.AddCat(newCat);
            }
            return null;

        }catch(Exception e){
            throw;

        }
        
    }

    public List<Cat> GetAllCats(){

        return _catRepo.GetAllCats();
    }

    public Cat? GetCatById(int id)
    {
        return _catRepo.GetCatById(id);   
    }
}