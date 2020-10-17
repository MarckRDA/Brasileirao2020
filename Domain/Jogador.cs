using System;

namespace Domain
{
    public abstract class Jogador
    {
        public Guid Id { get; private set; } = new Guid();
        public string Nome { get; private set; }
        public int Gol { get; private set;}
        public Jogador(string nome)
        {
            Nome = nome;
            Id = Guid.NewGuid();
        }

        public void MarcarGol()
        {
            Gol++;
        }
     
    }
}