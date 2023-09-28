namespace Emparelhador.Models
{
    public class Mesa
    {
        public int id { get; set; }
        public int rodada { get; set; }
        public Torneio? torneio { get; set; }
        public int torneioID { get; set; }
        public ICollection<PontosJogadorMesa>? pontosjogadores { get; set;}
    }
}
