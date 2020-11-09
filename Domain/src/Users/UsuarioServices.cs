using System;
using System.Collections.Generic;

namespace Domain.src.Users
{
    public class UsuarioServices
    {
        public Usuario CriarUsuario(string senha, string nome)
        {
            if (senha == "admin")
            {
                var novoAdmin = new CBF(nome);
                UsuarioRepositorio.AdicionarUsuario(novoAdmin);
                return novoAdmin;
            }
            else if (senha == "torcedor")
            {
                var novoTorcedor = new Torcedor(nome);
                UsuarioRepositorio.AdicionarUsuario(novoTorcedor);
                return novoTorcedor;
            }

            return null;
        }

        public Usuario ObterUsuario(Guid idUser)
        {
            return UsuarioRepositorio.ObterUsuario(idUser);
        }

        public bool AdicionarUsuario(string nome, string senha)
        {
            var novoUsuario = CriarUsuario(senha, nome);
            
            if (novoUsuario == null)
            {
                return false;   
            }

            UsuarioRepositorio.AdicionarUsuario(novoUsuario);
            return true;
        }

        public void RemoverUsuario(Guid idUser)
        {
            UsuarioRepositorio.RemoverUsuario(idUser);
        }
    }
}