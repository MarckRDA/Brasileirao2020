using System;
using System.Collections.Generic;
using System.Linq;
using Domain.src;

namespace WebAPI.Repositorio
{
    public class RepositorioJogadores
    {
        private static List<Jogador> Jogadores { get; set; } = new List<Jogador>()
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

        public static List<Jogador> ObterJogadores()
        {
            return RepositorioJogadores.Jogadores;
        }

        public static Jogador ObterJogador(Guid id)
        {
            return Jogadores.FirstOrDefault(jogador => jogador.Id == id);
        }

        public static void GravarJogador(Jogador jogador)
        {
            Jogadores.Add(jogador);
        }
        
        public static void RemoverJogador(Guid id)
        {
            if (!Jogadores.Exists(x => x.Id == id))
            {
                return;
            }
            Jogadores.Remove(ObterJogador(id));
        }
    }
}