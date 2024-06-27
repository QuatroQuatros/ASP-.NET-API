using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestaoDeResiduos.ViewModels;

namespace GestaoDeResiduos.Models;

[Table("USERS")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    public string Name { get; set; }

    [Column("EMAIL")]
    public string Email { get; set; }

    [Column("PASSWORD")]
    public string Password { get; set; }

    [Column("BIRTH_DATE")]
    public DateTime BirthDate { get; set; }

    [Column("ROLE")]
    public string Role { get; set; }
}
