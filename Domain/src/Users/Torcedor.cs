namespace Domain.src.Users
{
    public sealed class Torcedor : Usuario
    {
        public Torcedor(string nome, string senha, string tipo) : base(nome, senha, tipo)
        {
        }
    }
}