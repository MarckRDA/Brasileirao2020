using System;
using System.Linq;

namespace Domain.src
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

        public void AdicionarNomeJogador(string nome)
        {
            Nome = nome;
        }

        public bool ValidarNomeJogador()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome) || Nome.StartsWith(" ") || Nome.EndsWith(" ")) return false;

            if(Nome.Any(char.IsDigit) || Nome.Any(char.IsSymbol) || Nome.Any(char.IsNumber)) return false;

            return true;
        }
    }
}