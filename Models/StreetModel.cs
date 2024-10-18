using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDeResiduos.Models;

public class StreetModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("DISTRICT_ID")]
    [ForeignKey("DISTRICT")]
    public int DistrictId { get; set; }
    
    [Column("NAME")]
    public string? Name { get; set; }

    public DistrictModel? District { get; set; }
}