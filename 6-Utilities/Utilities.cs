using CatstoneApi.DTO;


namespace CatstoneApi.Utilities;

public static class CatUtility{
    public static Cat DTOtoCat(CatDTO catDTO){
        Cat newCat = new(){
            ImgId = catDTO.ImgId,
            Width = catDTO.Width,
            Url = catDTO.Url,
            Height = catDTO.Height,
            Favorite = false,          // change when put method added
            UserId = catDTO.UserId
        };

        return newCat;
    }


}

public static class UserUtility{
    public static User DTOtoUser(UserDTO userDTO){
        User newUser = new(){
            Username = userDTO.Username,
            Password = userDTO.Password
        };

        return newUser;
    }


}