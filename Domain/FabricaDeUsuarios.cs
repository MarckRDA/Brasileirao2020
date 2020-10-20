namespace Domain
{
    public class FabricaDeUsuarios
    {
        public Usuario CriarUsuario(string TipoDeUsuario)
        {
            if (TipoDeUsuario == "admin")
            {
                return new CBF();
            }
            else if (TipoDeUsuario == "torcedor")
            {
                return new Torcedor();
            }

            return null;
        }
    }
}