namespace Domain
{
    public class FabricaDeCampeonato
    {
        // Só é um desing pattern
        public ICampeonato CriarCampeonato()
        {
            return new CampeonatoBrasileirao();
        }
    }
}