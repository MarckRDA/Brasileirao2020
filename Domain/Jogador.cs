using System;

namespace Domain
{
    public abstract class Jogador
    {
        protected Guid Id { get; private set; } = new Guid();
        public string Nome { get; private set; }
        public int Gol { get; private set;}
        protected Jogador(string nome)
        {
            Nome = nome;
            Id = Guid.NewGuid();
        }

        protected Jogador(){}

        public void MarcarGol()
        {
            Gol++;
        }

        public void AdicionarNomeJogador(string nome)
        {
            Nome = nome;
        }
    }
}