using System;
using System.ComponentModel.DataAnnotations;
using GestaoDeResiduos.Attributes;

namespace GestaoDeResiduos.ViewModels;

public class UserViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email informado não é válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
    [BirthDate(ErrorMessage = "A Data de Nascimento não pode estar no futuro.")]
    public DateTime? BirthDate { get; set; }

    public string? Role { get; set; }

}
