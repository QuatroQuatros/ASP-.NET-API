using System.ComponentModel.DataAnnotations;

namespace GestaoDeResiduos.ViewModels;

public class GarbageCollectionTypeViewModel
{
    [Required(ErrorMessage = "O nome do tipo de coleta é obrigatório.")]
    public string Name { get; set; }
}