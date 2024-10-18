using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDeResiduos.Models;

public class StateModel
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("NAME")]
    public string Name { get; set; }
    
    [Column("UF")]
    public string? UF { get; set; }
    
}