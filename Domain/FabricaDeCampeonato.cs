namespace Domain
{
    public class FabricaDeCampeonato
    {
        public ICampeonato CriarCampeonato()
        {
            return new CampeonatoBrasileirao();
        }
    }
}