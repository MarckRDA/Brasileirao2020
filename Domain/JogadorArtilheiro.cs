using System;

namespace Domain
{
    public abstract class JogadorArtilheiro : IJogador
    {
        public Guid Id { get; private set; } = new Guid();
        private string nome;
        private int gol = 0;

        public string NomeTime { get; private set; }
        protected JogadorArtilheiro(){}

        public void AdicionarNomeTimeJogadorArtilheiro(string nomeTime)
        {
            NomeTime = nomeTime;            
        }
        public void AdicionarGols(int gols)
        {
            for (int i = 0; i < gols; i++)
            {
                this.MarcarGol();
            }
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