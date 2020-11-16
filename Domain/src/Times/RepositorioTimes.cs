using System;
using System.Collections.Generic;
using System.Linq;
using Domain.src.Jogadores;


namespace Domain.src.Times
{
    public static class RepositorioTimes
    {
        private static List<Time> times = new List<Time>()
        {
            new TimeCampeonatoBrasileirao("Atheltico"),
            new TimeCampeonatoBrasileirao("Athletico Goianiense"),
            new TimeCampeonatoBrasileirao("Athletico Mineiro"),
            new TimeCampeonatoBrasileirao("Bahia"),
            new TimeCampeonatoBrasileirao("Botafogo"),
            new TimeCampeonatoBrasileirao("Bragantino"),
            new TimeCampeonatoBrasileirao("Ceara"),
            new TimeCampeonatoBrasileirao("Flamengo")
        };

        public static IReadOnlyCollection<Time> Times => times;


        private static List<Jogador> JogadoresAthletico = new List<Jogador>()
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
        private static List<Jogador> JogadoresAtleticoGoianiense = new List<Jogador>()
        {
            new JogadorTime("Jean"),
            new JogadorTime("Dudu"),
            new JogadorTime("João Victor"),
            new JogadorTime("Éder"),
            new JogadorTime("Natanael"),
            new JogadorTime("Willian Maranhão"),
            new JogadorTime("Marlon Freitas"),
            new JogadorTime("Arnaldo"),
            new JogadorTime("Zé Roberto"),
            new JogadorTime("Hyuri"),
            new JogadorTime("Matheus Vargas"),
            new JogadorTime("Matheuzinho"),
            new JogadorTime("Mauricio Kozlinski"),
            new JogadorTime("Luan Sales"),
            new JogadorTime("Oliveira"),
            new JogadorTime("Gilvan"),
            new JogadorTime("Everton Felipe"),
            new JogadorTime("Wellington Rato"),
            new JogadorTime("Junior Brandão"),
        };

        private static List<Jogador> JogadoresAteticoMineiro = new List<Jogador>()
        {
            new JogadorTime("Éverson"),
            new JogadorTime("Guga"),
            new JogadorTime("Réver"),
            new JogadorTime("junior Alonso"),
            new JogadorTime("Guilherme Arana"),
            new JogadorTime("Jair"),
            new JogadorTime("Franco"),
            new JogadorTime("Nathan"),
            new JogadorTime("Savarino"),
            new JogadorTime("Savarino"),
            new JogadorTime("Eduardo Sasha"),
            new JogadorTime("Keno"),
            new JogadorTime("Victor"),
            new JogadorTime("Rafael"),
            new JogadorTime("Mailton"),
            new JogadorTime("Mariano"),
            new JogadorTime("Igor Rabello"),
            new JogadorTime("Bueno"),
            new JogadorTime("Gabriel"),
            new JogadorTime("Borrero"),
            new JogadorTime("Calebe"),
            new JogadorTime("Savio"),
            new JogadorTime("Marrony"),
            new JogadorTime("Marquinhos"),
        };


        private static List<Jogador> JogadoresBahia = new List<Jogador>()
        {
            new JogadorTime("Douglas Friedrich"),
            new JogadorTime("Ernando"),
            new JogadorTime("Lucas Fonseca"),
            new JogadorTime("Juninho"),
            new JogadorTime("Juninho Capixaba"),
            new JogadorTime("Gregore"),
            new JogadorTime("Elias"),
            new JogadorTime("Edson"),
            new JogadorTime("Ramon"),
            new JogadorTime("Fessin"),
            new JogadorTime("Clayson"),
            new JogadorTime("Mateus Claus"),
            new JogadorTime("Wanderson"),
            new JogadorTime("Nino Paraiba"),
            new JogadorTime("Anderson Martins"),
            new JogadorTime("Matheus Bahia"),
            new JogadorTime("Ronaldo"),
            new JogadorTime("Daniel"),
            new JogadorTime("Ramires"),
            new JogadorTime("Alesson"),
            new JogadorTime("Marco Antonio"),
            new JogadorTime("Gilberto"),
            new JogadorTime("Saldanha"),
            
        };
        private static List<Jogador> JogadoresBotafogo = new List<Jogador>()
        {
            new JogadorTime("Diego Cavalieri"),
            new JogadorTime("Kevin"),
            new JogadorTime("Marcelo Benevenuto"),
            new JogadorTime("Kanu"),
            new JogadorTime("Victor Luis"),
            new JogadorTime("Rafael Forster"),
            new JogadorTime("Caio Alexandre"),
            new JogadorTime("Honda"),
            new JogadorTime("Bruno Nazario"),
            new JogadorTime("Rhuan"),
            new JogadorTime("Pedro Raul"),
            new JogadorTime("Saulo"),
            new JogadorTime("Barrandeguy"),
            new JogadorTime("David Souza"),
            new JogadorTime("Guilherme Santos"),
            new JogadorTime("Renteria"),
            new JogadorTime ("Eder Bessa"),
            new JogadorTime("Luiz Otávio"),
            new JogadorTime("Cicero"),
            new JogadorTime("matheus Babi"),
            new JogadorTime("Lecaros"),
            new JogadorTime("Warley"),
            new JogadorTime("Davi Araujo"),
        };
        private static List<Jogador> JogadoresBragantino = new List<Jogador>()
        {
            new JogadorTime("Cleiton"),
            new JogadorTime("Aderlan"),
            new JogadorTime("Léo Ortiz"),
            new JogadorTime("Ligger"),
            new JogadorTime("Wewerson"),
            new JogadorTime("Edimar"),
            new JogadorTime("Ricardo Ryller"),
            new JogadorTime("Uillian Correia"),
            new JogadorTime ("Raul"),
            new JogadorTime ("Arthur"),
            new JogadorTime("Cuello"),
            new JogadorTime("Bruno Tubarão"),
            new JogadorTime("Ytalo"),
            new JogadorTime("Hurtado"),
            new JogadorTime("Claudinho"),
            new JogadorTime("Luis Phelipe"),
            new JogadorTime("Julio Cesar"),
            new JogadorTime("Haydar"),
            new JogadorTime("Fabricio Brunno"),
            new JogadorTime("Chirgor"),
            new JogadorTime("Morato"),
        };


        private static List<Jogador> JogadoresCeara = new List<Jogador>()
        {
            new JogadorTime("Fernando Prass"),
            new JogadorTime("Fabinho"),
            new JogadorTime("Tiago"),
            new JogadorTime("Eduardo Brock"),
            new JogadorTime("Bruno Pacheco"),
            new JogadorTime("Pedro Naressi"),
            new JogadorTime("Leandro Carvalho"),
            new JogadorTime("Charles"),
            new JogadorTime("Fernando Sobral"),
            new JogadorTime("Vinicius"),
            new JogadorTime("Lima"),
            new JogadorTime("Leo Chu"),
            new JogadorTime("Kelvyn"),
            new JogadorTime("Rafael Sobis"),
            new JogadorTime("Cleber"),
            new JogadorTime("Diogo Silva"),
            new JogadorTime("Gabriel Lacerda"),
            new JogadorTime("Alyson"),
            new JogadorTime("Marthã"),
            new JogadorTime("Felipe Baixola"),
            new JogadorTime("Wescley"),
            new JogadorTime("Mateus Goncalves"),
        };

        private static List<Jogador> JogadoresFlamengo = new List<Jogador>()
        {
            new JogadorTime("Hugo Souza"),
            new JogadorTime("Diego Alves"),
            new JogadorTime("Mauricio Isla"),
            new JogadorTime("Rodrigo Caio"),
            new JogadorTime("Gustavo Henrique"),
            new JogadorTime("Natan"),
            new JogadorTime("Gabriel Noga"),
            new JogadorTime("Filipe Luis"),
            new JogadorTime("Thiago Maia"),
            new JogadorTime("Willian Arão"),
            new JogadorTime("Gerson"),
            new JogadorTime("Everton Ribeiro"),
            new JogadorTime("Ramon"),
            new JogadorTime("De Arrascaeta"),
            new JogadorTime("Bruno Henrique"),
            new JogadorTime("Pedro"),
            new JogadorTime("Lincoln"),
            new JogadorTime("Vitinho"),
            new JogadorTime("Diego"),
            new JogadorTime("Gabigol"),
            new JogadorTime("Cézar"),
            new JogadorTime("Gabriel Batista"),
            new JogadorTime("Matheuzinho"),
            new JogadorTime("João Lucas"),
            new JogadorTime("Thuler"),
            new JogadorTime("Léo Pereira"),
            new JogadorTime("Renê"),
            new JogadorTime("Gomes"),
            new JogadorTime("Pepe"),
            new JogadorTime("Pedro Rocha"),
            new JogadorTime("Michael"),
        };

        private static void AdicionarJogadoresAoTimes()
        {
            times[0].AdicionarListaDeJogadores(JogadoresAthletico);
            times[1].AdicionarListaDeJogadores(JogadoresAtleticoGoianiense);
            times[2].AdicionarListaDeJogadores(JogadoresAteticoMineiro);
            times[3].AdicionarListaDeJogadores(JogadoresBahia);
            times[4].AdicionarListaDeJogadores(JogadoresBotafogo);
            times[5].AdicionarListaDeJogadores(JogadoresBragantino);
            times[6].AdicionarListaDeJogadores(JogadoresCeara);
            times[7].AdicionarListaDeJogadores(JogadoresFlamengo);
        }
                
        public static List<Time> ObterTimes()
        {
            AdicionarJogadoresAoTimes();
            return times;
        }
        public static Time ObterTime(Guid idTime)
        {
            return ObterTimes().FirstOrDefault(t => t.Id == idTime);
        }

        public static IReadOnlyCollection<Jogador> ObterJogadoresDoTime(Guid idTime)
        {
            return ObterTime(idTime).Jogadores;
        }

        public static Jogador ObterJogadorDoTime(Guid idTime, Guid idJogador)
        {
            return ObterJogadoresDoTime(idTime).FirstOrDefault(j => j.Id == idJogador);
        }

        public static void AdicionarTime(Time timeAAdicionar)
        {
            times.Add(timeAAdicionar);
        }

        public static void RemoverTime(Guid idTime)
        {
            var timeARemover = Times.FirstOrDefault(t => t.Id == idTime);
            times.Remove(timeARemover);
        }

        public static bool AdicionarJogadorAoTime(Guid idTime, Jogador jogador)
        {
            var timeSelecionado = ObterTimes().FirstOrDefault(t => t.Id == idTime);
            return timeSelecionado.AdicionarJogador(jogador);
        }

        public static bool RemoverJogadorDoTime(Guid idTime, Guid idJogador)
        {
            var timeSelecionado = ObterTimes().FirstOrDefault(t => t.Id == idTime);
            return timeSelecionado.RemoverJogador(
                    timeSelecionado.Jogadores.FirstOrDefault(j => j.Id == idJogador)
            );
        }

        public static bool AdicionarJogadoresAoTime(Guid idTime, List<Jogador> jogadores)
        {
            return Times.FirstOrDefault(t => t.Id == idTime).AdicionarListaDeJogadores(jogadores);
        }
         
    }
}