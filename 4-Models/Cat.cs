using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using CatstoneApi.DTO;

public class Cat{


    public int CatId {get; set;}

    // not primary key, from JSON obj
    public string ImgId {get; set;}
    public string Url {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}


    // favorite relationship here
    public bool Favorite {get; set;}

    [JsonIgnore]
    public User? Owner {get; set;}

    
}