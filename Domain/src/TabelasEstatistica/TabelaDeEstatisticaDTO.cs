namespace Domain.src.TabelaEstatistica
{
    public class TabelaDeEstatisticaDTO
    {
        public int Pontuacao { get; private set;}
        public int PartidasDisputadas { get; private set;} 
        public int Vitorias { get; private set; } 
        public int Derrotas { get; private set; }   
        public int Empate { get; private set;} 
        public int GolsPro { get; private set;}
        public int GolsContra { get; private set;} 
        public int SaldoDeGols {get; private set;}
        public double PercentagemAproveitamento { get; private set;} = 0.0;
    }
}