using System;
using System.Collections.Generic;


namespace Domain.src
{
    public interface ICampeonato
    {
        public void CadastrarTimes(Usuario usuario, List<Time> times);
        public bool RemoverJogadorTime(Usuario usuario, Guid idTime, Jogador jogador);
        public bool AdicionarJogadorTime(Usuario usuario, Guid idTime, Jogador jogador);
        public IReadOnlyCollection<Time> ObterListaTimes();
        public IReadOnlyCollection<Partida> ObterTodasAsPartidas();
        public List<string> ApresentarPartidas(Usuario usuario);
        public List<string> MostrarPartidasQueOTimeEnfrenta(string nomeDoTime);
        public bool InscreverResultadoDaPartida(Usuario usuario, int partida, int golsAnfitriao, int golsVisitante);
        public bool RegistrarJogadoresGoleadoresDaPartida(Usuario usuario, string timeVencedor, string nomeJogador, int golFeitos);
        public bool RegistrarRodada(Usuario usuario);
        public List<string> ExibirResultadoDaRodada(Usuario usuario, int qtdRodadas);
        public List<string> ApresentarTabela(Usuario usuario);
        public List<string> ExibirTimesClassificadosLibertadores(Usuario usuario); 
        public List<string> ExibirTimesRebaixados(Usuario usuario);
        public List<string> ExibirClassificacaoDeArtilheiros(Usuario usuario);
        
    }
}