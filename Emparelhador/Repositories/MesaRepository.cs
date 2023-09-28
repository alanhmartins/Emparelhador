using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class MesaRepository : IMesaRepositories
    {
        private readonly AppContexto contexto;

        public MesaRepository(AppContexto context)
        {
            contexto = context;
        }
        public async Task<Mesa> Create(Mesa request)
        {
            contexto.mesas.AddAsync(request);
            await contexto.SaveChangesAsync();
            return request;
        }

        public async Task<Mesa> Delete(int id)
        {
            var request = GetbyId(id);
            contexto.Remove(request);
            await contexto.SaveChangesAsync();
            return await request;
        }

        public async Task<IEnumerable<Mesa>> GetAll()
        {
            return await contexto.mesas.ToListAsync();
        }

        public async Task<Mesa> GetbyId(int id)
        {
            return await contexto.mesas.FindAsync(id);
        }

        public async Task<List<Mesa>> GetByTorneio(int id)
        {
            var mesas = await contexto.mesas.Where(c=>c.torneioID == id).ToListAsync();
            return mesas;
        }

        public async Task<Mesa> Update(Mesa request)
        {
            contexto.mesas.Entry(request).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return request;
        }
    }
}
