using Emparelhador.Models;

namespace Emparelhador.DTOs
{
    public class TorneioDTO
    {
        public int id { get; set; }       
        public int rodadas { get; set; }
        public DateTime? data { get; set; }
        public List<int> jogadores { get; set; }
   
    }
}
