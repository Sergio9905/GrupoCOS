using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PruebaTecnicaGrupoCOS.Application.DTOs;

namespace PruebaTecnicaGrupoCOS.Application.Responses
{
    public class PostResponse : PostDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserAccountResponse Author { get; set; }
    }
}
