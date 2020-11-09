using System;
using System.Collections.Generic;
using Domain.src.Jogadores;
using Domain.src.Times;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Times;

namespace WebAPI.Controllers.Times
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class TimesController : ControllerBase
    {
        private readonly TimeServices timeServices;

        public TimesController()
        {
            timeServices = new TimeServices();
        }

        [HttpGet]
        public List<TimeDTO> GetTimes()
        {
            return timeServices.ObterTimes();
        }

        [HttpGet("{idTime}")]
        public TimeDTO GetTime(Guid idTime)
        {
            return timeServices.ObterTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores")]
        public List<JogadorDTO> GetJogadoresDoTime(Guid idTime)
        {
            return timeServices.ObterJogadoresDoTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores/{idJogador}")]
        public JogadorDTO GetJogador(Guid idTime, Guid idJogador)
        {
            return timeServices.ObterJogadorDoTime(idTime, idJogador);
        }

        [HttpPost]
        public IActionResult PostTime(TimeRequest request)
        {
            timeServices.AdicionarTime(request.Nome);
            return NoContent();
        }

        // [HttpPost("{idTime}/Jogadores")]
        // public IActionResult PostJogadorAoTime(Guid idTime, JogadorRequest request)
        // {
        //     if (!timeServices.AdicionarJogadorAoTime(idTime, request.Nome)) return BadRequest("Nome Inválido");
        //     return NoContent();
        // }

        [HttpPost("{idTime}/Jogadores")]
        public IActionResult PostJogadoresAoTime(Guid idTime, List<Jogador> jogadores)
        {
            RepositorioTimes.AdicionarJogadoresAoTime(idTime, jogadores);
            return Ok();
        }

        [HttpPut("{idTime}")]
        public IActionResult PutNomeTime(Guid idTime, TimeRequest request)
        {
            if (!timeServices.ModificarNomeTime(idTime, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }
            return NoContent();
        }

        [HttpPatch("{idTime}")]
        public IActionResult PatchNomeTime(Guid idTime, TimeRequest request)
        {
            if (!timeServices.ModificarNomeTime(idTime, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }
            return NoContent();
        }

        [HttpPut("{idTime}/Jogadores/{idJogador}")]
        public IActionResult PutJogadorTime(Guid idTime, Guid idJogador, JogadorRequest request)
        {
            if (!timeServices.ModificarNomeJogador(idTime, idJogador, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }

            return Ok(request.Nome);
        }

        [HttpDelete("{idTime}")]
        public IActionResult DeleteTime(Guid idTime)
        {
            timeServices.RemoverTime(idTime);
            return NoContent();
        }

        [HttpDelete("{idTime}/Jogadores/{idJogador}")]
        public IActionResult DeleteJogador(Guid idTime, Guid IdJogador)
        {
            if (!timeServices.RemoverJogadorDoTime(idTime,IdJogador)) return Forbid();
            return NoContent();
        }
    }
}