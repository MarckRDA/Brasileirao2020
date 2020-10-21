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
        private List<Partida> partidas = new List<Partida>(); // !!Ponto de atenção; Para plano de contingência apagar essa linha
        private List<Rodada> rodadas = new List<Rodada>(); // ! Ponto de atenção; Para plano de contingência apagar essa linha
        private Partida partida; // ! Criado só para criar o tipo de partida fazendo injeção de dependencia; Para plano de contingência apagar essa linha
        private Rodada rodada = new RodadaCampeonatoBrasileirao(); // ! ! Criado só para criar o tipo de Rodada fazendo injeção de dependencia; Para plano de contingência apagar essa linha
    


        // ! Se der merda, para plano de contingência apagar essa linha
        

        // *<------------------- Metodos de operações externas a classe --------------------------->
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

        public IReadOnlyCollection<Time> ObterListaTimes() => Times;

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

        public List<string> ApresentarTabela(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            var listaTabelaDeClassificacao = new List<string>();
            var timesOrdenador = times.OrderByDescending(time => time.Tabela.Pontuacao);

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

        public List<string> ExibirResultadoDaRodada(Usuario usuario, int qtdRodadas = 1)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            //GerarRodada(qtdRodadas); // !Descomentar para rodar normalmente o programa;
            var listaResultados = new List<string>();
            for (int i = 0; i < rodadas.Count; i++)
            {
                for (int j = 0; j < rodadas[i].Partidas.Count; j++)
                {
                    var resultadosAMostrar = $"Rodada {i + 1} Resultado: {rodadas[i].Partidas.ElementAt(j).TimeAnfitriao.NomeTime} {rodadas[i].Partidas.ElementAt(j).GolsAnfitriao} X {rodadas[i].Partidas.ElementAt(j).GolsVisitante} {rodadas[i].Partidas.ElementAt(j).TimeVisitante.NomeTime}";
                    listaResultados.Add(resultadosAMostrar);
                }
            }

            return listaResultados;
        }

       
        // *<------------------- Sistema Determinístico --------------------------->
        // ! Utilizando dados mocados para ter previsões nos testes
        public void GerarRodadaMockada(int golsAnfitriao, int golsVisitante)
        {
            nRodada++;

            GerarProximoConfrontoMocado(golsAnfitriao, golsVisitante);

            for (int i = 0; i < partidas.Count; i++)
            {
                var timeAnfitriaoGols = golsAnfitriao;
                var timeVisitanteGols = golsVisitante;
                var timesEmPartida = partidas.ElementAt(i);

                if (timeAnfitriaoGols > timeVisitanteGols)
                {
                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeVisitante, timeVisitanteGols);
                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
                    timesEmPartida.TimeAnfitriao.Tabela.MarcarVitoria();
                    timesEmPartida.TimeAnfitriao.Tabela.MarcarPontuacao();
                    timesEmPartida.TimeVisitante.Tabela.MarcarDerrota();
                    timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
                    timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
                    timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
                    timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();
                }
                else if (timeAnfitriaoGols < timeVisitanteGols)
                {
                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeVisitante, timeVisitanteGols);
                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
                    timesEmPartida.TimeAnfitriao.Tabela.MarcarDerrota();
                    timesEmPartida.TimeVisitante.Tabela.MarcarVitoria();
                    timesEmPartida.TimeVisitante.Tabela.MarcarPontuacao();
                    timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
                    timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
                    timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
                    timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();
                }
                else
                {
                    for (int j = 0; j < timeAnfitriaoGols; j++)
                    {
                        timesEmPartida.TimeAnfitriao.Tabela.MarcarGolsPro();
                        timesEmPartida.TimeAnfitriao.Tabela.MarcarGolsContra();
                        timesEmPartida.TimeVisitante.Tabela.MarcarGolsPro();
                        timesEmPartida.TimeVisitante.Tabela.MarcarGolsContra();
                    }

                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeVisitante, timeVisitanteGols);
                    GeradorJogadoresGoleadoresMockados(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
                    timesEmPartida.TimeAnfitriao.Tabela.MarcarEmpate();
                    timesEmPartida.TimeAnfitriao.Tabela.MarcarPontuacao();
                    timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
                    timesEmPartida.TimeVisitante.Tabela.MarcarEmpate();
                    timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
                    timesEmPartida.TimeVisitante.Tabela.MarcarPontuacao();
                    timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
                    timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();

                }

            }
            rodada.AdicionarPartida(partidas);
            rodadas.Add(rodada);

        }

        private void GerarProximoConfrontoMocado(int golsAnfitriao, int golsVisitante)
        {
            for (int i = 0; i < times.Count; i += 2)
            {
                partida = new PartidaCampeonatoBrasileirao();
                partida.AdicionarTimeAnfitriaoAPartida(times[i]);
                partida.MarcarGolAnfitriao(golsAnfitriao);

                for (int j = 0; j < golsVisitante; j++)
                {
                    partida.TimeAnfitriao.Tabela.MarcarGolsContra();
                }
                partida.AdicionarTimeVisitanteAPartida(times[i + 1]);
                partida.MarcarGolVisitante(golsVisitante);

                for (int j = 0; j < golsAnfitriao; j++)
                {
                    partida.TimeVisitante.Tabela.MarcarGolsContra();
                }
                partidas.Add(partida);
                partida = null;
            }

        }


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


        //*Descomentar para Ter um sistema mais randômico
        // private void GerarRodada(int qtdRodadas)
        // {
        //     while (qtdRodadas <= nRodada)
        //     {
        //         nRodada++;

        //         GerarProximoConfronto();

        //         for (int i = 0; i < partidas.Count; i++)
        //         {
        //             var timeAnfitriaoGols = GeradorGols();
        //             var timeVisitanteGols = 6 - timeAnfitriaoGols;
        //             var timesEmPartida = partidas.ElementAt(i);


        //             if (timeAnfitriaoGols > timeVisitanteGols)
        //             {
        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeVisitante, timeVisitanteGols);
        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
        //                 timesEmPartida.TimeAnfitriao.Tabela.MarcarVitoria();
        //                 timesEmPartida.TimeAnfitriao.Tabela.MarcarPontuacao();
        //                 timesEmPartida.TimeVisitante.Tabela.MarcarDerrota();
        //                 timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
        //                 timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();
        //             }
        //             else if (timeAnfitriaoGols < timeVisitanteGols)
        //             {
        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeVisitante, timeVisitanteGols);
        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
        //                 timesEmPartida.TimeAnfitriao.Tabela.MarcarDerrota();
        //                 timesEmPartida.TimeVisitante.Tabela.MarcarVitoria();
        //                 timesEmPartida.TimeVisitante.Tabela.MarcarPontuacao();
        //                 timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
        //                 timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();
        //             }
        //             else
        //             {
        //                 for (int j = 0; j < timeAnfitriaoGols; j++)
        //                 {
        //                     timesEmPartida.TimeAnfitriao.Tabela.MarcarGolsPro();
        //                     timesEmPartida.TimeAnfitriao.Tabela.MarcarGolsContra();
        //                     timesEmPartida.TimeVisitante.Tabela.MarcarGolsPro();
        //                     timesEmPartida.TimeVisitante.Tabela.MarcarGolsContra();
        //                 }

        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeVisitante, timeVisitanteGols);
        //                 GeradorJogadoresGoleadores(timesEmPartida.TimeAnfitriao, timeAnfitriaoGols);
        //                 timesEmPartida.TimeAnfitriao.Tabela.MarcarEmpate();
        //                 timesEmPartida.TimeAnfitriao.Tabela.MarcarPontuacao();
        //                 timesEmPartida.TimeAnfitriao.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeVisitante.Tabela.MarcarEmpate();
        //                 timesEmPartida.TimeAnfitriao.Tabela.AtualizarPerctAproveitamento();
        //                 timesEmPartida.TimeVisitante.Tabela.MarcarPontuacao();
        //                 timesEmPartida.TimeVisitante.Tabela.DisputarPartida();
        //                 timesEmPartida.TimeVisitante.Tabela.AtualizarPerctAproveitamento();

        //             }

        //         }

        //         rodada.AdicionarPartida(partidas);
        //         rodadas.Add(rodada);
        //     }

        // }

        // // ! Se der merda no teste, apagar esse método para plano de contingência
        // private void GerarProximoConfronto()
        // {
        //     Random sorteador = new Random();
        //     var quemJogaComQuem = new List<int>();

        //     while (quemJogaComQuem.Count <= times.Count)
        //     {
        //         int getNumber = sorteador.Next(0, times.Count - 1);

        //         if (!quemJogaComQuem.Contains(getNumber))
        //         {
        //             quemJogaComQuem.Add(getNumber);
        //         }
        //     }

        //     for (int i = 0; i < times.Count; i += 2)
        //     {
        //         partida = new PartidaCampeonatoBrasileirao();
        //         partida.AdicionarTimeAnfitriaoAPartida(times[quemJogaComQuem[i]]);
        //         partida.AdicionarTimeVisitanteAPartida(times[quemJogaComQuem[i + 1]]);
        //         partidas.Add(partida);
        //     }

        // }

        // private int GeradorGols()
        // {
        //     var geradorGols = new Random();

        //     return geradorGols.Next(0, 6);
        // }

        // private void GeradorJogadoresGoleadores(Time timeVencedor, int golFeitos)
        // {
        //     var gerador = new Random();
        //     var idTime = timeVencedor.Id;

        //     for (int i = 0; i < golFeitos; i++)
        //     {
        //         var indexJogador = gerador.Next(0, timeVencedor.Jogadores.Count);
        //         var jogadorGoleador = times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador);
        //         times.Find(i => i.Id == idTime).Tabela.MarcarGolsPro();
        //         times.Find(i => i.Id == idTime).Jogadores.ElementAt(indexJogador).MarcarGol();
        //     }

        // }


    }
}