using Microsoft.EntityFrameworkCore;
using CatstoneApi.DTO;

namespace CatstoneApi.Data;


public class CatRepo : ICatRepo{

    private readonly CatstoneDbContext _context;

    public CatRepo(CatstoneDbContext context){
        this._context = context;
    }

    // CRUD -ish
    public Cat AddCat(Cat newCat){
        _context.Add(newCat);
        _context.SaveChanges();
        return newCat;
    }

    public List<Cat> GetAllCats(){
        return _context.Cats.ToList();
    }

    public Cat? GetCatById(int id){
        return _context.Cats.FirstOrDefault(c => c.CatId == id);
        
    }
}