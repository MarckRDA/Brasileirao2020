using Domain.src.Users;

namespace WebAPI.Users
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }        
    }
}