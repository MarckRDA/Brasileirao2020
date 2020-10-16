using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Campeonato
    {
        private List<TimeCampeonato> times { get; set; }
        public IReadOnlyCollection<TimeCampeonato> Times => times;
        private bool inicioCampeonato = false;
        public int Rodada { get; private set; }
        //private (int anfitriao, int visitante) confronto;

        public void CadastrarTimes(Usuario usuario, List<TimeCampeonato> times)
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

        private List<(TimeCampeonato anfitriao, TimeCampeonato visitante)> GerarProximoConfronto()
        {
            Random sorteador = new Random();
            var quemJogaComQuem = new List<int>();

            var partida = new List<(TimeCampeonato anfitriao, TimeCampeonato visitante)>();

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

            System.Console.WriteLine($"----------Rodada{Rodada} do brasileirão");
            foreach (var time in partida)
            {
                System.Console.WriteLine($"{time.anfitriao.NomeTime} X {time.visitante.NomeTime}");
            }

            return partida;
        }

        private int GeradorGols()
        {
            var geradorGols = new Random();

            return geradorGols.Next(0, 6);
        }

        // private int GeradorProbabilidadeTimeVencedor()
        // {
        //     var geradorGols = new Random();

        //     return geradorGols.Next(0, 100);
        // }

        private void GeradorJogadoresGoleadores(TimeCampeonato timeVencedor, TimeCampeonato timePerdedor, int golFeitos)
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

        public void ExibirResultado(Usuario usuario)
        {
            if (!(usuario is CBF))
            {
                throw new PermissaoNegadaException("Você tem de ser o administrador para acessar a função");
            }

            Rodada++;
            
            var partidas = GerarProximoConfronto();
            // var golsGanhador = 0;
            // var golsPerdedor = 0;
            // // Pensar em casa =>
            for (int i = 0; i < partidas.Count; i++)
            {
                var timeAnfitriaoGols = GeradorGols();
                var timeVisitanteGols = 6 - timeAnfitriaoGols;              
                // var timeAnfitricaoVence = GeradorProbabilidadeTimeVencedor();
                // var timeVisitanteVence = 100 - timeAnfitricaoVence;  
                var timesEmPartida = partidas.ElementAt(i);

                if (timeAnfitriaoGols > timeVisitanteGols)
                {
                    GeradorJogadoresGoleadores(timesEmPartida.anfitriao, timesEmPartida.visitante, timeAnfitriaoGols);
                    timesEmPartida.anfitriao.MarcarVitoria();
                    timesEmPartida.visitante.MarcarDerrota();
                }
                else if (timeAnfitriaoGols < timeVisitanteGols)
                {
                    GeradorJogadoresGoleadores(timesEmPartida.visitante, timesEmPartida.anfitriao, timeVisitanteGols);
                    timesEmPartida.anfitriao.MarcarDerrota();
                    timesEmPartida.visitante.MarcarVitoria();
                }
                else
                {
                    for (int j = 0; j < timeAnfitriaoGols; j++)
                    {
                        timesEmPartida.anfitriao.MarcarGolsPro();
                        timesEmPartida.visitante.MarcarGolsPro();
                    }

                    timesEmPartida.anfitriao.MarcarEmpate();
                    timesEmPartida.visitante.MarcarEmpate();
                }

                
            }
                        
        }

        public void ApresentarTabela(Usuario usuario)
        {
            System.Console.WriteLine("---------- Tabela Do Brasileirão ---------");
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Time | PT | PD | V | E | D | GP | GC | %");
            System.Console.WriteLine("------------------------------------------");

            foreach (var time in times)
            {
                System.Console.WriteLine(time.ToString());
            }

            System.Console.WriteLine("------------------------------------------");
        }
    }
}