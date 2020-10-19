namespace Domain
{
    public class FabricaDeCampeonato
    {
        public ICampeonato CriarCampeonato()
        {
            return new CampeonatoBrasileirao(new PartidaCampeonatoBrasileirao(), 
                                            new RodadaCampeonatoBrasileirao(),
                                            new JogadorArtilheiroCampeonatoBrasileirao());
        }
    }
}