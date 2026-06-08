namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class SprintPlanning
    {
        public int Id { get; set; }
        public string NomeSprint { get; set; } = "";
        public string MetaSprint { get; set; } = "";
        public int QtdDevs { get; set; }
        public int HorasDev { get; set; }
        public int Velocity { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<SprintTask> Backlog { get; set; } = new();
    }
}