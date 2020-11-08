using System;
using System.Collections.Generic;
using System.Linq;

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

        public static Usuario ObterUsuario(Guid idUser)
        {
            return usuarios.FirstOrDefault(u => u.Id == idUser);
        }

        public static void RemoverUsuario(Guid idUsuario)
        {
            usuarios.Remove(usuarios.FirstOrDefault(u => u.Id == idUsuario));
        }

    }
}