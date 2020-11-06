using System;
using System.Collections.Generic;
using Domain.src;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositorio;
using WebAPI.Times;

namespace WebAPI.Controllers.Times
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class TimesController : ControllerBase
    {
        [HttpGet]
        public List<Time> GetTimes()
        {
            return RepositorioTimes.ObterTimes();
        }

        [HttpGet("{idTime}")]
        public Time GetTime(Guid idTime)
        {
            return RepositorioTimes.ObterTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores")]
        public IReadOnlyCollection<Jogador> GetJogadoresDoTime(Guid idTime)
        {
            return RepositorioTimes.ObterJogadoresDoTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores/{idJogador}")]
        public Jogador GetJogador(Guid idTime, Guid idJogador)
        {
            return RepositorioTimes.ObterJogadorDoTime(idTime, idJogador);
        }

        [HttpPost]
        public IActionResult PostTime(TimeRequest request)
        {
            var timeAAdicionar = new TimeCampeonatoBrasileirao(request.Nome);
            RepositorioTimes.AdicionarTime(timeAAdicionar);
            return NoContent();
        }

        [HttpPost("{idTime}/Jogadores")]
        public IActionResult PostJogadorAoTime(Guid idTime, JogadorRequest request)
        {
            var jogadorAadicionar = new JogadorTime(request.Nome);
            var timeAAdicionarJogador = RepositorioTimes.ObterTime(idTime);
            if (!timeAAdicionarJogador.AdicionarJogador(jogadorAadicionar)) return BadRequest(); 
            return NoContent();
        }

        [HttpPut("{idTime}")]
        public IActionResult PutNomeTime(Guid idTime, TimeRequest request)
        {
            var timeAAtualizar = RepositorioTimes.ObterTime(idTime);
            timeAAtualizar.ModificarNomeTime(request.Nome);
            return NoContent();
        }

        [HttpPatch("{idTime}")]
        public IActionResult PatchNomeTime(Guid idTime, TimeRequest request)
        {
            var timeAAtualizar = RepositorioTimes.ObterTime(idTime);
            timeAAtualizar.ModificarNomeTime(request.Nome);
            return Ok(timeAAtualizar);
        }

        [HttpPut("{idTime}/Jogadores/{idJogador}")]
        public IActionResult PutJogadorTime(Guid idTime, Guid idJogador, JogadorRequest request)
        {
            var jogadorRecuperado = RepositorioTimes.ObterJogadorDoTime(idTime, idJogador);
            jogadorRecuperado.AdicionarNomeJogador(request.Nome);
            return NoContent();
        }

        [HttpDelete("{idTime}")]
        public IActionResult DeleteTime(Guid idTime)
        {
            RepositorioTimes.RemoverTime(idTime);
            return NoContent();
        }

        [HttpDelete("{idTime}/Jogadores/{idJogador}")]
        public IActionResult DeleteJogador(Guid idTime, Guid IdJogador)
        {
            if (!RepositorioTimes.RemoverJogadorDoTime(idTime, IdJogador)) return Forbid();
            return NoContent();
        }
    }
}