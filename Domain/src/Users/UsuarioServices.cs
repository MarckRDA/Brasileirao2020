using System;
using Doamin.src.Users;

namespace Domain.src.Users
{
    public class UsuarioServices
    {
        private IUsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public Usuario CriarUsuario(string senha, string nome, Perfil perfil)
        {
            var novoAdmin = new Usuario(nome, senha, perfil);
            if (novoAdmin.Validar().eValido)
            {
                _usuarioRepositorio.AdicionarUsuario(novoAdmin);
                return novoAdmin;
            }

            return null;
        }

        public Usuario ObterUsuario(Guid idUser)
        {
            return _usuarioRepositorio.ObterUsuario(idUser);
        }

        public (bool isValid, Guid id) AdicionarUsuario(string nome, string senha, Perfil perfil)
        {
            var novoUsuario = CriarUsuario(senha, nome, perfil);

            if (novoUsuario == null)
            {
                return (false, Guid.Empty);
            }

            return (true, novoUsuario.Id);
        }

        public void RemoverUsuario(Guid idUser)
        {
            _usuarioRepositorio.RemoverUsuario(idUser);
        }
    }
}