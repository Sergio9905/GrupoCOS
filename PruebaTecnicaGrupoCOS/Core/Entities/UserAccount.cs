using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PruebaTecnicaGrupoCOS.Core.Entities
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(180)]
        public string Email { get; set; }

        [Required]
        [StringLength(180)]
        public string Password { get; set; }

        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
