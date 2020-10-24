using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public sealed class CampeonatoBrasileirao : ICampeonato
    {
        private List<Time> times { get; set; } = new List<Time>();
        public IReadOnlyCollection<Time> Times => times;
        private bool inicioCampeonato = false;
        private int nRodada = 0;
        private List<Partida> partidas = new List<Partida>();
        private List<Rodada> rodadas = new List<Rodada>();
        private List<Partida> todasAsPartidas = new List<Partida>();
        private Partida partida;
        private Rodada rodada = new RodadaCampeonatoBrasileirao();


        // *<------------------- Metodos de operações externas a classe --------------------------->
        public IReadOnlyCollection<Time> ObterListaTimes() => Times;
        public void CadastrarTimes(Usuario usuario, List<Time> times)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não tem permissão para executar essa operação!!");
            }
            else if (inicioCampeonato)
            {
                throw new PermissaoNegadaException("Você não pode fazer essa operação. O campeonato já começou!");
            }
            else if (times.Count % 2 != 0)
            {
                throw new LimiteNaoPermitidoException("Deverá inscrever quantidades de times pares!!");
            }
            else if (times.Count < 7)
            {
                throw new LimiteNaoPermitidoException("Deverá inscrever mais de 8 times para o campeonato!!");
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

        public bool AdicionarJogadorTime(Usuario usuario, Guid idTime, Jogador jogador)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!");
            }
            return times.FirstOrDefault(time => time.Id == idTime).AdicionarJogador(jogador);
        }

        public List<string> ApresentarPartidas()
        {
            var mostradorDePartidas = new List<string>();
            GerarProximoConfronto();

            mostradorDePartidas.Add("------------Proxima Partida------------");
            for (int i = 0; i < partidas.Count; i++)
            {
                mostradorDePartidas.Add($"Partida{i + 1} -> {partidas[i].TimeAnfitriao.NomeTime} X {partidas[i].TimeVisitante.NomeTime}");
            }
            return mostradorDePartidas;
        }

        public bool InscreverResultadoDaPartida(Usuario usuario, int partida, int golsAnfitriao, int golsVisitante)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!");
            }

            partidas[partida - 1].MarcarGolAnfitriao(golsAnfitriao);

            for (int j = 0; j < golsVisitante; j++)
            {
                partidas[partida - 1].TimeAnfitriao.Tabela.MarcarGolsContra();
            }

            partidas[partida - 1].MarcarGolVisitante(golsVisitante);

            for (int j = 0; j < golsAnfitriao; j++)
            {
                partidas[partida - 1].TimeVisitante.Tabela.MarcarGolsContra();
            }

            return true;
        }

        public bool RegistrarJogadoresGoleadoresDaPartida(Usuario usuario, string timeVencedor, string nomeJogador, int golFeitos)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não é permitido para a acessar essa função");
            }

            var time = times.FirstOrDefault(x => x.NomeTime == timeVencedor);
            var jogador = time.Jogadores.FirstOrDefault(x => x.Nome == nomeJogador);

            for (int i = 0; i < golFeitos; i++)
            {
                time.Tabela.MarcarGolsPro();
                jogador.MarcarGol();
            }
            return true;
        }
        public bool RegistrarRodada(Usuario usuario)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não é permitido para a acessar essa função");
            }

            GerarRodadas();
            partidas.Clear();
            return true;
        }
        public List<string> ExibirResultadoDaRodada(Usuario usuario, int rodadaDesejada)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            var listaResultados = new List<string>();

            for (int j = 0; j < rodadas[rodadaDesejada].Partidas.Count; j++)
            {
                var resultadosAMostrar = $"Rodada {rodadaDesejada} Resultado: {rodadas[rodadaDesejada].Partidas.ElementAt(j).TimeAnfitriao.NomeTime} {rodadas[rodadaDesejada].Partidas.ElementAt(j).GolsAnfitriao} X {rodadas[rodadaDesejada].Partidas.ElementAt(j).GolsVisitante} {rodadas[rodadaDesejada].Partidas.ElementAt(j).TimeVisitante.NomeTime}";
                listaResultados.Add(resultadosAMostrar);
            }

            return listaResultados;
        }

        public List<string> ApresentarTabela(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            var listaTabelaDeClassificacao = new List<string>();
            var timesOrdenador = times.OrderByDescending(time => time.Tabela.Pontuacao);
            listaTabelaDeClassificacao.Add($"---------------Tabela Brasileirão 2020 ----------------------");
            listaTabelaDeClassificacao.Add($"Time  |  P  |  PD  |  V  |  E  |  D   | SG  |  GP  |  GC  |  PA");

            foreach (var time in timesOrdenador)
            {
                listaTabelaDeClassificacao.Add($"{time.NomeTime} | " + time.Tabela.ToString());
            }

            return listaTabelaDeClassificacao;
        }

        public List<string> ExibirTimesClassificadosLibertadores(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }
            return ApresentarTabela(usuario).Take(4).ToList();
        }


        public List<string> ExibirTimesRebaixados(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            return ApresentarTabela(usuario).TakeLast(4).ToList();
        }

        public List<string> ExibirClassificacaoDeArtilheiros(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }
            var jogadores = new List<Jogador>();

            for (int i = 0; i < times.Count; i++)
            {
                jogadores.AddRange(times[i].Jogadores.Select(x => x));
            }

            var jogadoresArtilheiros = jogadores.OrderByDescending(x => x.Gol).ToList();

            var jogadorArtilheiroFormatados = jogadoresArtilheiros.Select((x, i) => $"{i + 1} {x.Nome} - {x.Gol} Gols").Take(5).ToList();

            return jogadorArtilheiroFormatados;
        }


        //* --------------------------Operações Internas a classe------------------------------->

        // *<-------------------------------Sistema Determinístico---------------------------------->

        /*
            Deixei o Gerador de rodada com sorteios de confronto de times aleatoriamente
            irá gerar as rodadas igual ao numero da rodada recebida como paramentro no
            método ExibirResultadoDaRodada.
            Nos testes, chamei apenas uma vez para testar se estava retornando o que era proposto.

        */

        private void GerarRodadas()
        {
            nRodada++;
            for (int i = 0; i < partidas.Count; i++)
            {
                var timesEmPartida = partidas.ElementAt(i);
                SetarEstatisticas(timesEmPartida.TimeAnfitriao, timesEmPartida.TimeVisitante, timesEmPartida.GolsAnfitriao, timesEmPartida.GolsVisitante);
            }
            rodada.AdicionarPartida(partidas);
            rodadas.Add(rodada);
        }
        /*
            O método SetarEstatisticas irá chamar as operações de estatisticas do time anfitrião e 
            visitante. Cada time tem sua própria tabela de estatisticas onde será utilizada para ranquear
            os times do campeonáto para assim fazer a classificação entre libertadores e rebaixados
        */
        private void SetarEstatisticas(Time anfitriao, Time visitante, int golsAnfitriao, int golsVisitante)
        {
            GeradorJogadoresGoleadoresMockados(visitante, golsVisitante);
            GeradorJogadoresGoleadoresMockados(anfitriao, golsAnfitriao);

            if (golsAnfitriao > golsVisitante)
            {
                anfitriao.Tabela.MarcarVitoria();
                anfitriao.Tabela.MarcarPontuacao();
                anfitriao.Tabela.DisputarPartida();
                anfitriao.Tabela.AtualizarPerctAproveitamento();
                anfitriao.Tabela.MarcarSaldoDeGols();
                visitante.Tabela.MarcarDerrota();
                visitante.Tabela.DisputarPartida();
                visitante.Tabela.AtualizarPerctAproveitamento();
                visitante.Tabela.MarcarSaldoDeGols();
            }
            else if (golsAnfitriao < golsVisitante)
            {
                anfitriao.Tabela.MarcarDerrota();
                anfitriao.Tabela.DisputarPartida();
                anfitriao.Tabela.AtualizarPerctAproveitamento();
                anfitriao.Tabela.MarcarSaldoDeGols();
                visitante.Tabela.MarcarVitoria();
                visitante.Tabela.MarcarPontuacao();
                visitante.Tabela.DisputarPartida();
                visitante.Tabela.MarcarSaldoDeGols();
                visitante.Tabela.AtualizarPerctAproveitamento();
            }
            else
            {
                for (int j = 0; j < golsAnfitriao; j++)
                {
                    anfitriao.Tabela.MarcarGolsPro();
                    anfitriao.Tabela.MarcarGolsContra();
                    visitante.Tabela.MarcarGolsPro();
                    visitante.Tabela.MarcarGolsContra();
                }

                anfitriao.Tabela.MarcarEmpate();
                anfitriao.Tabela.MarcarPontuacao();
                anfitriao.Tabela.DisputarPartida();
                anfitriao.Tabela.AtualizarPerctAproveitamento();
                anfitriao.Tabela.MarcarSaldoDeGols();
                visitante.Tabela.MarcarEmpate();
                visitante.Tabela.MarcarPontuacao();
                visitante.Tabela.DisputarPartida();
                visitante.Tabela.AtualizarPerctAproveitamento();
                visitante.Tabela.MarcarSaldoDeGols();
            }
        }

        /*
            Aqui, este método abaixo é onde a mágica dos confontros aleatórios acontece.
            Organizo de forma aleatória a lista de times e adiciono na minha propriedade interna partidas.
            Ao chamar ExibirResultadosDaRodada, essa lista é utilizada para fazer a exibição dos confrontos.
        */

        private void GerarProximoConfronto()
        {
            if (nRodada == 0)
            {
                for (int i = 0; i < times.Count; i += 2)
                {
                    partida = new PartidaCampeonatoBrasileirao();
                    partida.AdicionarTimeAnfitriaoAPartida(times[i]);
                    partida.AdicionarTimeVisitanteAPartida(times[i + 1]);
                    partidas.Add(partida);
                    partida = null;
                }
            }

            if (nRodada == 1)
            {
                for (int i = times.Count - 1; i > 0; i -= 2)
                {
                    partida = new PartidaCampeonatoBrasileirao();
                    partida.AdicionarTimeAnfitriaoAPartida(times[i]);
                    partida.AdicionarTimeVisitanteAPartida(times[i - 1]);
                    partidas.Add(partida);
                    partida = null;
                }
            }

            if (nRodada == 2)
            {
                for (int i = 0; i < times.Count; i += 2)
                {
                    partida = new PartidaCampeonatoBrasileirao();
                    partida.AdicionarTimeAnfitriaoAPartida(times[i]);
                    partida.AdicionarTimeVisitanteAPartida(times[i + 2]);
                    partidas.Add(partida);
                    partida = null;
                }
            }

        }


        /*
            No método abaixo gero jogadores que fazem gols para seu time.
        */
        private void GeradorJogadoresGoleadoresMockados(Time timeVencedor, int golFeitos)
        {
            var gerador = new Random();
            var idTime = timeVencedor.Id;

            for (int i = 0; i < golFeitos; i++)
            {
                var indexJogador = gerador.Next(0, timeVencedor.Jogadores.Count);
                var jogadorGoleador = times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador);
                times.Find(i => i.Id == idTime).Tabela.MarcarGolsPro();
                times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador).MarcarGol();
            }

        }

    }
}