using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.src.Jogadores
{
    public class JogadorServices
    {
        public JogadorDTO CriarJogador(string nome)
        {
            var novoJogador = new JogadorTime(nome);
            if (!novoJogador.Validar().isValid)
            {
                return null;    
            }
            RepositorioJogadores.GravarJogador(novoJogador);
            return new JogadorDTO
            {
                Id = novoJogador.Id,
                Nome = novoJogador.Nome,
                Gol = novoJogador.Gol
            };
        }

        public List<JogadorDTO> ObterJogadores() => RepositorioJogadores.ObterJogadores().Select(j => new JogadorDTO
                                                                                                {
                                                                                                    Id = j.Id,
                                                                                                    Nome = j.Nome,
                                                                                                    Gol = j.Gol
                                                                                                }).ToList();

        public JogadorDTO ObterJogador(Guid idJogador)
        {
            var jogadorObtido = RepositorioJogadores.ObterJogador(idJogador);
            return new JogadorDTO
            {
                Id = jogadorObtido.Id,
                Nome = jogadorObtido.Nome,
                Gol = jogadorObtido.Gol
            };
        }

        public JogadorDTO AtualizarJogador(Guid idJogador, string novoNome)
        {
            var jogadorAAtualizar = RepositorioJogadores.AtualizarJogador(idJogador, novoNome);
            
            if (jogadorAAtualizar == null)
            {
                return null;
            }

            return new JogadorDTO 
            {
                Id = jogadorAAtualizar.Id,
                Nome = jogadorAAtualizar.Nome
            };
        }

        public void RemoverJogador(Guid idJogador)
        {
            RepositorioJogadores.RemoverJogador(idJogador);
        }
                                        
    }
}