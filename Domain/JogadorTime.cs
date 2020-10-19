using System;

namespace Domain
{
    public sealed class JogadorTime : IJogador
    {
        public Guid Id { get; private set; } = new Guid();
        private string nome;
        private int gol;
        public JogadorTime(string nome)
        {
            this.nome = nome;
            Id = Guid.NewGuid();
        }

        public void MarcarGol()
        {
            gol++;
        }

        public void AdicionarNomeJogador(string nome)
        {
            this.nome = nome;
        }

        public int MostrarGols()
        {
            return gol;
        }

        public string MostrarNome()
        {
            return nome;
        }

        public Guid MostrarID()
        {
            return Id;
        }
    }
}