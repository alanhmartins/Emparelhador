using Emparelhador.Models;

namespace Emparelhador.Repositories
{
    public interface IJogadorTorneioRepositories
    {
        Task<IEnumerable<JogadorTorneio>> GetAll();
        Task<JogadorTorneio> GetbyId(int id);
        Task<JogadorTorneio> Create(JogadorTorneio request);
        Task<JogadorTorneio> Update(JogadorTorneio request);
        Task<JogadorTorneio> Delete(int id);
        Task<IEnumerable<JogadorTorneio>> GetByTorneio(int idTorneio);
        Task<JogadorTorneio> GetByIdTorneio(int id, int idTorneio);
    }
}
