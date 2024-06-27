using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDeResiduos.Models;

public class DistrictModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("REGION_ID")]
    [ForeignKey("REGION")]
    public int RegionId { get; set; }
    
    [Column("NAME")]
    public string Name { get; set; }

    public RegionModel Region { get; set; }
}