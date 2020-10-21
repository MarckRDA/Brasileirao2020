using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public interface ICampeonato
    {
        public void CadastrarTimes(Usuario usuario, List<Time> times);
        public bool RemoverJogadorTime(Usuario usuario, Guid idTime, Jogador jogador);
        public bool AdicionarJogadorTime(Usuario usuario, Guid idTime, Jogador jogador);
        public List<string> ApresentarTabela(Usuario usuario);
        public List<string> ExibirTimesClassificadosLibertadores(Usuario usuario); 
        public List<string> ExibirTimesRebaixados(Usuario usuario);
        public List<string> ExibirClassificacaoDeArtilheiros(Usuario usuario);
        public List<string> ExibirResultadoDaRodada(Usuario usuario, int qtdRodadas);
        
        public void GerarRodadaMockada(int golsAnfitriao, int golsVisitante); // !Comentar após fim do testes

        //public void GerarProximoConfrontoMocado();  // !Comentar após fim do testes
        public IReadOnlyCollection<Time> ObterListaTimes() ;  // !Comentar após fim do testes


    }
}