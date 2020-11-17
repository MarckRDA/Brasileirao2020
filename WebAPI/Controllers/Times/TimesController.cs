using System;
using System.Collections.Generic;
using Domain.src.Jogadores;
using Domain.src.Times;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public List<TimeDTO> GetTimes()
        {
            return timeServices.ObterTimes();
        }

        [HttpGet("{idTime}")]
        [Authorize]
        public TimeDTO GetTime(Guid idTime)
        {
            return timeServices.ObterTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores")]
        [Authorize]
        public List<JogadorDTO> GetJogadoresDoTime(Guid idTime)
        {
            return timeServices.ObterJogadoresDoTime(idTime);
        }

        [HttpGet("{idTime}/Jogadores/{idJogador}")]
        [Authorize]
        public JogadorDTO GetJogador(Guid idTime, Guid idJogador)
        {
            return timeServices.ObterJogadorDoTime(idTime, idJogador);
        }

        [HttpPost]
        [Authorize(Roles = "cbf")]
        public IActionResult PostTime(TimeRequest request)
        {
            if (!timeServices.AdicionarTime(request.Nome)) return BadRequest("Nome Inválido");

            return NoContent();
        }

        [HttpPost("{idTime}/Jogadores")]
        [Authorize(Roles = "cbf")]
        public IActionResult PostJogadorAoTime(Guid idTime, JogadorRequest request)
        {
            if (!timeServices.AdicionarJogadorAoTime(idTime, request.Nome)) return BadRequest("Nome Inválido");
            return NoContent();
        }

        [HttpPost("{idTime}/Jogadores")]
        [Authorize(Roles = "cbf")]
        public IActionResult PostJogadoresAoTime(Guid idTime, List<Jogador> jogadores)
        {
            RepositorioTimes.AdicionarJogadoresAoTime(idTime, jogadores);
            return Ok();
        }

        [HttpPut("{idTime}")]
        [Authorize(Roles = "cbf")]
        public IActionResult PutNomeTime(Guid idTime, TimeRequest request)
        {
            if (!timeServices.ModificarTime(idTime, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }
            return NoContent();
        }

        [HttpPatch("{idTime}")]
        [Authorize(Roles = "cbf")]
        public IActionResult PatchNomeTime(Guid idTime, TimeRequest request)
        {
            if (!timeServices.ModificarTime(idTime, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }
            return NoContent();
        }

        [HttpPut("{idTime}/Jogadores/{idJogador}")]
        [Authorize(Roles = "cbf")]
        public IActionResult PutJogadorTime(Guid idTime, Guid idJogador, JogadorRequest request)
        {
            if (!timeServices.ModificarNomeJogador(idTime, idJogador, request.Nome))
            {
                return BadRequest("Nome Inválido");
            }

            return Ok(request.Nome);
        }

        [HttpDelete("{idTime}")]
        [Authorize(Roles = "cbf")]
        public IActionResult DeleteTime(Guid idTime)
        {
            timeServices.RemoverTime(idTime);
            return NoContent();
        }

        [HttpDelete("{idTime}/Jogadores/{idJogador}")]
        [Authorize(Roles = "cbf")]
        public IActionResult DeleteJogador(Guid idTime, Guid IdJogador)
        {
            if (!timeServices.RemoverJogadorDoTime(idTime,IdJogador)) return Forbid();
            return NoContent();
        }
    }
}