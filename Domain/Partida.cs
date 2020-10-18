namespace Domain
{
    public abstract class Partida
    {
        public Time TimeAnfitriao { get; private set; }
        public Time TimeVisitante { get; private set; }
        public int GolsAnfitriao { get; private set; }
        public int GolsVisitante { get; private set; }

        
        public void AdicionarTimeAnfitriaoAPartida(Time timeAnfitriao)
        {
            TimeAnfitriao = timeAnfitriao;
        }

        public void AdicionarTimeVisitanteAPartida(Time timeVisitante)
        {
            TimeAnfitriao = timeVisitante;
        }
        public void MarcarGolAnfitriao()
        {
            GolsAnfitriao++;
        }

        public void MarcarGolVisitante()
        {
            GolsVisitante++;
        }
    }
}