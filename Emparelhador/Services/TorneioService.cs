using AutoMapper;
using Emparelhador.DTOs;
using Emparelhador.Models;
using Emparelhador.Repositories;

namespace Emparelhador.Services
{
    public class TorneioService : ITorneioService
    {
        private readonly ITorneioRepositories _repository;
        private readonly IJogadorTorneioRepositories _repositoryt;
        private readonly IMesaRepositories _repositoryMesas;
        private readonly IPontosJogadorMesaRepositories _repositoryp;
        private readonly IJogadorRepositories _repositoryjogador;
        private readonly IMapper _mapper;


        public TorneioService(ITorneioRepositories repository, IJogadorTorneioRepositories repositoryt, IMesaRepositories repositoryMesas, IPontosJogadorMesaRepositories repositoryp, IJogadorRepositories repositoryjogador, IMapper mapper)
        {
            _repository = repository;
            _repositoryt = repositoryt;
            _repositoryMesas = repositoryMesas;
            _repositoryp = repositoryp;
            _repositoryjogador = repositoryjogador;
            _mapper = mapper;
        }

        public async Task Atualizar(TorneioDTO request)
        {
           var dado = _mapper.Map<Torneio>(request);
            var dados = await _repository.Update(dado);
        }

        public async Task<List<List<PontosJogadorMesa>>> NovaRodada(IEnumerable<JogadorTorneio> jogadores)
        {
            int numeroDeListas = jogadores.Count() / 4;
            List<JogadorTorneio> jogs = jogadores.ToList();
            // Calcula o tamanho de cada sublista
            int tamanhoDaSublista = 4;
            int idtorneio = 0;
            var pegartorneio = jogadores.FirstOrDefault().torneioId;
            var torneio =  _repository.GetbyId(pegartorneio).Result;
            idtorneio = torneio.id;
            var pegarmesastorneio = _repositoryMesas.GetByTorneio(idtorneio).Result;
            var pegarrodada = pegarmesastorneio.OrderByDescending(c => c.id).FirstOrDefault();
            int rodada = pegarrodada.rodada + 1;
            // Divide a lista em sublistas
            List<List<JogadorTorneio>> listasDivididas = new List<List<JogadorTorneio>>();
            for (int i = 0; i < numeroDeListas; i++)
            {
                List<JogadorTorneio> sublista = jogs.Skip(i * tamanhoDaSublista).Take(tamanhoDaSublista).ToList();
                listasDivididas.Add(sublista);
            }
            List<List<PontosJogadorMesa>> ListaMesas = new List<List<PontosJogadorMesa>>();
            List<List<PontosJogadorMesaDTO>> ListaMesasR = new List<List<PontosJogadorMesaDTO>>();
           
            foreach (var item in listasDivididas)
            {
                List<PontosJogadorMesa> MesasRodada = new List<PontosJogadorMesa>();
                Mesa mesa = new Mesa();
                mesa.torneioID = idtorneio;
                mesa.rodada = rodada;
                var cadmesa = await _repositoryMesas.Create(mesa);
                foreach (var items in item)
                {
                    PontosJogadorMesa pmj = new PontosJogadorMesa();
                    pmj.pontos = 0;
                    pmj.vitoria = false;
                    pmj.posicao = 0;
                    pmj.jogadorid = items.jogadorId;
                    pmj.mesaid = cadmesa.id;
                    await _repositoryp.Create(pmj);
                }
                MesasRodada = await _repositoryp.GetbyMesa(cadmesa.id);
                ListaMesas.Add(MesasRodada);
            }
            return ListaMesas;
        }

        public async Task<List<List<PontosJogadorMesa>>> Criar(TorneioDTO request)
        {
            var dado = _mapper.Map<Torneio>(request);
            var dados = await _repository.Create(dado);
          
          
            foreach (var item in request.jogadores)
            {
                JogadorTorneioDTO jogt = new JogadorTorneioDTO();
                jogt.jogadorId = item;
                jogt.torneioId = dados.id;
                jogt.pontos = 0;
                jogt.vitorias = 0;
                jogt.somaAdversarios = 0;
                jogt.titulos = _repositoryjogador.GetbyId(item).Result.vitorias;
                var jogts = _mapper.Map<JogadorTorneio>(jogt);
                await _repositoryt.Create(jogts);
            } 


            List<Jogador> jogadorestorneio=  _repositoryjogador.GetAll().Result.Where(c => request.jogadores.Contains(c.id)).ToList();
            int numeroDeListas = jogadorestorneio.Count / 4;

            // Calcula o tamanho de cada sublista
            int tamanhoDaSublista = 4;

            // Divide a lista em sublistas
            List<List<Jogador>> listasDivididas = new List<List<Jogador>>();
            for (int i = 0; i < numeroDeListas; i++)
            {
                List<Jogador> sublista = jogadorestorneio.Skip(i * tamanhoDaSublista).Take(tamanhoDaSublista).ToList();
                listasDivididas.Add(sublista);
            }
            List<List<PontosJogadorMesa>> ListaMesas = new List<List<PontosJogadorMesa>>();
            List<List<PontosJogadorMesaDTO>> ListaMesasR = new List<List<PontosJogadorMesaDTO>>();
            foreach (var item in listasDivididas)
            {
                List<PontosJogadorMesa> MesasRodada = new List<PontosJogadorMesa>();
                Mesa mesa = new Mesa();
                mesa.torneioID = dados.id;
                mesa.rodada = 1;
                var cadmesa = await _repositoryMesas.Create(mesa);
                foreach (var items in item)
                {
                    PontosJogadorMesa pmj = new PontosJogadorMesa();
                    pmj.pontos = 0;
                    pmj.vitoria = false;
                    pmj.posicao = 0;
                    pmj.jogadorid = items.id;
                    pmj.mesaid = cadmesa.id;
                    await _repositoryp.Create(pmj);                   
                }
                MesasRodada = await _repositoryp.GetbyMesa(cadmesa.id);
                ListaMesas.Add(MesasRodada);
                
            }
            return ListaMesas;

        }


    



        public async Task Excluir(int id)
        {
            var dados = _repository.GetbyId(id).Result;
            await _repository.Delete(dados.id);
        }

        public async Task<IEnumerable<TorneioDTO>> GetAll()
        {
            var dados = _repository.GetAll();
            return _mapper.Map<IEnumerable<TorneioDTO>>(dados);
        }

        public Task<TorneioDTO> GetByData(DateTime data)
        {
            throw new NotImplementedException();
        }

        public async Task<TorneioDTO> GetById(int id)
        {
            var dados = _repository.GetbyId(id);
            return _mapper.Map<TorneioDTO>(dados);
        }
    }
}
