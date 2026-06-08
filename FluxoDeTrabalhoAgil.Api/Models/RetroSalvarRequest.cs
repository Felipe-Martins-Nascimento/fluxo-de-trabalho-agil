namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class RetroSalvarRequest
    {
        public int Id { get; set; }
        public string NomeSprint { get; set; }
        public List<RetroCardDto> Cards { get; set; } = new();
    }

    public class RetroCardDto
    {
        public string Tipo { get; set; }
        public string Texto { get; set; }
        public int Votos { get; set; }
    }
}