using Domain.src.Campeonato;


namespace Domain.src.Factories
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