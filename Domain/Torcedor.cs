namespace Domain
{
    public sealed class Torcedor : IUsuario
    {
        public string Nome { get; private set; }
        public Torcedor(string nome)
        {
            Nome = nome;
        }


    }
}