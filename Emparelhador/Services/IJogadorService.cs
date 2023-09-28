using Emparelhador.DTOs;

namespace Emparelhador.Services
{
    public interface IJogadorService
    {
        Task<IEnumerable<JogadorDTO>> GetAll();
        Task<JogadorDTO> GetbyId(int id);
        Task Criar(JogadorDTO request);
        Task Atualizar(JogadorDTO request);
        Task Delete(int id);
    }
}
