using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class PontosJogadorMesaRepository : IPontosJogadorMesaRepositories
    {
        private readonly AppContexto contexto;

        public PontosJogadorMesaRepository(AppContexto context)
        {
            contexto = context;
        }
        public async Task<PontosJogadorMesa> Create(PontosJogadorMesa request)
        {
            contexto.pontosJogadoresMesa.AddAsync(request);
            await contexto.SaveChangesAsync();
            return request;
        }

        public async Task<PontosJogadorMesa> Delete(int id)
        {
            var request = GetbyId(id);
            contexto.Remove(request);
            await contexto.SaveChangesAsync();
            return await request;
        }

        public async Task<IEnumerable<PontosJogadorMesa>> GetAll()
        {
            return await contexto.pontosJogadoresMesa.ToListAsync();
        }

        public async Task<PontosJogadorMesa> GetbyId(int id)
        {
            return await contexto.pontosJogadoresMesa.FindAsync(id);
        }

        public async Task<List<PontosJogadorMesa>> GetbyMesa(int mesa)
        {
            var lista = await contexto.pontosJogadoresMesa.Where(c => c.mesaid == mesa).Include(c => c.jogador).ToListAsync();
            return  lista;
        }

        public async Task<PontosJogadorMesa> Update(PontosJogadorMesa request)
        {
            contexto.pontosJogadoresMesa.Entry(request).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return request;
        }
    }
}
