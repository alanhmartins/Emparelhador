using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class TorneioRepository :ITorneioRepositories
    {
        private readonly AppContexto contexto;

        public TorneioRepository(AppContexto context)
        {
            contexto = context;
        }
        public async Task<Torneio> Create(Torneio request)
        {
            contexto.torneios.AddAsync(request);
            await contexto.SaveChangesAsync();
            return request;
        }

        public async Task<Torneio> Delete(int id)
        {
            var request = GetbyId(id);
            contexto.Remove(request);
            await contexto.SaveChangesAsync();
            return await request;
        }

        public async Task<IEnumerable<Torneio>> GetAll()
        {
            return await contexto.torneios.ToListAsync();
        }

        public async Task<Torneio> GetbyId(int id)
        {
            return await contexto.torneios.FindAsync(id);
        }

        public async Task<Torneio> Update(Torneio request)
        {
            contexto.torneios.Entry(request).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return request;
        }
    }
}
