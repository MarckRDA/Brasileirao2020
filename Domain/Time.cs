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
        public int Pontuacao { get; private set;}
        public int PartidasDisputadas { get; private set;} 
        public int Vitorias { get; private set; } 
        private bool vitoria = false;  
        public int Derrotas { get; private set; }   
        public int Empate { get; private set;} 
        private bool empate = false;
        public int GolsPro { get; private set;}
        public int GolsContra { get; private set;} 
        public double PercentagemAproveitamento { get; private set;}

        protected Time(string nomeTime)
        {
            NomeTime = nomeTime;
            jogadores = new List<Jogador>();
            Id = Guid.NewGuid();        
        }

        public void DisputarPartida()
        {
            PartidasDisputadas++;
        }

        public void MarcarVitoria()
        {
            Vitorias++;
            vitoria = true;
            empate = false;
        }
        
        public void MarcarDerrota()
        {
            Derrotas++;
        }

        public void MarcarEmpate()
        {
            Empate++;
            vitoria = false;
            empate = true;
        }

        public void MarcarGolsContra()
        {
            GolsContra++;
        }

        public void MarcarGolsPro()
        {
            GolsPro++;
        }

        public void AtualizarPerctAproveitamento()
        {
            PercentagemAproveitamento = (Pontuacao/(PartidasDisputadas * 3))*100;
        }

        public void MarcarPontuacao()
        {
            if (vitoria)
            {
                Pontuacao +=3;
            }
            else if (empate)
            {
                Pontuacao +=1;
            }
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

        public override string ToString()
        {
            return $"{NomeTime} | {Pontuacao} | {PartidasDisputadas} | {Vitorias} | {Empate} | {Derrotas} | {GolsPro} | {GolsContra} | {Math.Round(PercentagemAproveitamento, 2)}%";
        }

    }
}