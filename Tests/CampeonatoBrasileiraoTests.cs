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
        public void Deve_Retornar_As_Partidas_Da_Primeira_Rodada()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            
            //When
            var partidasAApresentar = new List<string>()
            {
                "------------Proxima Partida------------",
                "Partida1 -> Atheltico X Athletico Goianiense",
                "Partida2 -> Athletico Mineiro X Bahia",
                "Partida3 -> Botafogo X Bragantino",
                "Partida4 -> Ceara X Flamengo"
            };

            var resultado = campeonatoBrasileirao.ApresentarPartidas();
            
            //Then
            Assert.Equal(partidasAApresentar, resultado);
        }

        [Fact]
        public void Deve_Retornar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Os_Resultados_Da_Partida()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new FabricaDeUsuarios().CriarUsuario("torcedor");
            
            //When
            var partidasAApresentar = new List<string>()
            {
                "------------Proxima Partida------------",
                "Partida1 -> Atheltico X Athletico Goianiense",
                "Partida2 -> Athletico Mineiro X Bahia",
                "Partida3 -> Botafogo X Bragantino",
                "Partida4 -> Ceara X Flamengo"
            };

            campeonatoBrasileirao.ApresentarPartidas();
            var Atheltico = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Atheltico").Tabela.GolsPro;

            //Then
            Assert.NotEqual(3, Atheltico);
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.InscreverResultadoDaPartida(torcedor, 1, 3, 4));
        }

        [Fact]
        public void Deve_Inscrever_3_X_4_Na_Partida_Athletico_X_Athletico_Goianiense()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            
            //When
            var partidasAApresentar = new List<string>()
            {
                "------------Proxima Partida------------",
                "Partida1 -> Atheltico X Athletico Goianiense",
                "Partida2 -> Athletico Mineiro X Bahia",
                "Partida3 -> Botafogo X Bragantino",
                "Partida4 -> Ceara X Flamengo"
            };
            
            var partidasExibidas = campeonatoBrasileirao.ApresentarPartidas();
            var inscreverResultadoPrimeiraPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 1, 3, 4);
            var jogadorSantos = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Santos", 2);
            var jogadorLeoGomes = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Leo Gomes", 1);
            var jogadorDudu = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Goianiense", "Dudu", 4);
            var atheltico = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Atheltico").Tabela.GolsPro;
            var athleticoGoianience = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Goianiense").Tabela.GolsPro;

            
            //Then
            Assert.True(inscreverResultadoPrimeiraPartida);
            Assert.Equal(partidasAApresentar, partidasExibidas);
            Assert.Equal(3, atheltico);
            Assert.Equal(4, athleticoGoianience);
        }

        [Fact]
        public void Deve_Increver_Os_Resultados_Da_Primeira_Rodada()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            
            //When
            var partidasAApresentar = new List<string>()
            {
                "------------Proxima Partida------------",
                "Partida1 -> Atheltico X Athletico Goianiense",
                "Partida2 -> Athletico Mineiro X Bahia",
                "Partida3 -> Botafogo X Bragantino",
                "Partida4 -> Ceara X Flamengo"
            };
            
            var partidasExibidas = campeonatoBrasileirao.ApresentarPartidas();
            
            var resultadoPrimeiraPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 1, 3, 4);
            
            var resultadoSegundoPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 2, 1, 3);

            var resultadoTerceiraPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 3, 1, 1);

            var resultadoQuartaPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 4, 0, 3);
            
            var athelticoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Atheltico").Tabela.GolsPro;
            
            var athleticoGoianienceGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Goianiense").Tabela.GolsPro;
            
            var  athleticoMineiroGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Mineiro").Tabela.GolsPro;
            
            var  bahiaGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Bahia").Tabela.GolsPro;
            
            var botafogoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Botafogo").Tabela.GolsPro;

            var bragantinoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Bragantino").Tabela.GolsPro;
            
            var cearaGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Ceara").Tabela.GolsPro;

            var flamengoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Flamengo").Tabela.GolsPro;
            
            var jogadorSantos = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Santos", 2);
            
            var jogadorLeoGomes = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Leo Gomes", 1);
            
            var jogadorDudu = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Goianiense", "Dudu", 4);
            
            var jogadorRever = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Mineiro", "Réver", 1);
            
            var jogadorGilberto = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Gilberto", 2);
            
            var jogadorNinoParaiba = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Nino Paraiba", 1); 

            var jogadorPedroRaul = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Botafogo", "Pedro Raul", 1);

            var jogadorClaudinho = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bragantino", "Claudinho", 1);

            var jogadorPedro = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Flamengo", "Pedro", 3);
            
            
            //Then
            Assert.Equal(partidasAApresentar, partidasExibidas);
            Assert.True(resultadoPrimeiraPartida);
            Assert.Equal(1, athleticoMineiroGols);
            Assert.Equal(3, bahiaGols);
            Assert.Equal(1, botafogoGols);
            Assert.Equal(1, bragantinoGols);
            Assert.Equal(0, cearaGols);
            Assert.Equal(3, flamengoGols);
            Assert.True(jogadorSantos);
            Assert.True(jogadorLeoGomes);
            Assert.True(jogadorClaudinho);
            Assert.True(jogadorDudu);
            Assert.True(jogadorGilberto);
            Assert.True(jogadorNinoParaiba);
            Assert.True(jogadorRever);
            Assert.True(jogadorGilberto);
            Assert.True(jogadorPedro);
            Assert.True(jogadorPedroRaul);
        }

        
        [Fact]
        public void Deve_Retornar_A_Tabela_De_Pontos_Corridos()
        {
          
        }

        [Fact]
        public void Deve_Retornar_A_Relacao_Dos_Artilheiros()
        {
            
            
        }


        [Fact]
        public void Deve_Retornar_Os_Times_Rebaixados()
        {
            
            
        }

        [Fact]
        public void Deve_Retornar_Os_Times_Classificados_Para_A_Libertadores()
        {
                    
        }



        //* Mocks

        public List<Time> GeradorDeTimesCompleto()
        {
            var timeAthletico = new TimeCampeonatoBrasileirao("Atheltico");
            timeAthletico.AdicionarListaDeJogadores(GeradorDeJogadoresAthletico());

            var timeAthleticoGoianience = new TimeCampeonatoBrasileirao("Athletico Goianiense");
            timeAthleticoGoianience.AdicionarListaDeJogadores(GeradorDeJogadoresAtleticoGoianiense());

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
        public List<Jogador> GeradorDeJogadoresAthletico()
        {
            List<Jogador> timeAthleticoParanaense = new List<Jogador>()
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

            return timeAthleticoParanaense;

        }
        public List<Jogador> GeradorDeJogadoresAtleticoGoianiense()
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