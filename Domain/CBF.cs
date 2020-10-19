namespace Domain
{
    public sealed class CBF : IUsuario
    {
        public string Nome { get; private set; }
        public CBF (string nome)
        {
            Nome = nome;
        }
    }
}