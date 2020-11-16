using System;
using System.Collections.Generic;

namespace Domain.src.Users
{
    public class UsuarioServices
    {
        public Usuario CriarUsuario(string senha, string nome, string tipo)
        {
            if (tipo == "cbf")
            {
                var novoAdmin = new CBF(nome, senha, tipo);
                if (novoAdmin.Validar().eValido)
                {
                    UsuarioRepositorio.AdicionarUsuario(novoAdmin);
                    return novoAdmin;
                }

                return null;
            }
            else if (tipo == "torcedor")
            {
                var novoTorcedor = new Torcedor(nome, senha, tipo);
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

        public bool AdicionarUsuario(string nome, string senha, string tipo)
        {
            var novoUsuario = CriarUsuario(senha, nome, tipo);
            
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