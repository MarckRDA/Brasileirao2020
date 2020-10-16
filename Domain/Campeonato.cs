using System;
using System.Collections.Generic;

namespace Domain
{
    public class Campeonato
    {
        private List<Time> times { get; set; }
        public IReadOnlyCollection<Time> Times => times;
        private bool inicioCampeonato = false;
        public int Rodada { get; private set; }
        //private (int anfitriao, int visitante) confronto;
        private List<(Time anfitriao, Time visitante)> GerarConfrontos()
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

            for (int i = 0; i < times.Count; i+=2)
            {
                partida.Add((times[quemJogaComQuem[i]], times[quemJogaComQuem[i+1]]));
            } 

            return partida;
        }

        public void ApresentarTabela(Usuario usuario)
        {
            System.Console.WriteLine("---------- Tabela Do Brasileirão ---------");
            System.Console.WriteLine("------------------------------------------");
            System.Console.WriteLine("Time | PT | PD | V | E | D | GP | GC | PA");
            System.Console.WriteLine("------------------------------------------");

            foreach (var time in times)
            {
                System.Console.WriteLine(time.ToString());
            }

            System.Console.WriteLine("------------------------------------------");
        }

        public void CadastrarTimes(Usuario usuario, List<Time> times)
        {
            if(!(usuario is CBF))
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
        
    }
}