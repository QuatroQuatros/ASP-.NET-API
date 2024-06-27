using System.ComponentModel.DataAnnotations;
using GestaoDeResiduos.Attributes;

namespace GestaoDeResiduos.ViewModels.Update;

public class UserViewModelUpdate
{
    public string? Name { get; set; }
    
    [EmailAddress(ErrorMessage = "O email informado não é válido.")]
    public string? Email { get; set; }
    
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string? Password { get; set; }
    
    [BirthDate(ErrorMessage = "A Data de Nascimento não pode estar no futuro.")]
    public DateTime? BirthDate { get; set; }

    public string? Role { get; set; }
}