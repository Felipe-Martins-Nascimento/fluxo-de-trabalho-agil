namespace FluxoDeTrabalhoAgil.Api.Models
{
    public class MensagemDaily
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Texto { get; set; }
        public int Nota { get; set; } = 0;
        public DateTime Data { get; set; }
        public List<string> Audios { get; set; } = new();
        public List<RespostaDaily> Respostas { get; set; } = new();
    }
}