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

        public string ModificarNomeJogador(Guid idJogador, string novoNome)
        {
            return RepositorioJogadores.ModificarNomeJogador(idJogador, novoNome).Nome;
        }

        public void RemoverJogador(Guid idJogador)
        {
            RepositorioJogadores.RemoverJogador(idJogador);
        }
                                        
    }
}