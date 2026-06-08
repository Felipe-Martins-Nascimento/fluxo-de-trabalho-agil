namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class SprintRetro
    {
        public int Id { get; set; }
        public string NomeSprint { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? ResumoIA { get; set; }
        public ICollection<RetroCard> Cards { get; set; } = new List<RetroCard>();
    }
}