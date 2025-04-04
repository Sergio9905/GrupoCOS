using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PruebaTecnicaGrupoCOS.Application.DTOs;

namespace PruebaTecnicaGrupoCOS.Application.Responses
{
    public class CommentResponse : CommentDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserAccountResponse Author { get; set; }
        public PostResponse Post { get; set; }
    }
}
