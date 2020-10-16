namespace Domain
{
    public abstract class Jogador
    {
        public string Nome { get; private set; }
        public int Gol { get; private set;}
        public Jogador(string nome)
        {
            Nome = nome;
        }

        public void MarcarGol()
        {
            Gol++;
        }
     
    }
}