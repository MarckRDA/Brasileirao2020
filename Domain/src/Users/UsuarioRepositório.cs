using System;
using System.Collections.Generic;
using System.Linq;
using Doamin.src.Users;
using Domain.src.Infra;

namespace Domain.src.Users
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public void AdicionarUsuario(Usuario usuario)
        {
            using (var _context = new BrasileiraoContext())
            {
                _context.Add(usuario);
                _context.SaveChanges();
            }

        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            using (var _context = new BrasileiraoContext())
            {
                return _context.Usuarios.ToList();
            }
        }

        public Usuario ObterUsuario(Guid idUser)
        {
            using (var _context = new BrasileiraoContext())
            {
                return _context.Usuarios.FirstOrDefault(u => u.Id == idUser);
            }
        }

        public void RemoverUsuario(Guid idUsuario)
        {
            using (var _context = new BrasileiraoContext())
            {
                var usuarioARemover = _context.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
                _context.Usuarios.Remove(usuarioARemover);
                _context.SaveChanges();
            }

        }

    }
}