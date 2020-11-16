using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.src.Jogadores
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

        private bool ValidarNomeJogador()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome) || Nome.StartsWith(" ") || Nome.EndsWith(" ")) return false;

            if(Nome.Any(char.IsDigit) || Nome.Any(char.IsSymbol) || Nome.Any(char.IsNumber)) return false;

            return true;
        }

        public (bool isValid, List<string> errors) Validar()
        {
            var erros = new List<string>();

            if (!ValidarNomeJogador())
            {
                erros.Add("Nome innválido");    
            }

            return(erros.Count == 0, erros);
        }
    }
}