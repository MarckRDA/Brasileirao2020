using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Domain.src.Jogadores;

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
        public List<JogadorDTO> GetJogadores()
        {
            
            return jogadorServices.ObterJogadores();
        }

        [HttpGet("{id}")]
        public JogadorDTO GetJogador(Guid id)
        {
            return jogadorServices.ObterJogador(id);
        }

        [HttpPost]
        public IActionResult Post (JogadorRequest request)
        {
           var jogadorAGravar = jogadorServices.CriarJogador(request.Nome);
           return CreatedAtAction(nameof(GetJogador), new {id = jogadorAGravar}, jogadorAGravar);
        }

        [HttpPut("{id}")]
        public IActionResult PutJogador(Guid id, JogadorRequest request)
        {
            jogadorServices.ModificarNomeJogador(id, request.Nome);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJogador(Guid id)
        {
            jogadorServices.RemoverJogador(id);
            return NoContent();
        }
    }
}