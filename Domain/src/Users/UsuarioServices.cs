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

        public List<Usuario> ObterUsuarios()
        {
            return UsuarioRepositorio.ObterUsuarios();
        }
    }
}