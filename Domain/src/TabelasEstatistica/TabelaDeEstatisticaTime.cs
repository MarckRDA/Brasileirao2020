using System;

namespace Domain.src.TabelaEstatistica
{
    public abstract class TabelaDeEstatisticaTime 
    {
        public int Pontuacao { get; private set;}
        public int PartidasDisputadas { get; private set;} 
        public int Vitorias { get; private set; } 
        private bool vitoria = false;  
        public int Derrotas { get; private set; }   
        public int Empate { get; private set;} 
        private bool empate = false;
        public int GolsPro { get; private set;}
        public int GolsContra { get; private set;} 
        public int SaldoDeGols {get; private set;}
        public double PercentagemAproveitamento { get; private set;} = 0.0;

      
       
        
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

       public void MarcarSaldoDeGols()
       {
           SaldoDeGols = GolsPro - GolsContra;
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
            PercentagemAproveitamento = (Pontuacao/(PartidasDisputadas * 3.0))*100.0;
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

        

        public override string ToString()
        {
            return $"{Pontuacao}  |  {PartidasDisputadas}  |  {Vitorias}  |  {Empate}  |  {Derrotas}  |  {SaldoDeGols}  |  {GolsPro}  |  {GolsContra}  |   {Math.Round(PercentagemAproveitamento, 2)}%";
        }

    }
}