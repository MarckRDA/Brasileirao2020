
namespace Domain
{
    public sealed class TimeCampeonatoBrasileirao : Time
    {
        public TimeCampeonatoBrasileirao(string nomeTime) : base(nomeTime)
        {
            Tabela = new TabelaDeEstatisticaTimeCampeonatoBrasileirao();
        }
    }
}