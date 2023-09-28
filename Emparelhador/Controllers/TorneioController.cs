using Emparelhador.DTOs;
using Emparelhador.Models;
using Emparelhador.Repositories;
using Emparelhador.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emparelhador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneioController : ControllerBase
    {
        private readonly ITorneioService _service;
        private readonly ITorneioRepositories _torneio;
        private readonly IPontosJogadorMesaRepositories _pontosjogador;
        private readonly IJogadorTorneioRepositories _jogador;
        private readonly IMesaRepositories _mesa;
        private readonly IConfrontosDiretosRepositories _cf;

        public TorneioController(ITorneioService service, IPontosJogadorMesaRepositories pontosjogador, IJogadorTorneioRepositories jogador, IMesaRepositories mesa, ITorneioRepositories torneio, IConfrontosDiretosRepositories cf)
        {
            _service = service;
            _pontosjogador = pontosjogador;
            _jogador = jogador;
            _mesa = mesa;
            _torneio = torneio;
            _cf = cf;
        }

        [HttpPost]
        [Route("/api/FecharRodada")]
        public async Task<ActionResult> EncerrarRodada([FromBody] List<List<PontosJogadorMesa>> request)
        {
            int idTorneio = 0;
            var mesa = _mesa.GetbyId(request[0][0].mesaid).Result;
            idTorneio = mesa.torneioID;
            Torneio torneio = _torneio.GetbyId(mesa.torneioID).Result;
            bool encerrartorneio = false;
            if (torneio.rodadas == mesa.rodada)
                encerrartorneio = true;
            foreach (var items in request)
            {
                foreach (var item in items)
                {
                    await _pontosjogador.Update(item);
                    JogadorTorneio jog = await _jogador.GetByIdTorneio(item.jogadorid, idTorneio);
                    jog.pontos += item.pontos;
                    if (item.vitoria)
                        jog.vitorias++;
                    if (item.nby)
                        jog.qtdby++;
                    var soma = items.Where(c => c.mesaid == item.mesaid).Sum(c => c.pontos) - item.pontos;
                    if(item.vitoria)
                    {
                        var lista = items.Where(c => c.mesaid == item.mesaid && c != item).ToList();
                        foreach (var itemv in lista)
                        {
                            confrontosdiretos cff = new confrontosdiretos();
                            cff.idtorneio = idTorneio;
                            cff.idjogadorvencido = itemv.jogadorid;
                            cff.idjogadorvencedor = item.jogadorid;
                            await _cf.Create(cff);
                        }
                    }
                    jog.somaAdversarios += soma;
                    await _jogador.Update(jog);
                }
                
            }
            var tabela =  _jogador.GetByTorneio(idTorneio).Result;
            return Ok(tabela);
            if (encerrartorneio)
            {
                #region logica confronto direto
                // var tabelaagrupadapontos = tabela.GroupBy(j => new { j.pontos, j.vitorias });
                //List<JogadorTorneio> tabelaordenada = new List<JogadorTorneio>();
                // var x = 0;
                // foreach (var grupo in tabelaagrupadapontos)
                // {
                //     var vit = grupo.Key.vitorias;
                //     if (vit > 0)
                //     {
                //         int contador = 1;
                //         bool ordenou = false;
                //         foreach (var item in grupo)
                //         {


                //             if (grupo.Count() == 1)
                //                 tabelaordenada.Add(item);
                //             else
                //             {
                //                 var confrontosdiretos = _cf.ListaJogadoresVencidosTorneio(item.jogadorId, idTorneio).Result;


                //                 if (grupo.Count()==2)
                //                 {
                //                     var confrontosdiretos = _cf.ListaJogadoresVencidosTorneio(item.jogadorId, idTorneio).Result;                                    
                //                     {
                //                         foreach (var it in confrontosdiretos)
                //                         {
                //                             bool jt = grupo.Any(c => c.jogadorId == it.idjogadorvencido);
                //                             if(jt)
                //                             {
                //                                 if (contador == 1)
                //                                 {
                //                                     tabelaordenada.Add(item);
                //                                     tabelaordenada.Add(grupo.ElementAt(1));
                //                                     ordenou = true;
                //                                 }
                //                                 else
                //                                 {
                //                                     tabelaordenada.Add(item);
                //                                     tabelaordenada.Add(grupo.ElementAt(0));
                //                                     ordenou = true;
                //                                 }
                //                                 break;
                //                             }

                //                         }
                //                     }
                //                 }
                //             }

                //         }
                //         if(!ordenou)
                //         {
                //             foreach (var item in grupo)
                //             {
                //                 tabelaordenada.Add(item);
                //             }
                //         }
                //     }
                //     else
                //     {
                //         foreach (var item in grupo)
                //         {
                //             tabelaordenada.Add(item);
                //         }
                //     }
                // }
                #endregion
            }
            return Ok(tabela);
        }
        [HttpPost]
        [Route("/api/ProximaRodada")]
        public async Task<ActionResult> ProximaRodada([FromBody] IEnumerable<JogadorTorneio> jogadores)
        {
            var rodada = await _service.NovaRodada(jogadores);
            return Ok(rodada);
        }
        [HttpPost]
        public async Task<ActionResult> Novo([FromBody] TorneioDTO request)
        {
            if (request == null)
                return BadRequest();
          var lista = await _service.Criar(request);
            return Ok(lista);
           // return new CreatedAtRouteResult("PegarTorneio", new { id = request.id }, request);
        }


        [HttpGet("{id:int}", Name = "PegarTorneio")]

        public async Task<ActionResult<TorneioDTO>> Get(int id)
        {
            var dados = await _service.GetById(id);
            if (dados == null)
                return NotFound();
            return Ok(dados);
        }
    }
}
