using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.src.Users
{
    public class Usuario
    {
        public Guid Id { get; private set; } = new Guid();
        public string Name {get; private set;}
        public Perfil Perfil { get; set; }        
        public string Senha { get; private set; }

        public Usuario(string nome, string senha, Perfil perfil)
        {
            Id = Guid.NewGuid();
            Name = nome;
            Senha = senha;
            Perfil = perfil;
        }

        public Usuario()
        {
            
        }

        private bool ValidarNomeUsuario()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name) || Name.StartsWith(" ") || Name.EndsWith(" ")) return false;

            if(Name.Any(char.IsDigit) || Name.Any(char.IsSymbol) || Name.Any(char.IsNumber)) return false;

            return true;
        }

       public (List<string> erros, bool eValido) Validar()
       {
           var erros = new List<string>();

           if (!ValidarNomeUsuario())
           {
                erros.Add("Nome inválido");    
           }

           return (erros, erros.Count==0);
       }
    }
}