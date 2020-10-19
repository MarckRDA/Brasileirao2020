namespace Domain
{
    public abstract class Usuario
    {
        public string Nome { get; set; }

        public Usuario(string nome)
        {
            Nome = nome;
        }
    }
}