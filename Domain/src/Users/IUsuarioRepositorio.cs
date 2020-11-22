using System;
using System.Collections.Generic;
using Domain.src.Users;

namespace Doamin.src.Users
{
    public interface IUsuarioRepositorio
    {
        void AdicionarUsuario(Usuario usuario);
        IEnumerable<Usuario> ObterUsuarios();
        Usuario ObterUsuario(Guid idUser);
        void RemoverUsuario(Guid idUser);        
    }
}