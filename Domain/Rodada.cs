using System.Collections.Generic;

namespace Domain
{
    public abstract class Rodada
    {
        
        private List<Partida> partidas { get; set; } = new List<Partida>();
        public IReadOnlyCollection<Partida> Partidas => partidas;

        public void AdicionarPartida(Partida partida)
        {
            partidas.Add(partida);
        }

        public void LimparListaDePartidas()
        {
            partidas.Clear();
        }

        
    }
}