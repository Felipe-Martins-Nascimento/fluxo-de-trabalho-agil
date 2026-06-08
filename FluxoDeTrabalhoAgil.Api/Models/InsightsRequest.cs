namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class InsightsRequest
    {
        public List<string> Positivos { get; set; }
        public List<string> Negativos { get; set; }
        public List<string> Sugestoes { get; set; }
    }
}