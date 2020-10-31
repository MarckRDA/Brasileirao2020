using WebAPI.Controllers.Jogadores;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.src;
using WebAPI.Repositorio;
using System.Linq;
using System;

namespace Brasileiras2020.WebAPI.Controllers.Jogadores
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

        [HttpPut("{id}")]
        public Jogador PutJogador(Guid id, Jogador jogador)
        {
            var jogadorRecuperado = RepositorioJogadores.ObterJogador(id);
            jogadorRecuperado.AdicionarNomeJogador(jogador.Nome);
            return jogadorRecuperado;
        }

        [HttpPost]
        public string Post (JogadorRequest request)
        {
           var Jogador = new JogadorTime(request.Nome);
           return Jogador.Nome;
        }
    }
}