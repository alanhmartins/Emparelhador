using AutoMapper;
using Emparelhador.DTOs;
using Emparelhador.Models;
using Emparelhador.Repositories;

namespace Emparelhador.Services
{
    public class JogadorTorneioService : IJogadorTorneioService
    {
        private readonly IJogadorTorneioRepositories _repository;
        private readonly IMapper _mapper;

        public  JogadorTorneioService(IJogadorTorneioRepositories repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Atualizar(JogadorTorneioDTO request)
        {
            var dados = _mapper.Map<JogadorTorneio>(request);
            await _repository.Update(dados);
        }

        public async Task Criar(JogadorTorneioDTO request)
        {
            var dados = _mapper.Map<JogadorTorneio>(request);
            await _repository.Create(dados);
        }

        public async Task Excluir(int id)
        {
            var dados = _repository.GetbyId(id).Result;
            await _repository.Delete(dados.id);
        }

        public async Task<IEnumerable<JogadorTorneioDTO>> GetAll()
        {
            var dados = _repository.GetAll();
            return _mapper.Map<IEnumerable<JogadorTorneioDTO>>(dados);
        }

        public async Task<JogadorTorneioDTO> GetbyId(int id)
        {
            var dados = _repository.GetbyId(id);
            return _mapper.Map<JogadorTorneioDTO>(dados);
        }

        public async Task<IEnumerable<JogadorTorneioDTO>> GetbyTorneio(int idTorneio)
        {
            var dados = _repository.GetByTorneio(idTorneio);
            return _mapper.Map<IEnumerable<JogadorTorneioDTO>>(dados);
        }
    }
}
