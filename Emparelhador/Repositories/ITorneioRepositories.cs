using Emparelhador.Models;

namespace Emparelhador.Repositories
{
    public interface ITorneioRepositories
    {
        Task<IEnumerable<Torneio>> GetAll();
        Task<Torneio> GetbyId(int id);
        Task<Torneio> Create(Torneio request);
        Task<Torneio> Update(Torneio request);
        Task<Torneio> Delete(int id);
    }
}
