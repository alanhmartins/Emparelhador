using System.ComponentModel.DataAnnotations;

namespace Emparelhador.DTOs
{
    public class JogadorDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Nome é obrigatório")]
        [MaxLength(100)]
        public string? nome { get; set; }
        public int vitorias { get; set; }
    }
}
