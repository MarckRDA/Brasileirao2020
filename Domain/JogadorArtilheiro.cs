namespace Domain
{
    public abstract class JogadorArtilheiro : Jogador
    {
        public string NomeTime { get; private set; }
        public JogadorArtilheiro(string nome, int gols, string nomeTime) : base(nome)
        {
            NomeTime = nomeTime;
        }
    }
}