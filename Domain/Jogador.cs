namespace Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public Time Time { get; private set; }     
        public Jogador(string nome, Time time)
        {
            Nome = nome;
            Time = time;
        }

     
    }
}