using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain.src.Jogadores;
using Domain.src.TabelaEstatistica;

namespace Domain.src.Times
{
    public abstract class Time
    {
        public Guid Id { get; private set; } = new Guid();
        public string NomeTime {get; private set;}
        private List<Jogador> jogadores { get; set;} = new List<Jogador>();
        public IReadOnlyCollection<Jogador> Jogadores => jogadores;
        
        [NotMapped]
        public TabelaDeEstatisticaTime Tabela{get; set;}
        public Guid TabelaId {get; set;} 
     
        public Time(string nomeTime)
        {
            NomeTime = nomeTime;
            Id = Guid.NewGuid();     
        }

        public void ModificarNomeTime(string novoNomeTime)
        {
            NomeTime = novoNomeTime;
        }
     
        public bool AdicionarJogador(Jogador jogador)
        {
            if (jogadores.Exists(j => j.Id == jogador.Id)) return false;
            
            if (jogadores.Count > 32 ) return false;
            jogador.TimeId = Id;
            jogadores.Add(jogador);
            return true;
        }

        public bool RemoverJogador(Jogador jogador)
        {
            if (!jogadores.Exists(j => j.Id == jogador.Id)) return false;
            
            if (jogadores.Count < 16) return false;
            
            jogadores.Remove(jogador);
            
            return true;
        }

        public bool AdicionarListaDeJogadores(List<Jogador> jogadores)
        {
            if (jogadores.Count < 16 || jogadores.Count > 32 ) return false;
            jogadores.Select(j => j.TimeId=Id);            
            this.jogadores = jogadores;
            return true;
        }

        public (bool isValid, List<string> errors) Validar()
        {
            var erros = new List<string>();

            if (!ValidarNomeTime())
            {
                erros.Add("Nome inválido");    
            }

            return(erros.Count == 0, erros);
        }

        private bool ValidarNomeTime()
        {
            if (string.IsNullOrEmpty(NomeTime) || string.IsNullOrWhiteSpace(NomeTime) || NomeTime.StartsWith(" ") || NomeTime.EndsWith(" ")) return false;

            if(NomeTime.Any(char.IsDigit) || NomeTime.Any(char.IsSymbol) || NomeTime.Any(char.IsNumber)) return false;

            return true;
        }
    }
}