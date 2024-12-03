namespace CatstoneApi.DTO;

public class CatDTO{


    public int CatId {get; set;}
    // not primary key, from JSON obj
    public string ImgId {get; set;}
    public string Url {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}

    
}

public class CatWithUserDTO : CatDTO{
    public int UserId {get; set;}
}