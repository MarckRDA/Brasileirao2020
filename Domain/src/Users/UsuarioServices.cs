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
                if (novoAdmin.Validar().eValido)
                {
                    UsuarioRepositorio.AdicionarUsuario(novoAdmin);
                    return novoAdmin;
                }

                return null;
            }
            else if (senha == "torcedor")
            {
                var novoTorcedor = new Torcedor(nome);
                if (novoTorcedor.Validar().eValido)
                {
                    UsuarioRepositorio.AdicionarUsuario(novoTorcedor);
                    return novoTorcedor;
                }

                return null;

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