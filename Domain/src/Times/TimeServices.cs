using System;
using System.Collections.Generic;
using System.Linq;
using Domain.src.Jogadores;

namespace Domain.src.Times
{
    public class TimeServices
    {
        public List<TimeDTO> ObterTimes() 
        {
            return RepositorioTimes.ObterTimes().Select(t => new TimeDTO
            {
                Id = t.Id,
                NomeTime = t.NomeTime,
                Jogadores = t.Jogadores.Select(j => new JogadorDTO {Id = j.Id, Nome = j
                .Nome, IdTime = j.Id, Gol = j.Gol}).ToList(),
                Tabela = t.Tabela
            }).ToList();
        }

        public TimeDTO ObterTime(Guid idTime)
        {
            var timeRecuperado = RepositorioTimes.ObterTime(idTime);
            return new TimeDTO
            {
                Id = timeRecuperado.Id,
                NomeTime = timeRecuperado.NomeTime,
                Jogadores = timeRecuperado.Jogadores.Select(j => new JogadorDTO{Id = j.Id, Nome = j.Nome,Gol = j.Gol}).ToList(),
                Tabela = timeRecuperado.Tabela
            };
        }

        public List<JogadorDTO> ObterJogadoresDoTime(Guid idTime) => RepositorioTimes.ObterJogadoresDoTime(idTime).Select(j => new JogadorDTO{Id = j.Id, Nome = j.Nome, Gol = j.Gol}).ToList();
        
        public JogadorDTO ObterJogadorDoTime(Guid idTime, Guid idJogador)
        {
            var jogadorRecuperado = RepositorioTimes.ObterJogadorDoTime(idTime, idJogador);
            return new JogadorDTO
            {
                Id = jogadorRecuperado.Id,
                Nome = jogadorRecuperado.Nome,
                Gol = jogadorRecuperado.Gol
            };
        }

        public bool AdicionarTime(string nomeTime)
        {
            var novoTime = new TimeCampeonatoBrasileirao(nomeTime);
            if (!novoTime.Validar().isValid)
            {
                return false;
            }
            
            RepositorioTimes.AdicionarTime(novoTime);
            return true;
        }

        public void RemoverTime(Guid idTime)
        {
            RepositorioTimes.RemoverTime(idTime);
        }

        public bool AdicionarJogadorAoTime(Guid idTime, string nomeJogador)
        {
            var jogadorAAdicionar = new JogadorTime(nomeJogador);
            if (!jogadorAAdicionar.Validar().isValid)
            {
                return false;
            }
            return RepositorioTimes.AdicionarJogadorAoTime(idTime, jogadorAAdicionar);
        }

        public bool RemoverJogadorDoTime(Guid idTime, Guid idJogador)
        {
            return RepositorioTimes.RemoverJogadorDoTime(idTime, idJogador);
        }

        public bool ModificarTime(Guid idTime, string nomeNovoTime)
        {
            var timeAAtualizar = RepositorioTimes.ObterTime(idTime);
            var nomeAntigo = timeAAtualizar.NomeTime;

            timeAAtualizar.ModificarNomeTime(nomeNovoTime);
            if (!timeAAtualizar.Validar().isValid)
            {
                timeAAtualizar.ModificarNomeTime(nomeAntigo);
                return false;
            }

            return true;
        }


        public bool ModificarNomeJogador(Guid idTime, Guid idJogador, string nomeNovoJogador)
        {
            var jogadorAAtualizar = RepositorioTimes.ObterJogadorDoTime(idTime, idJogador);
            var nomeAntigo = jogadorAAtualizar.Nome;
            jogadorAAtualizar.AdicionarNomeJogador(nomeNovoJogador);
            if (!jogadorAAtualizar.Validar().isValid)
            {
                jogadorAAtualizar.AdicionarNomeJogador(nomeAntigo);
                return false;
            }
            
            return true;
        }
    }
}