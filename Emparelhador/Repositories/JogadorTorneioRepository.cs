using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class JogadorTorneioRepository : IJogadorTorneioRepositories
    {
        private readonly AppContexto contexto;

        public JogadorTorneioRepository(AppContexto context)
        {
            contexto = context;
        }
        public async  Task<JogadorTorneio> Create(JogadorTorneio request)
        {
            contexto.jogadoresTorneios.AddAsync(request);
            await contexto.SaveChangesAsync();
            return request;
        }

        public async Task<JogadorTorneio> Delete(int id)
        {
            var request = GetbyId(id);
            contexto.Remove(request);
            await contexto.SaveChangesAsync();
            return await request;
        }

        public async Task<IEnumerable<JogadorTorneio>> GetAll()
        {
            return await contexto.jogadoresTorneios.OrderByDescending(c =>c.pontos).ToListAsync();
        }

        public async Task<JogadorTorneio> GetbyId(int id)
        {
            var x = await contexto.jogadoresTorneios.Where(c => c.id == id).FirstOrDefaultAsync();
            return x;
        }
        public async Task<JogadorTorneio> GetByIdTorneio(int id, int torneio)
        {
            var x = await contexto.jogadoresTorneios.Where(c => c.jogadorId==id&&c.torneioId==torneio).FirstOrDefaultAsync() ;   
            return x;
        }

        public async Task<IEnumerable<JogadorTorneio>> GetByTorneio(int idTorneio)
        {
            var randomSeed = new Random();
            return await contexto.jogadoresTorneios.Include(c=>c.jogador).Where(c=>c.torneioId==idTorneio).OrderByDescending(c=>c.pontos).ThenByDescending(c=>c.vitorias).ThenBy(c => c.qtdby).ThenByDescending(c=>c.somaAdversarios).ThenByDescending(c=>c.titulos).ThenBy(c => Guid.NewGuid()).ToListAsync();
        }

        public async Task<JogadorTorneio> Update(JogadorTorneio request)
        {
            contexto.jogadoresTorneios.Entry(request).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return request;
        }
    }
}
