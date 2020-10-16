namespace Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public string Posicao { get; private set;}
        public int QtGols { get; private set; }

        public Jogador(string nome, string posicao)
        {
            Nome = nome;
            Posicao = posicao;
        }

        public void MarcarGol()
        {
            QtGols++;
        }
    }
}