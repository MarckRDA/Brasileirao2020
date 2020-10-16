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
        private Random sorteador = new Random();
        private List<(Time anfitrião, Time visitante)> GerarConfrontos()
        {
            
            while (true)
            {
                sorteador.Next(1, times.Count);
            }
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