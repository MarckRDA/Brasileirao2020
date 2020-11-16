using Domain.src.TabelaEstatistica;

namespace Domain.src.Times
{
    public sealed class TimeCampeonatoBrasileirao : Time
    {
        public TimeCampeonatoBrasileirao(string nomeTime) : base(nomeTime)
        {
            Tabela = new TabelaDeEstatisticaTimeCampeonatoBrasileirao();
        }
    }
}