namespace Domain
{
    public abstract class Usuario
    {
        protected string Nome { get; set; }

        public Usuario(string nome)
        {
            Nome = nome;
        }
    }
}