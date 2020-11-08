using System;
using System.Collections.Generic;
using System.Linq;
using Domain.src.ClassesAuxiliadoras;
using Domain.src.ExceptionsHandlers;
using Domain.src.Jogadores;
using Domain.src.Times;
using Domain.src.Users;

namespace Domain.src.Campeonato
{
    public sealed class CampeonatoBrasileirao : ICampeonato // Criei uma interface com o intuíto de "extender" para um outro campeonato que não seja o brasileiro
    {
        private List<Time> times { get; set; } = new List<Time>();
        private List<Partida> partidas = new List<Partida>();
        private List<Rodada> rodadas = new List<Rodada>();
        private List<Partida> todasAsPartidas = new List<Partida>();
        private bool inicioCampeonato = false;
        private int nRodada = 0;
        private Partida partida;
        private Rodada rodada = new RodadaCampeonatoBrasileirao();


        // *<------------------- Metodos de operações externas a classe --------------------------->

        //Método acessor ao campo times criado para ser utilizado como operador na classe de teste
        public IReadOnlyCollection<Time> ObterListaTimes() => times;

        // Outro método acessor para fazer a mesma porcaria descrito acima.
        public IReadOnlyCollection<Partida> ObterTodasAsPartidas() => todasAsPartidas;
        /*
            Aqui efetivamente começa a implementação da lógica das operações definidas no modelo de negócio
            espero que esteja certo... Enfim, Cadastrar times só permitirá a inserção de times no campeonato
            se uma serie de condições forem atendidas.
            * Somente o usuário CBF pode cadastrar times
            * Se o campeonato estiver sido iniciado, não poderá cadastrar nenhum time
            * a lista de times for maior que 8... sim, 8, só vejo lógica com 8.

            Se passar pelas condições, recederá a lista de times, iniciará o campeonato e gerará de antemão 
            as partidas, explicarei mais abaixo o método GerarTodasAsPartidas
        */
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
            else if (times.Count < 8)
            {
                throw new LimiteNaoPermitidoException("Deverá inscrever mais de 8 times para o campeonato!!");
            }


            this.times = times;
            inicioCampeonato = true;
            GerarTodasAsPartidas();
        }

        /*
            Método feito para remover os jogadores de um time de interesse.Só pode ser acessada como CBF
            Se não for atendida, lançará uma exceção.
        */

        public bool RemoverJogadorTime(Usuario usuario, Guid idTime, Jogador jogador)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!");
            }
            return times.FirstOrDefault(time => time.Id == idTime).RemoverJogador(jogador);
        }

        /*
            Mesma lógica de defesa do método acima. Aqui será para adicionar um jogador ao time de interesse.
            A lógica é impleentada na classe Time.
        */

        public bool AdicionarJogadorTime(Usuario usuario, Guid idTime, Jogador jogador)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Essa função só pode ser acessada como CBF!");
            }
            return times.FirstOrDefault(time => time.Id == idTime).AdicionarJogador(jogador);
        }

        /*
            Método que apresenta as partida para aquela rodada em especial que foi chamada. 
            A quantidade de vezes que for chamada esse método irá acrescer +1 rodada.
            Quem é o cabeça da função é o método privado GerarProximoConfronto, onde ele fará a logica de
            separar todas as partidas possíveis já geradas em 7 rodadas.
        */
        public List<string> ApresentarPartidas(Usuario usuario)
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

        /*
            Método criado para saber se algum time de interesse irá realmente jogar com todos os outros times do campeonato.
            Nessa modalidade de campeonato não tem returno, ou seja, se o time for uma merda fora de casa, se F*&@u.
            Todas as operações que esta sendo realizada, está alterando o estado (campos) dessa classe.
        */
        public List<string> MostrarPartidasQueOTimeEnfrenta(string nomeDoTime)
        {
            var mostradorDePartidas = new List<string>();
            if (!times.Exists(t => t.NomeTime == nomeDoTime)) return mostradorDePartidas = null;

            var idTime = times.FirstOrDefault(x => x.NomeTime == nomeDoTime).Id;

            var anfitriao = todasAsPartidas.Where(x => x.TimeAnfitriao.Id == idTime).Select(x => $"{x.TimeAnfitriao.NomeTime} X {x.TimeVisitante.NomeTime}").ToList();
            var visitante = todasAsPartidas.Where(x => x.TimeVisitante.Id == idTime).Select(x => $"{x.TimeAnfitriao.NomeTime} X {x.TimeVisitante.NomeTime}").ToList();

            for (int i = 0; i < anfitriao.Count; i++)
            {
                mostradorDePartidas.Add(anfitriao[i]);
            }

            for (int i = 0; i < visitante.Count; i++)
            {
                mostradorDePartidas.Add(visitante[i]);
            }

            return mostradorDePartidas;
        }

        /*
            Aqui estará simulando um quarto arbitro, onnde ele irá escrever os resultados das partidas da rodada
            será chamado no teste para o simples mockar de gols. a lógica esta sendo implementada na classe partida.
        */

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

        /*
            Aqui é a mesma palhaçada do método acima, porem é com os jogadores que fizeram gols nas partidas da ultima rodada.
            A lógica é implementada na classe TabelaDeEstatistica.
        */
        public bool RegistrarJogadoresGoleadoresDaPartida(Usuario usuario, string timeVencedor, string nomeJogador, int golFeitos)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não é permitido para a acessar essa função");
            }

            if (!times.Exists(t => t.NomeTime == timeVencedor)) return false;

            var time = times.FirstOrDefault(x => x.NomeTime == timeVencedor);

            var jogador = time.Jogadores.FirstOrDefault(x => x.Nome == nomeJogador);

            for (int i = 0; i < golFeitos; i++)
            {
                time.Tabela.MarcarGolsPro();
                jogador.MarcarGol();
            }
            return true;
        }

        /*
            Irá efetivamente registar os resultado das partidas e finalizará a rodada.
            GerarRodadas é o cabeça na área
        */
        public bool RegistrarRodada(Usuario usuario)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você não é permitido para a acessar essa função");
            }

            GerarRodadas();
            return true;
        }

        /*
            Aqui irá apenas mostrar para o usuario, os resultados da rodada.. um Pretty Print dos resultados
        */
        public List<string> ExibirResultadoDaRodada(Usuario usuario, int rodadaDesejada)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            var listaResultados = new List<string>();

            if (rodadaDesejada <= nRodada)
            {
                for (int j = 0; j < rodadas[rodadaDesejada - 1].Partidas.Count; j++)
                {
                    var resultadosAMostrar = $"Rodada {rodadaDesejada} Resultado: {rodadas[rodadaDesejada - 1].Partidas.ElementAt(j).TimeAnfitriao.NomeTime} {rodadas[rodadaDesejada - 1].Partidas.ElementAt(j).GolsAnfitriao} X {rodadas[rodadaDesejada - 1].Partidas.ElementAt(j).GolsVisitante} {rodadas[rodadaDesejada - 1].Partidas.ElementAt(j).TimeVisitante.NomeTime}";
                    listaResultados.Add(resultadosAMostrar);
                }
            }
            return listaResultados;
        }

        /*
            Aqui tbm é a mesma bobeira do método acima. mostrará um Pretty Print da tabela de classeificação.
        */
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
            var posições = 1;

            foreach (var time in timesOrdenador)
            {
                listaTabelaDeClassificacao.Add($"{posições} - {time.NomeTime} | " + time.Tabela.ToString());
                posições++;
            }

            return listaTabelaDeClassificacao;
        }

        /*
            O nome fala por si só
        */
        public List<string> ExibirTimesClassificadosLibertadores(Usuario usuario)
        {
            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }
            var listaClassificados = new List<string>();

            for (int i = 2; i < ApresentarTabela(usuario).Count - 4; i++)
            {
                listaClassificados.Add(ApresentarTabela(usuario)[i]);
            }

            return listaClassificados;
        }

        /*
            Idem, idem.. é o se.. 
        */

        public List<string> ExibirTimesRebaixados(Usuario usuario)
        {

            if (!(usuario is CBF || usuario is Torcedor))
            {
                return null;
            }

            return ApresentarTabela(usuario).TakeLast(4).ToList();
        }

        /*
            Aqui a lógica é pegar todos os jogadores de todos os times e jogá-los numa lista
            onde irei ordená-los pela quantidade de gols feitos e pegar somente os 5 primeiros.
        */

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
            O gerador de rodadas somente pegará as partidas da atual rodada e setará as estatisticas da partidas
            tipo, marcar vitória e so on..
        */

        private void GerarRodadas()
        {

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
            Deixei esse método de geração de todas as partiddas o mais Determinístico o possível, ou seja,
            sempre na primeira rodada será os mesmos times se emfrentanto. É melhor para trabalhar com testes
            Claro, do jeito o qual estou estanto meus métodos.        
        */
        //* Aqui é onde gera todas as partidas que o campeonato terá.
        //* Nesse caso, serão 28 partidas (7 * 4)
        private void GerarTodasAsPartidas()
        {
            var p = new List<Time>(times);
            p = times.Select(x => x).ToList();

            while (p.Count > 0)
            {
                for (int j = 0; j < p.Count; j++)
                {

                    if (j == 0)
                    {
                        continue;
                    }

                    partida = new PartidaCampeonatoBrasileirao();
                    partida.AdicionarTimeAnfitriaoAPartida(p[0]);
                    partida.AdicionarTimeVisitanteAPartida(p[j]);
                    todasAsPartidas.Add(partida);
                    partida = null;
                }

                p.Remove(p[0]);
            }
        }

        // * deixei esse método gerando apenas três rodadas
        // * porém todos os times jogam uns contras os outros sem repetição. 
        private void GerarProximoConfronto()
        {
            partidas.Clear();
            partidas.Add(todasAsPartidas[0]);
            partidas.Add(todasAsPartidas[todasAsPartidas.Count - 15]);
            partidas.Add(todasAsPartidas[todasAsPartidas.Count - 6]);
            partidas.Add(todasAsPartidas[todasAsPartidas.Count - ++nRodada]);
            todasAsPartidas.Remove(todasAsPartidas[0]);
            todasAsPartidas.Remove(todasAsPartidas[todasAsPartidas.Count - 15]);
            todasAsPartidas.Remove(todasAsPartidas[todasAsPartidas.Count - 6]);
            todasAsPartidas.Remove(todasAsPartidas[todasAsPartidas.Count - ++nRodada]);

        }
    }
}