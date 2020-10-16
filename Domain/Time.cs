using System.Collections.Generic;

namespace Domain
{
    public class Time
    {
        public string NomeTime {get; private set;}
        private List<Jogador> jogadores { get; set;}
        public IReadOnlyCollection<Jogador> Jogadores => jogadores;
        public int Pontuacao { get; private set;}
        public int PartidasDisputadas { get; private set;} 
        public int Vitorias { get; private set; }   
        public int Derrotas { get; private set; }   
        public int Empate { get; private set;} 
        public int GolsPro { get; private set;}
        public int GolsContra { get; private set;} 
        public double PercentagemAproveitamento { get; private set;}

        public Time(string nomeTime)
        {
            NomeTime = nomeTime;
            jogadores = new List<Jogador>();
        }

        public void DisputarPartida()
        {
            PartidasDisputadas++;
        }

        public bool MarcarVitoria()
        {
            Vitorias++;
            return true;
        }
        
        public bool MarcarDerrota()
        {
            Derrotas++;
            return true;
        }

        public bool MarcarEmpate()
        {
            Empate++;
            return true;
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
            if (this.MarcarVitoria())
            {
                Pontuacao +=3;
            }
            else if (this.MarcarEmpate())
            {
                Pontuacao +=1;
            }
        }

        public bool AdicionarJogador(Jogador jogador)
        {
            if (jogadores.Count < 16 || jogadores.Count > 32 )
            {
                return false;
            }

            jogadores.Add(jogador);
            return true;
        }

        public bool RemoverJogador(Jogador jogador)
        {
            if (jogadores.Count < 16 || jogadores.Count > 32 )
            {
                return false;
            }

            jogadores.Remove(jogador);
            return true;
        }

        public override string ToString()
        {
            return $"{NomeTime} - {Pontuacao} - {PartidasDisputadas} - {Vitorias} - {Empate} - {Derrotas} - {GolsPro} - {GolsContra} - {PercentagemAproveitamento}";
        }

    }
}