namespace Domain.src
{
    public class FabricaDeUsuarios
    {
        //Só é um desing pattern
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