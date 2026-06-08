using System.Text.Json.Serialization;

namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class RetroCard
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Texto { get; set; }
        public int Votos { get; set; }
        public DateTime DataCriacao { get; set; }
        public int SprintRetroId { get; set; }

        [JsonIgnore]
        public SprintRetro SprintRetro { get; set; }
    }
}