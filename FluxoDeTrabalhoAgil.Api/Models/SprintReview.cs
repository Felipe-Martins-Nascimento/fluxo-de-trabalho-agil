namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class SprintReview
    {
        public int Id { get; set; }
        public string NomeSprint { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TasksTotais { get; set; }
        public int TasksConcluidas { get; set; }
        public int TasksNaoConcluidas { get; set; }
        public int VelocityEntregue { get; set; }
        public int VelocityNaoEntregue { get; set; }
        public int Participantes { get; set; }
        public int BugsCriticos { get; set; }
        public int BugsMedios { get; set; }
        public int BugsBaixos { get; set; }
        public int BugsCorrigidos { get; set; }
        public int BugsPendentes { get; set; }
        public int BugsEncontrados { get; set; }
        public int StoryPointsPlanejados { get; set; }
        public int StoryPointsEntregues { get; set; }
        public string EficienciaSprint { get; set; }
        public string TaxaSucesso { get; set; }
        public string TempoMedioTask { get; set; }
        public string ParticipacaoTime { get; set; }
        public string PontosPositivos { get; set; }
        public string PontosMelhoria { get; set; }
        public string FeedbackScrumMaster { get; set; }
        public string FeedbackProductOwner { get; set; }
        public string ProximosPassos { get; set; }
        public string DecisoesTomadas { get; set; }
        public List<EntregaReview> Entregas { get; set; } = new();
    }
}