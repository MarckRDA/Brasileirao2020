using System;

namespace Domain
{
    public interface IJogador
    {

        public void MarcarGol();
        public void AdicionarNomeJogador(string nome);
        public int MostrarGols();
        public string MostrarNome();
        public Guid MostrarID();

    }
}