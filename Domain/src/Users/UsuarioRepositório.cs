using System.Collections.Generic;

namespace Domain.src.Users
{
    public static class UsuarioRepositorio
    {
        private static List<Usuario> usuarios = new List<Usuario>();
        public static IReadOnlyCollection<Usuario> Usuarios => usuarios;

        public static void AdicionarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public static List<Usuario> ObterUsuarios()
        {
            return usuarios;
        }

    }
}