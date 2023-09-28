using AutoMapper;
using Emparelhador.DTOs;
using Emparelhador.Models;
using Emparelhador.Repositories;

namespace Emparelhador.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepositories _repository;
        private readonly IMapper _mapper;

        public JogadorService(IJogadorRepositories repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Atualizar(JogadorDTO request)
        {
            var dado = _mapper.Map<Jogador>(request);
            var dados = await _repository.Update(dado);
        }

        public async Task Criar(JogadorDTO request)
        {
            var dado = _mapper.Map<Jogador>(request);
            var dados = await _repository.Create(dado);
            dado.id = dados.id;
            
        }

        public async Task Delete(int id)
        {
            var dado = _repository.GetbyId(id).Result;
            await _repository.Delete(dado.id);
        }

        public async Task<IEnumerable<JogadorDTO>> GetAll()
        {
            var dados = await _repository.GetAll();
            return _mapper.Map<IEnumerable<JogadorDTO>>(dados);
        }

        public async Task<JogadorDTO> GetbyId(int id)
        {
            var dados = await _repository.GetbyId(id);
            return _mapper.Map<JogadorDTO>(dados);
        }
    }
}
