using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Domain.src.Jogadores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Controllers.Jogadores
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class JogadoresController : ControllerBase
    {
        public readonly JogadorServices jogadorServices;

        public JogadoresController()
        {
            jogadorServices = new JogadorServices();
        }

        [HttpGet]
        [Authorize]
        public List<JogadorDTO> GetJogadores()
        {
            return jogadorServices.ObterJogadores();
        }

        [HttpGet("{id}")]
        [Authorize]
        public JogadorDTO GetJogador(Guid id)
        {
            return jogadorServices.ObterJogador(id);
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult PostJogador(JogadorRequest request)
        {
            StringValues userId;
            
            if (!Request.Headers.TryGetValue("UserId", out userId))
            {
                return Unauthorized();
            }

            var jogadorAGravar = jogadorServices.CriarJogador(request.Nome);
            
            if (jogadorAGravar == null)
            {
                return BadRequest();    
            }

            return CreatedAtAction(nameof(GetJogador), new { id = jogadorAGravar }, jogadorAGravar);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "0")]
        public IActionResult PutJogador(Guid id, JogadorRequest request)
        {
            var jogadorAAtualizar = jogadorServices.AtualizarJogador(id, request.Nome);

            if (jogadorAAtualizar == null)
            {
                return BadRequest();
            }
            return Ok(jogadorAAtualizar.Id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "0")]
        public IActionResult DeleteJogador(Guid id)
        {
            jogadorServices.RemoverJogador(id);
            return NoContent();
        }
    }
}