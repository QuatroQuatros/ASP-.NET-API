using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDeResiduos.Models;

public class RegionModel
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("STATE_ID")]
    [ForeignKey("State")]
    public int StateId { get; set; }
    
    [Column("NAME")]
    public string Name { get; set; }

    public StateModel State { get; set; }
}