using Emparelhador.Models;

namespace Emparelhador.Repositories
{
    public interface IPontosJogadorMesaRepositories
    {
        Task<IEnumerable<PontosJogadorMesa>> GetAll();
        Task<PontosJogadorMesa> GetbyId(int id);
        Task<List<PontosJogadorMesa>> GetbyMesa(int mesa);
        Task<PontosJogadorMesa> Create(PontosJogadorMesa request);
        Task<PontosJogadorMesa> Update(PontosJogadorMesa request);
        Task<PontosJogadorMesa> Delete(int id);
    }
}
