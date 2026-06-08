namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class SprintTask
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public int Pontos { get; set; }
        public string Prioridade { get; set; } = "";
        public bool NaSprint { get; set; }
        public int SprintPlanningId { get; set; }
    }
}