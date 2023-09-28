using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class ConfrontosDiretosRepository : IConfrontosDiretosRepositories
    {
        private readonly AppContexto _contexto;

        public ConfrontosDiretosRepository(AppContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<confrontosdiretos> Create(confrontosdiretos request)
        {
            _contexto.confrontosdiretos.Add(request);
            await _contexto.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<confrontosdiretos>> ListaJogadoresVencidosTorneio(int jogador, int torneio)
        {
            IEnumerable<confrontosdiretos> confrontos = await _contexto.confrontosdiretos.Where(c => c.idjogadorvencedor == jogador && c.idtorneio == torneio).ToListAsync();
            return confrontos;
        }
    }
}
