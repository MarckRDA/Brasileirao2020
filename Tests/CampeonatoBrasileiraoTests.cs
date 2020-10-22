using System.Collections.Generic;
using System.Linq;
using Domain;
using Xunit;

namespace Tests
{
    public class CampeonatoBrasileiraoTests
    {

        [Fact]
        public void Deve_Retornar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Times_Com_Usuário_Torcedor()
        {
            //Given
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();

            //When

            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.CadastrarTimes(torcedor, GeradorDeTimesCompleto()));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Limite_Nao_Permitido_Ao_Tentar_Inscrever_Menos_De_Sete_Times_No_Campeonato()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();

            //When

            //Then
            Assert.Throws<LimiteNaoPermitidoException>(() => campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeSeisTimes()));

        }

        [Fact]
        public void Deve_Lancar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Times_No_Campeonato_Depois_Dele_Ter_Começado()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            //When

            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto()));

        }

        [Fact]
        public void Deve_Lancar_Excecao_Permissao_Negada_Ao_Tentar_Remover_Um_Jogador_Com_Usuario_Torcedor()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorNatan = campeonatoBrasileirao.ObterListaTimes().First(x => x.Id == idFlamengo).Jogadores.First(x => x.Nome == "Natan");


            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.RemoverJogadorTime(torcedor, idFlamengo, jogadorNatan));

        }

        [Fact]
        public void Deve_Lancar_Excecao_Permissao_Negada_Ao_Tentar_Adicionar_Um_Jogador_Com_Usuario_Torcedor()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorNatan = campeonatoBrasileirao.ObterListaTimes().First(x => x.Id == idFlamengo).Jogadores.First(x => x.Nome == "Natan");


            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.AdicionarJogadorTime(torcedor, idFlamengo, jogadorNatan));
        }

        [Fact]
        public void Deve_Retornar_Verdadeiro_Ao_Tentar_Remover_Um_Jogador_Com_Usuario_CBF()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());

            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorNatan = campeonatoBrasileirao.ObterListaTimes().First(x => x.Id == idFlamengo).Jogadores.First(x => x.Nome == "Natan");

            //When
            var result = campeonatoBrasileirao.RemoverJogadorTime(cbf, idFlamengo, jogadorNatan);

            //Then
            Assert.True(result);
        }

        [Fact]
        public void Deve_Retornar_Verdadeiro_Ao_Tentar_Adicionar_Um_Jogador_Com_Usuario_CBF()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());

            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorMarcos = new JogadorTime("Marcos");

            //When
            var result = campeonatoBrasileirao.AdicionarJogadorTime(cbf, idFlamengo, jogadorMarcos);

            //Then
            Assert.True(result);
        }

        [Fact]
        public void Deve_Retornar_Resultado_Da_Rodada()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(3,4);
                        
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf);
            var resultadoEsperado = new List<string>()
            {
                "Rodada 1 Resultado: Atheltico 3 X 4 Athletico Goianiense",
                "Rodada 1 Resultado: Athletico Mineiro 3 X 4 Bahia",
                "Rodada 1 Resultado: Botafogo 3 X 4 Bragantino",
                "Rodada 1 Resultado: Ceara 3 X 4 Flamengo"
            };

            Assert.Equal(resultadoEsperado, resultadoRodada);
            
        }

        
        [Fact]
        public void Deve_Retornar_A_Tabela_De_Pontos_Corridos()
        {
          //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,1);
            campeonatoBrasileirao.GerarRodadaMockada(1,2);
            campeonatoBrasileirao.GerarRodadaMockada(4,2);

            //When
            var tabelaAtualizadaAposTresRodadas = campeonatoBrasileirao.ApresentarTabela(torcedor);
            var tabelaEsperada = new List<string>()
            {
                "Atheltico | 10 | 6 | 3 | 1 | 2 | 14 | 5 | 55,56%",
                "Athletico Mineiro | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Botafogo | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Ceara | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Athletico Goianiense | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Bahia | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Bragantino | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Flamengo | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%"
            };

            Assert.Equal(tabelaEsperada, tabelaAtualizadaAposTresRodadas);
        }

        [Fact]
        public void Deve_Retornar_A_Relacao_Dos_Artilheiros()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,1);
            campeonatoBrasileirao.GerarRodadaMockada(1,2);
            campeonatoBrasileirao.GerarRodadaMockada(4,2);

            //When
            var tabelaArtilheiros = campeonatoBrasileirao.ExibirClassificacaoDeArtilheiros(torcedor);
            var tabelaDeArtilheirosExperada = new List<string>()
            {
                "1 Guilherme Santos - 4 Gols",
                "2 Rafael Forster - 3 Gols",
                "3 Aderlan - 3 Gols",
                "4 Fabinho - 2 Gols",
                "5 Wellington - 2 Gols"
            };
            
            //Then
            Assert.Equal(tabelaDeArtilheirosExperada, tabelaArtilheiros);
        }


        [Fact]
        public void Deve_Retornar_Os_Times_Rebaixados()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,1);
            campeonatoBrasileirao.GerarRodadaMockada(1,2);
            campeonatoBrasileirao.GerarRodadaMockada(4,2);

            //When
            var timesRebaixados = campeonatoBrasileirao.ExibirTimesRebaixados(torcedor);
            var timesReibaxadosEsperados = new List<string>()
            {
                "Athletico Goianiense | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Bahia | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Bragantino | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%",
                "Flamengo | 7 | 6 | 2 | 1 | 3 | 12 | 7 | 38,89%"
            };

            Assert.Equal(timesReibaxadosEsperados, timesRebaixados);
        }

        [Fact]
        public void Deve_Retornar_Os_Times_Classificados_Para_A_Libertadores()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,1);
            campeonatoBrasileirao.GerarRodadaMockada(1,2);
            campeonatoBrasileirao.GerarRodadaMockada(4,2);

            //When
            var timesClassificadosLibertadores = campeonatoBrasileirao.ExibirTimesClassificadosLibertadores(torcedor);
            var timesClassificadosLibertadoresEsperados = new List<string>()
            {
                "Atheltico | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Athletico Mineiro | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Botafogo | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
                "Ceara | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%"
            };

            Assert.Equal(timesClassificadosLibertadoresEsperados, timesClassificadosLibertadores);
        }



        //* Mocks

        public List<Time> GeradorDeTimesCompleto()
        {
            var timeAthletico = new TimeCampeonatoBrasileirao("Atheltico");
            timeAthletico.AdicionarListaDeJogadores(GeradorDeJogadoresAtheltico());

            var timeAthleticoGoianience = new TimeCampeonatoBrasileirao("Athletico Goianiense");
            timeAthleticoGoianience.AdicionarListaDeJogadores(GeradorDeJogadoresAteticoGoianiense());

            var timeAthleticoMineiro = new TimeCampeonatoBrasileirao("Athletico Mineiro");
            timeAthleticoMineiro.AdicionarListaDeJogadores(GeradorDeJogadoresAteticoMineiro());

            var timeBahia = new TimeCampeonatoBrasileirao("Bahia");
            timeBahia.AdicionarListaDeJogadores(GeradorDeJogadoresBahia());

            var timeBotafogo = new TimeCampeonatoBrasileirao("Botafogo");
            timeBotafogo.AdicionarListaDeJogadores(GeradorDeJogadoresBotafogo());

            var timeBragantino = new TimeCampeonatoBrasileirao("Bragantino");
            timeBragantino.AdicionarListaDeJogadores(GeradorDeJogadoresBragantino());

            var timeCeara = new TimeCampeonatoBrasileirao("Ceara");
            timeCeara.AdicionarListaDeJogadores(GeradorDeJogadoresCeara());

            var timeFlamengo = new TimeCampeonatoBrasileirao("Flamengo");
            timeFlamengo.AdicionarListaDeJogadores(GeradorTimeFlamengo());


            List<Time> listaTimes = new List<Time>
            {
                timeAthletico, timeAthleticoGoianience, timeAthleticoMineiro, timeBahia,
                timeBotafogo, timeBragantino, timeCeara, timeFlamengo
            };

            return listaTimes;
        }

        public List<Time> GeradorDeSeisTimes()
        {

            var timeAthleticoMineiro = new TimeCampeonatoBrasileirao("Athletico Mineiro");
            timeAthleticoMineiro.AdicionarListaDeJogadores(GeradorDeJogadoresAteticoMineiro());

            var timeBahia = new TimeCampeonatoBrasileirao("Bahia");
            timeBahia.AdicionarListaDeJogadores(GeradorDeJogadoresBahia());

            var timeBotafogo = new TimeCampeonatoBrasileirao("Botafogo");
            timeBotafogo.AdicionarListaDeJogadores(GeradorDeJogadoresBotafogo());

            var timeBragantino = new TimeCampeonatoBrasileirao("Bragantino");
            timeBragantino.AdicionarListaDeJogadores(GeradorDeJogadoresBragantino());

            var timeCeara = new TimeCampeonatoBrasileirao("Ceara");
            timeCeara.AdicionarListaDeJogadores(GeradorDeJogadoresCeara());

            var timeFlamengo = new TimeCampeonatoBrasileirao("Flamengo");
            timeFlamengo.AdicionarListaDeJogadores(GeradorTimeFlamengo());


            List<Time> listaTimes = new List<Time>
            {
                timeAthleticoMineiro, timeBahia,
                timeBotafogo, timeBragantino, timeCeara, timeFlamengo
            };

            return listaTimes;
        }
        public List<Jogador> GeradorDeJogadoresAtheltico()
        {
            List<Jogador> timeAthelticoParanaense = new List<Jogador>()
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

            return timeAthelticoParanaense;

        }
        public List<Jogador> GeradorDeJogadoresAteticoGoianiense()
        {

            List<Jogador> timeAtleticoGoianiense = new List<Jogador>()
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

            return timeAtleticoGoianiense;
        }

        public List<Jogador> GeradorDeJogadoresAteticoMineiro()
        {
            List<Jogador> timeAtleticoMineiro = new List<Jogador>()
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

            return timeAtleticoMineiro;
        }


        public List<Jogador> GeradorDeJogadoresBahia()
        {
            List<Jogador> timeBahia = new List<Jogador>()
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
            return timeBahia;

        }
        public List<Jogador> GeradorDeJogadoresBotafogo()
        {

            List<Jogador> timeBotafogo = new List<Jogador>()
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

            return timeBotafogo;
        }
        public List<Jogador> GeradorDeJogadoresBragantino()
        {
            List<Jogador> timeBragantino = new List<Jogador>()
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

            return timeBragantino;
        }


        public List<Jogador> GeradorDeJogadoresCeara()
        {
            List<Jogador> timeCeara = new List<Jogador>()
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
            return timeCeara;
        }

        public List<Jogador> GeradorTimeFlamengo()
        {
            List<Jogador> timeFlamengo = new List<Jogador>()
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
            return timeFlamengo;
        }

        
    }
}