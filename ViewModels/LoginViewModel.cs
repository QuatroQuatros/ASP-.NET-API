using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email informado não é válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string? Password { get; set; } 
}