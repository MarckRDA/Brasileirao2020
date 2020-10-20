using System;
using System.Collections.Generic;

namespace Domain
{
    public abstract class Time
    {
        public Guid Id { get; private set; } = new Guid();
        public string NomeTime {get; private set;}
        private List<Jogador> jogadores { get; set;} = new List<Jogador>();
        public IReadOnlyCollection<Jogador> Jogadores => jogadores;
        public TabelaDeEstatisticaTime Tabela{get; set;}
     
        protected Time(string nomeTime)
        {
            NomeTime = nomeTime;
            jogadores = new List<Jogador>();
            Id = Guid.NewGuid();     
        }
     
        public bool AdicionarJogador(Jogador jogador)
        {
            if (jogadores.Count < 16 && jogadores.Count > 32 )
            {
                return false;
            }
            jogadores.Add(jogador);
            return true;
        }

        public bool RemoverJogador(Jogador jogador)
        {
            if (jogadores.Count < 16 && jogadores.Count > 32 )
            {
                return false;
            }

            jogadores.Remove(jogador);
            return true;
        }

        public bool AdicionarListaDeJogadores(List<Jogador> jogadores)
        {
            if (jogadores.Count < 16 && jogadores.Count > 32 )
            {
                return false;
            }

            this.jogadores = jogadores;
            return true;
        }
    }
}