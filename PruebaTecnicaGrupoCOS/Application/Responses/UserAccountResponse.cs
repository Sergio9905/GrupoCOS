using PruebaTecnicaGrupoCOS.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaGrupoCOS.Application.Responses
{
    public class UserAccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
