using CatstoneApi.DTO;


namespace CatstoneApi.Utilities;

public static class CatUtility{
    public static Cat DTOtoCat(CatDTO catDTO){
        Cat newCat = new(){
            CatId = catDTO.CatId,
            ImgId = catDTO.ImgId,
            Width = catDTO.Width,
            Height = catDTO.Height,
            Favorite = false            // change when put method added
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