namespace PruebaTecnicaGrupoCOS.Application.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserAccountId { get; set; }
        public int PostId { get; set; }
    }
}
