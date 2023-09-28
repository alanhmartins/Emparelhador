using Emparelhador.Models;

namespace Emparelhador.DTOs
{
    public class MesaDTO
    {
        public int id { get; set; }
        public int rodada { get; set; }
        public Torneio? torneio { get; set; }
        public int torneioID { get; set; }
        public ICollection<PontosJogadorMesa>? pontosjogadores { get; set; }
    }
}
