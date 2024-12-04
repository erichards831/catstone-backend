namespace CatstoneApi.Utilities;


public class JwtSettings{
    public string? Secret {get; set;}
    public int ExpireInMinutes {get; set;}
}