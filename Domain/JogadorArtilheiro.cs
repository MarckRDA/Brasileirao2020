namespace Domain
{
    public abstract class JogadorArtilheiro : Jogador
    {
        public string NomeTime { get; private set; }
        protected JogadorArtilheiro(){}

        public void AdicionarNomeTimeJogadorArtilheiro(string nomeTime)
        {
            NomeTime = nomeTime;            
        }
        public void AdicionarGols(int gols)
        {
            for (int i = 0; i < gols; i++)
            {
                this.MarcarGol();
            }
        }
    }
}