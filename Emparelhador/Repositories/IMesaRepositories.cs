using Emparelhador.Models;

namespace Emparelhador.Repositories
{
    public interface IMesaRepositories
    {
        Task<IEnumerable<Mesa>> GetAll();
        Task<Mesa> GetbyId(int id);
        Task<Mesa> Create(Mesa request);
        Task<Mesa> Update(Mesa request);
        Task<Mesa> Delete(int id);
        Task<List<Mesa>> GetByTorneio(int id);
    }
}
