using Emparelhador.Models;

namespace Emparelhador.Repositories
{
    public interface IConfrontosDiretosRepositories
    {
        Task<IEnumerable<confrontosdiretos>> ListaJogadoresVencidosTorneio(int jogador, int torneio);
        Task<confrontosdiretos> Create(confrontosdiretos request);
    }
}
