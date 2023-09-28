namespace Emparelhador.Models
{
    public class PontosJogadorMesa
    {
        public int id { get; set; }
        public Jogador? jogador { get; set; }
        public int jogadorid { get; set; }
        public bool vitoria { get; set; }
        public int pontos { get; set; }
        public int posicao { get; set; }       
        public int mesaid { get; set; }
        public bool nby { get; set; }
       
    }
}
