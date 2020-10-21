using System.Collections.Generic;

namespace Domain
{
    public abstract class Rodada
    {
        
        private List<Partida> partidas { get; set; } = new List<Partida>();
        public IReadOnlyCollection<Partida> Partidas => partidas;

        public void AdicionarPartida(List<Partida> partidas)
        {
            this.partidas = partidas;
        }

        public void LimparListaDePartidas()
        {
            partidas.Clear();
        }

        
    }
}