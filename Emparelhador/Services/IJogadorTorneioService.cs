using Emparelhador.DTOs;

namespace Emparelhador.Services
{
    public interface IJogadorTorneioService
    {
        Task<IEnumerable<JogadorTorneioDTO>> GetAll();
        Task<JogadorTorneioDTO> GetbyId(int id);
        Task<IEnumerable<JogadorTorneioDTO>> GetbyTorneio(int idTorneio);
        Task Criar(JogadorTorneioDTO request);
        Task Atualizar(JogadorTorneioDTO request);
        Task Excluir (int id);
    }
}
