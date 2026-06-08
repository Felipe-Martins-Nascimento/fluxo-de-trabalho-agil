namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class Reuniao
    {
        public int Id { get; set; }
        public string Tipo { get; set; } 
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string? AudioPath { get; set; }
        public string? ResumoIA { get; set; }
        public DateTime Data { get; set; }
    }
}