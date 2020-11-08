using System;
using System.Linq;

namespace Domain.src.Users
{
    public abstract class Usuario
    {
        protected Guid Id { get; private set; } = new Guid();
        protected string Name {get; private set;}

        public Usuario(string nome)
        {
            Id = Guid.NewGuid();
            Name = nome;
        }

        public bool ValidarNomeUsuario()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name) || Name.StartsWith(" ") || Name.EndsWith(" ")) return false;

            if(Name.Any(char.IsDigit) || Name.Any(char.IsSymbol) || Name.Any(char.IsNumber)) return false;

            return true;
        }
    }
}