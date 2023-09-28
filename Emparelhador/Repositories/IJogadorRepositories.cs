using Emparelhador.Models;
using System.Collections;

namespace Emparelhador.Repositories
{
    public interface IJogadorRepositories
    {
        Task<IEnumerable<Jogador>> GetAll();
        Task<Jogador> GetbyId(int id);
        Task<Jogador> Create(Jogador jogador);  
        Task<Jogador> Update(Jogador jogador);
        Task<Jogador> Delete(int id);
    }
}
