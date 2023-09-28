namespace Emparelhador.Models
{
    public class JogadorTorneio
    {
        public int id { get; set; }
        public Jogador? jogador { get; set; }
        public int jogadorId { get; set; }
        public int pontos { get; set; }
        public int vitorias { get; set; }
        public int somaAdversarios { get; set; }
        //public ICollection<Jogador>? JogadoresVenceu { get; set; }
        //public Torneio? torneio { get; set; }
        public int torneioId { get; set; }
        public int titulos { get; set; }    
        public int qtdby { get; set; }
        public int indice { get; set; }
    }
}
