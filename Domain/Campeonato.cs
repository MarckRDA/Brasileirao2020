using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public abstract class Campeonato
    {
        private List<Time> times { get; set; } = new List<Time>();
        public IReadOnlyCollection<Time> Times => times;
        private bool inicioCampeonato = false;
        public int Rodada { get; private set; } = 1;
        private List<JogadorArtilheiro> jogadoresArtilheiros = new List<JogadorArtilheiro>();
        private List<int> logDePartidas = new List<int>();

        // *<------------------- Metodos de operações externas a classe --------------------------->
        public void CadastrarTimes(Usuario usuario, List<Time> times)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não tem permissão para executar essa operação!!");
            }

            if (inicioCampeonato)
            {
                throw new PermissaoNegadaException("Você não pode fazer essa operação. O campeonato já começou!");
            }

            if (times.Count < 7)
            {
                throw new LimiteNaoPermitidoException("Deverá inscrever mais de 7 times para o campeonato!!");
            }

            this.times = times;

            inicioCampeonato = true;
        }

        public bool RemoverJogadorTime(Usuario usuario, Guid idTime, Jogador jogador)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!"); 
            }
            return times.FirstOrDefault(time => time.Id == idTime).RemoverJogador(jogador);
        }

        public bool AdicionarJogadorTime (Usuario usuario, Guid idTime, Jogador jogador)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!"); 
            }
            return times.FirstOrDefault(time => time.Id == idTime).AdicionarJogador(jogador);
        }

        public List<string> ApresentarTabela(Usuario usuario)
        {
            var listaTabelaDeClassificacao = new List<string>();
            times.OrderByDescending(time => time.Pontuacao);

            foreach (var time in times)
            {
                listaTabelaDeClassificacao.Add(time.ToString());
            }

            return listaTabelaDeClassificacao;
        }
        public List<string> ExibirTimesClassificadosLibertadores(Usuario usuario) => ApresentarTabela(usuario).Take(4).ToList();

        public List<string> ExibirTimesRebaixados(Usuario usuario)
        {
            var rebaixados = new List<string>();

            for (int i = -4; i < 0; i++)
            {
                rebaixados.Add(ApresentarTabela(usuario)[i]);
            }

            return rebaixados;
        }

        public List<string> ExibirClassificacaoDeArtilheiros()
        {
            var listaDeGolsTime = new List<int>();
            var jogadoresArtilheiros = new List<JogadorArtilheiro>();
            var tabelaArtilheiros = new List<string>();
            var nomeJogadorGoleador = "";
            var qtdGolsJogadorGoleador = 0;

            for (int i = 0; i < times.Count; i++)
            {
                for (int j = 0; j < times.ElementAt(i).Jogadores.Count; j++)
                {
                    listaDeGolsTime.Add(times.ElementAt(i).Jogadores.ElementAt(j).Gol);
                }

                qtdGolsJogadorGoleador = listaDeGolsTime.Max();
                nomeJogadorGoleador = times.ElementAt(i).Jogadores.FirstOrDefault(x => x.Gol == qtdGolsJogadorGoleador).Nome;

                var jogadorArtilheiro = new JogadorArtilheiroCampeonato(nomeJogadorGoleador, qtdGolsJogadorGoleador, times.ElementAt(i).NomeTime);

                jogadoresArtilheiros.Add(jogadorArtilheiro);
                listaDeGolsTime.Clear();
            }

            jogadoresArtilheiros.OrderByDescending(jogador => jogador.Gol);
            tabelaArtilheiros = jogadoresArtilheiros.Select((jogadorArtilheiro, index) => $"{index + 1} {jogadorArtilheiro.Nome} - Gols: {jogadorArtilheiro.Gol} - Time: {jogadorArtilheiro.NomeTime}").ToList();

            return tabelaArtilheiros;
        }

        public List<string> ExibirResultadoDaRodada(Usuario usuario, int qtdRodadas)
        {
            var resultadosDaPartida = GerarRodada(qtdRodadas);
            var listaResultados = new List<string>();
            for (int i = 0; i < resultadosDaPartida.Count; i++)
            {
                var resultadosAMostrar = $"Rodada {resultadosDaPartida[i].rodada} Resultado: {resultadosDaPartida[i].Item1.anfitriao} {resultadosDaPartida[i].Item1.golsAnfitriao} X {resultadosDaPartida[i].Item1.golsVisitante} {resultadosDaPartida[i].Item1.visitante}/n";
                listaResultados.Add(resultadosAMostrar);
            }
            return listaResultados;
        }

        // *<------------------- Metodos de operações internas a classe --------------------------->
        private List<((string anfitriao, int golsAnfitriao, string visitante, int golsVisitante), int rodada)> GerarRodada(int qtdRodadas)
        {
            var listaResultadosRodada = new List<((string anfitriao, int golsAnfitriao, string visitante, int golsVisitante), int rodada)>();
            while (qtdRodadas <= Rodada)
            {
                Rodada++;

                var partidas = GerarProximoConfronto();

                for (int i = 0; i < partidas.Count; i++)
                {
                    var timeAnfitriaoGols = GeradorGols();
                    var timeVisitanteGols = 6 - timeAnfitriaoGols;
                    var timesEmPartida = partidas.ElementAt(i);

                    if (timeAnfitriaoGols > timeVisitanteGols)
                    {
                        GeradorJogadoresGoleadores(timesEmPartida.anfitriao, timesEmPartida.visitante, timeAnfitriaoGols);
                        timesEmPartida.anfitriao.MarcarVitoria();
                        timesEmPartida.anfitriao.MarcarPontuacao();
                        timesEmPartida.visitante.MarcarDerrota();
                        listaResultadosRodada.Add(((timesEmPartida.anfitriao.NomeTime, timeAnfitriaoGols, timesEmPartida.visitante.NomeTime, timeVisitanteGols), Rodada));
                    }
                    else if (timeAnfitriaoGols < timeVisitanteGols)
                    {
                        GeradorJogadoresGoleadores(timesEmPartida.visitante, timesEmPartida.anfitriao, timeVisitanteGols);
                        timesEmPartida.anfitriao.MarcarDerrota();
                        timesEmPartida.visitante.MarcarVitoria();
                        timesEmPartida.visitante.MarcarPontuacao();
                        listaResultadosRodada.Add(((timesEmPartida.anfitriao.NomeTime, timeAnfitriaoGols, timesEmPartida.visitante.NomeTime, timeVisitanteGols), Rodada));
                    }
                    else
                    {
                        for (int j = 0; j < timeAnfitriaoGols; j++)
                        {
                            timesEmPartida.anfitriao.MarcarGolsPro();
                            timesEmPartida.visitante.MarcarGolsPro();
                        }

                        timesEmPartida.anfitriao.MarcarEmpate();
                        timesEmPartida.anfitriao.MarcarPontuacao();
                        timesEmPartida.visitante.MarcarEmpate();
                        timesEmPartida.visitante.MarcarPontuacao();
                        listaResultadosRodada.Add(((timesEmPartida.anfitriao.NomeTime, timeAnfitriaoGols, timesEmPartida.visitante.NomeTime, timeVisitanteGols), Rodada));
                    }

                }

            }

            return listaResultadosRodada;
        }
        private List<(Time anfitriao, Time visitante)> GerarProximoConfronto()
        {
            Random sorteador = new Random();
            var quemJogaComQuem = new List<int>();

            var partida = new List<(Time anfitriao, Time visitante)>();

            while (quemJogaComQuem.Count <= times.Count)
            {
                int getNumber = sorteador.Next(0, times.Count - 1);

                if (!quemJogaComQuem.Contains(getNumber))
                {
                    quemJogaComQuem.Add(getNumber);
                }
            }

            for (int i = 0; i < times.Count; i += 2)
            {
                partida.Add((times[quemJogaComQuem[i]], times[quemJogaComQuem[i + 1]]));
            }

            return partida;
        }

        private int GeradorGols()
        {
            var geradorGols = new Random();

            return geradorGols.Next(0, 6);
        }

        private void GeradorJogadoresGoleadores(Time timeVencedor, Time timePerdedor, int golFeitos)
        {
            var gerador = new Random();
            var idTime = timeVencedor.Id;

            for (int i = 0; i < golFeitos; i++)
            {
                var indexJogador = gerador.Next(0, timeVencedor.Jogadores.Count);
                var jogadorGoleador = times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador);

                if (jogadorGoleador.Nome == "Gol Contra")
                {
                    timePerdedor.MarcarGolsPro();
                    timeVencedor.MarcarGolsContra();
                    continue;
                }

                times.Find(i => i.Id == idTime).MarcarGolsPro();
                times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador).MarcarGol();
            }

        }


    }
}