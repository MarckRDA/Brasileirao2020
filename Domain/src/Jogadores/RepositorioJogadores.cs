using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.src.Jogadores
{
    public class RepositorioJogadores
    {
        private static List<Jogador> jogadores = new List<Jogador>()
        {
            new JogadorTime("Santos"),
            new JogadorTime("Leo Gomes"),
            new JogadorTime("Fabinho"),
            new JogadorTime("Pedro Henrique"),
            new JogadorTime("Thiago Heleno"),
            new JogadorTime("Abner vinicius"),
            new JogadorTime("Wellington"),
            new JogadorTime("Richard"),
            new JogadorTime("Christian"),
            new JogadorTime("Leo Cittadin"),
            new JogadorTime("Ravanelli"),
            new JogadorTime("Bissoli"),
            new JogadorTime("Carlos Eduardo"),
            new JogadorTime("Nikão"),
            new JogadorTime("Renato Kayser"),
            new JogadorTime("Geuvanio"),
            new JogadorTime("Jandrei"),
            new JogadorTime("Kellven"),
            new JogadorTime("Aguilar"),
            new JogadorTime("João Victor"),
            new JogadorTime("Lucho Gonzales"),
            new JogadorTime("Alvarado"),
            new JogadorTime("Walter"),
        };

        public IReadOnlyCollection<Jogador> Jogadores => jogadores;

        public static List<Jogador> ObterJogadores()
        {
            return jogadores;
        }

        public static Jogador ObterJogador(Guid id)
        {
            return jogadores.FirstOrDefault(jogador => jogador.Id == id);
        }

        public static void GravarJogador(Jogador jogador)
        {
            jogadores.Add(jogador);
        }
        
        public static void RemoverJogador(Guid id)
        {
            if (!jogadores.Exists(x => x.Id == id))
            {
                return;
            }
            jogadores.Remove(ObterJogador(id));
        }

        public static Jogador AtualizarJogador (Guid id, string novoNome)
        {
            var jogador = ObterJogador(id);
            var jogadorAAdicionar = new JogadorTime(novoNome);
            
            if (!jogadorAAdicionar.Validar().isValid)
            {
                return null;    
            }
            jogadores.Remove(jogador);
            jogadores.Add(jogadorAAdicionar);

            return jogadorAAdicionar; 
        }
    }
}