using Emparelhador.DTOs;
using Emparelhador.Models;
using Emparelhador.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emparelhador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogadorController : ControllerBase
    {
        private readonly IJogadorService _service;

        public JogadorController(IJogadorService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Jogador>>> Get()
        {
            var dados = await _service.GetAll();
            if(dados==null)
                return NotFound("Sem Jogadores encontrados");
            return Ok(dados);
        }

        [HttpGet("{id:int}",Name= "PegarJogador")]

        public async Task<ActionResult<JogadorDTO>> Get(int id)
        {
            var dados = await _service.GetbyId(id);
            if (dados == null)
                return NotFound();
            return Ok(dados);   
        }

        [HttpPost]

        public async Task<ActionResult> Novo([FromBody] JogadorDTO request)
        {
            if (request == null)
                return BadRequest();
            await _service.Criar(request);
            return new CreatedAtRouteResult("PegarJogador", new { id = request.id }, request);
        }

        [HttpPut]

        public async Task<ActionResult> Atualizar([FromBody] JogadorDTO request)
        {
            if(request==null)
                return BadRequest();
            var jog = await Get(request.id);
            if (jog == null)
                return NotFound();
           await _service.Atualizar(request);
            return new CreatedAtRouteResult("PegarJogador", new { id = request.id }, request);
        }
    }
}
