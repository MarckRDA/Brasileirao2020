using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.src;
using WebAPI.Repositorio;
using System;

namespace WebAPI.Controllers.Jogadores
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class JogadoresController : ControllerBase
    {
        [HttpGet]
        public List<Jogador> GetJogadores()
        {
            return RepositorioJogadores.ObterJogadores();
        }

        [HttpGet("{id}")]
        public Jogador GetJogador(Guid id)
        {
            return RepositorioJogadores.ObterJogador(id);
        }

        [HttpPost]
        public IActionResult Post (JogadorRequest request)
        {
           var jogadorAGravar = new JogadorTime(request.Nome);
           RepositorioJogadores.GravarJogador(jogadorAGravar);
           return CreatedAtAction(nameof(GetJogador), new {id = jogadorAGravar}, jogadorAGravar);
        }

        [HttpPut("{id}")]
        public IActionResult PutJogador(Guid id, JogadorRequest jogador)
        {
            var jogadorRecuperado = RepositorioJogadores.ObterJogador(id);
            jogadorRecuperado.AdicionarNomeJogador(jogador.Nome);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJogador(Guid id)
        {
            RepositorioJogadores.RemoverJogador(id);
            return NoContent();
        }
    }
}