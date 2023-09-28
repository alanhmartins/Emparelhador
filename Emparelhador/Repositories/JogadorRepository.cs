using Emparelhador.Contexto;
using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Repositories
{
    public class JogadorRepository : IJogadorRepositories
    {
        private readonly AppContexto contexto;

        public JogadorRepository(AppContexto context)
        {
            contexto = context;
        }

        public async Task<IEnumerable<Jogador>> GetAll()
        {
            var randomSeed = new Random();

            var jogadores =  await contexto.jogadores.OrderByDescending(c => c.vitorias).ThenBy(c=> Guid.NewGuid()).ToListAsync();
            //jogadores =  jogadores.OrderBy(c => randomSeed.Next()).ToList();
            return jogadores;
        }

        public async Task<Jogador> GetbyId(int id)
        {
            return await contexto.jogadores.FindAsync(id);
        }
        public async Task<Jogador> Create(Jogador jogador)
        {
            contexto.jogadores.AddAsync(jogador);
            await contexto.SaveChangesAsync();
            return jogador;
        }

        public async Task<Jogador> Update(Jogador jogador)
        {
            try
            {
                contexto.jogadores.Entry(jogador).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
                return jogador;
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
        public async Task<Jogador> Delete(int id)
        {
            var jogador = GetbyId(id);
            contexto.Remove(jogador);
            await contexto.SaveChangesAsync();
            return await jogador;
        }



       
    }
}
