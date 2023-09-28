using AutoMapper;
using Emparelhador.Models;

namespace Emparelhador.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Jogador,JogadorDTO>().ReverseMap();
            CreateMap<Mesa,MesaDTO>().ReverseMap(); 
            CreateMap<JogadorTorneio,JogadorTorneioDTO>().ReverseMap(); 
            CreateMap<PontosJogadorMesa,PontosJogadorMesaDTO>().ReverseMap();
            CreateMap<Torneio, TorneioDTO>().ReverseMap();
        }
    }
}
