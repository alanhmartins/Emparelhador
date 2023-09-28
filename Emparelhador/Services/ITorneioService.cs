using Emparelhador.DTOs;
using Emparelhador.Models;

namespace Emparelhador.Services
{
    public interface ITorneioService
    {
        Task<IEnumerable<TorneioDTO>> GetAll();
        Task<TorneioDTO> GetById(int id);
        Task<TorneioDTO> GetByData(DateTime data);
        Task<List<List<PontosJogadorMesa>>> Criar(TorneioDTO request);
        Task Atualizar(TorneioDTO request);
        Task Excluir(int id);
        Task<List<List<PontosJogadorMesa>>> NovaRodada(IEnumerable<JogadorTorneio> jogadores);



    }
}
