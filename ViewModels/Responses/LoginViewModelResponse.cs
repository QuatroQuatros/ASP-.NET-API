namespace GestaoDeResiduos.Responses;

public class LoginViewModelResponse
{
    public UserViewModelResponse User { get; set; }
    public string Token { get; set; }
    
    
    public LoginViewModelResponse(UserViewModelResponse user, string token)
    {
        User = user;
        Token = token;
    }
}