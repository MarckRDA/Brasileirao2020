using System;
using System.Collections.Generic;
using Domain.src;
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

    }
}