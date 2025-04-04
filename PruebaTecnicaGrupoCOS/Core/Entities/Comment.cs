using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaGrupoCOS.Core.Entities
{
    [Table("Comment")]
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        // Relations
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccount UserAccount { get; set; }
    }
}
