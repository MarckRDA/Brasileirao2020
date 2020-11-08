using System.Collections.Generic;
using System.Linq;
using Domain.src.Campeonato;
using Domain.src.ExceptionsHandlers;
using Domain.src.Factories;
using Domain.src.Jogadores;
using Domain.src.Times;
using Domain.src.Users;
using Xunit;

namespace Tests
{
    public class CampeonatoBrasileiraoTests
    {

        [Fact]
        public void Deve_Retornar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Times_Com_Usuário_Torcedor()
        {
            //Given
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();

            //When

            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.CadastrarTimes(torcedor, GeradorDeTimesCompleto()));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Limite_Nao_Permitido_Ao_Tentar_Inscrever_Menos_De_Sete_Times_No_Campeonato()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();

            //When

            //Then
            Assert.Throws<LimiteNaoPermitidoException>(() => campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeSeisTimes()));

        }

        [Fact]
        public void Deve_Lancar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Times_No_Campeonato_Depois_Dele_Ter_Começado()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorNatan = campeonatoBrasileirao.ObterListaTimes().First(x => x.Id == idFlamengo).Jogadores.First(x => x.Nome == "Natan");


            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.RemoverJogadorTime(torcedor, idFlamengo, jogadorNatan));

        }

        [Fact]
        public void Deve_Lancar_Excecao_Permissao_Negada_Ao_Tentar_Adicionar_Um_Jogador_Com_Usuario_Torcedor()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            var idFlamengo = campeonatoBrasileirao.ObterListaTimes().First(x => x.NomeTime == "Flamengo").Id;
            var jogadorNatan = campeonatoBrasileirao.ObterListaTimes().First(x => x.Id == idFlamengo).Jogadores.First(x => x.Nome == "Natan");


            //Then
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.AdicionarJogadorTime(torcedor, idFlamengo, jogadorNatan));
        }

        [Fact]
        public void Deve_Retornar_Verdadeiro_Ao_Tentar_Remover_Um_Jogador_Com_Usuario_CBF()
        {
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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

            var resultado = campeonatoBrasileirao.ApresentarPartidas(cbf);
            
            
            //Then
            Assert.Equal(partidasAApresentar, resultado);
        }

        [Fact]
        public void Deve_Afirmar_Que_Flamengo_Jogará_Com_Todos_Os_Times()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());

            var jogosDoFlamengo = new List<string>()
            {
                "Atheltico X Flamengo",
                "Athletico Goianiense X Flamengo",
                "Athletico Mineiro X Flamengo",
                "Bahia X Flamengo",
                "Botafogo X Flamengo",
                "Bragantino X Flamengo",
                "Ceara X Flamengo"
            };

            //When
            var result = campeonatoBrasileirao.MostrarPartidasQueOTimeEnfrenta("Flamengo");

            //Then
            Assert.Equal(jogosDoFlamengo, result);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public void Deve_Retornar_Excecao_Permissao_Negada_Ao_Tentar_Inscrever_Os_Resultados_Da_Partida()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            
            //When
            var partidasAApresentar = new List<string>()
            {
                "------------Proxima Partida------------",
                "Partida1 -> Atheltico X Athletico Goianiense",
                "Partida2 -> Athletico Mineiro X Bahia",
                "Partida3 -> Botafogo X Bragantino",
                "Partida4 -> Ceara X Flamengo"
            };

            campeonatoBrasileirao.ApresentarPartidas(cbf);
            var Atheltico = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Atheltico").Tabela.GolsPro;

            //Then
            Assert.NotEqual(3, Atheltico);
            Assert.Throws<PermissaoNegadaException>(() => campeonatoBrasileirao.InscreverResultadoDaPartida(torcedor, 1, 3, 4));
        }

        [Fact]
        public void Deve_Inscrever_3_X_4_Na_Partida_Athletico_X_Athletico_Goianiense()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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
            
            var partidasExibidas = campeonatoBrasileirao.ApresentarPartidas(cbf);
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
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
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
            
            var partidasExibidas = campeonatoBrasileirao.ApresentarPartidas(cbf);
            
            var resultadoPrimeiraPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 1, 3, 4);
            
            var resultadoSegundoPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 2, 1, 3);

            var resultadoTerceiraPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 3, 1, 1);

            var resultadoQuartaPartida = campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 4, 0, 3);
            
            var jogadorSantos = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Santos", 2);
            
            var jogadorLeoGomes = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Leo Gomes", 1);
            
            var jogadorDudu = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Goianiense", "Dudu", 4);
            
            var jogadorRever = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Mineiro", "Réver", 1);
            
            var jogadorGilberto = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Gilberto", 2);
            
            var jogadorNinoParaiba = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Nino Paraiba", 1); 

            var jogadorPedroRaul = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Botafogo", "Pedro Raul", 1);

            var jogadorClaudinho = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bragantino", "Claudinho", 1);

            var jogadorPedro = campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Flamengo", "Pedro", 3);
            
            campeonatoBrasileirao.RegistrarRodada(cbf);
            
            var athelticoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Atheltico").Tabela.GolsPro;
            
            var athleticoGoianienceGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Goianiense").Tabela.GolsPro;
            
            var  athleticoMineiroGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Mineiro").Tabela.GolsPro;
            
            var  bahiaGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Bahia").Tabela.GolsPro;
            
            var botafogoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Botafogo").Tabela.GolsPro;

            var bragantinoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Bragantino").Tabela.GolsPro;
            
            var cearaGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Ceara").Tabela.GolsPro;

            var flamengoGols = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Flamengo").Tabela.GolsPro;
            
            
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
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            
            MockPrimeiraRodada(cbf, campeonatoBrasileirao);

            var tabela = new List<string>()
            {
                "---------------Tabela Brasileirão 2020 ----------------------",
                "Time  |  P  |  PD  |  V  |  E  |  D   | SG  |  GP  |  GC  |  PA",
                "1 - Athletico Goianiense | 3  |  1  |  1  |  0  |  0  |  1  |  4  |  3  |   100%",
                "2 - Bahia | 3  |  1  |  1  |  0  |  0  |  2  |  3  |  1  |   100%",
                "3 - Flamengo | 3  |  1  |  1  |  0  |  0  |  3  |  3  |  0  |   100%",
                "4 - Botafogo | 1  |  1  |  0  |  1  |  0  |  0  |  1  |  1  |   33,33%",
                "5 - Bragantino | 1  |  1  |  0  |  1  |  0  |  0  |  1  |  1  |   33,33%",
                "6 - Atheltico | 0  |  1  |  0  |  0  |  1  |  -1  |  3  |  4  |   0%",
                "7 - Athletico Mineiro | 0  |  1  |  0  |  0  |  1  |  -2  |  1  |  3  |   0%",
                "8 - Ceara | 0  |  1  |  0  |  0  |  1  |  -3  |  0  |  3  |   0%",

            };

            //When
            var tabelaDePontos = campeonatoBrasileirao.ApresentarTabela(cbf);

            //Then
            Assert.Equal(tabela, tabelaDePontos);
            Assert.Equal(10, tabelaDePontos.Count);
        }

        [Fact]
        public void Deve_Retornar_Resultados_Da_Rodada()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            MockPrimeiraRodada(cbf, campeonatoBrasileirao);
            
            var listaDeResultados = new List<string>()
            {
                "Rodada 1 Resultado: Atheltico 3 X 4 Athletico Goianiense",
                "Rodada 1 Resultado: Athletico Mineiro 1 X 3 Bahia",
                "Rodada 1 Resultado: Botafogo 1 X 1 Bragantino",
                "Rodada 1 Resultado: Ceara 0 X 3 Flamengo"
            };
            
            //When
            var resultados = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            
            //Then
            Assert.Equal(listaDeResultados, resultados);
        }

        [Fact]
        public void Deve_Retornar_A_Relacao_Dos_Artilheiros()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            MockPrimeiraRodada(cbf, campeonatoBrasileirao);
            var golsDudu = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Athletico Goianiense").Jogadores.FirstOrDefault(x => x.Nome == "Dudu").Gol; 
            var golsPedro = campeonatoBrasileirao.ObterListaTimes().FirstOrDefault(x => x.NomeTime == "Flamengo").Jogadores.FirstOrDefault(x => x.Nome == "Pedro").Gol; 
            
            var possivelListaDeArtilheiros = new List<string>()
            {
                "1 Dudu - 4 Gols",
                "2 Pedro - 3 Gols",
                "3 Santos - 2 Gols",
                "4 Gilberto - 2 Gols",
                "5 Leo Gomes - 1 Gols"
            };

            //When
            var atualListaDeArtilheiros = campeonatoBrasileirao.ExibirClassificacaoDeArtilheiros(cbf);

            //Then
            Assert.Equal(possivelListaDeArtilheiros, atualListaDeArtilheiros);
            Assert.Equal(4, golsDudu);
            Assert.Equal(3, golsPedro);
        }


        [Fact]
        public void Deve_Retornar_Os_Times_Rebaixados()
        {
             //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            MockPrimeiraRodada(cbf, campeonatoBrasileirao);
            var provaveisTimesRebaixados = new List<string>()
            {
                "5 - Bragantino | 1  |  1  |  0  |  1  |  0  |  0  |  1  |  1  |   33,33%",
                "6 - Atheltico | 0  |  1  |  0  |  0  |  1  |  -1  |  3  |  4  |   0%",
                "7 - Athletico Mineiro | 0  |  1  |  0  |  0  |  1  |  -2  |  1  |  3  |   0%",
                "8 - Ceara | 0  |  1  |  0  |  0  |  1  |  -3  |  0  |  3  |   0%",
            };

            //When
            var atualRebaixados = campeonatoBrasileirao.ExibirTimesRebaixados(torcedor);

            //Then
            Assert.Equal(provaveisTimesRebaixados, atualRebaixados);
        }

        [Fact]
        public void Deve_Retornar_Os_Times_Classificados_Para_A_Libertadores()
        {
            //Given
            var cbf = new UsuarioServices().CriarUsuario("admin", "Carlos");
            var torcedor = new UsuarioServices().CriarUsuario("torcedor", "Carlos");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            MockPrimeiraRodada(cbf, campeonatoBrasileirao);
            var provaveisTimesClassificados = new List<string>()
            {
                "1 - Athletico Goianiense | 3  |  1  |  1  |  0  |  0  |  1  |  4  |  3  |   100%",
                "2 - Bahia | 3  |  1  |  1  |  0  |  0  |  2  |  3  |  1  |   100%",
                "3 - Flamengo | 3  |  1  |  1  |  0  |  0  |  3  |  3  |  0  |   100%",
                "4 - Botafogo | 1  |  1  |  0  |  1  |  0  |  0  |  1  |  1  |   33,33%",
            };

            //When
            var atualClassificados = campeonatoBrasileirao.ExibirTimesClassificadosLibertadores(torcedor);

            //Then
            Assert.Equal(provaveisTimesClassificados, atualClassificados);
        }




        //* Mocks

        public void MockPrimeiraRodada(Usuario cbf, ICampeonato campeonatoBrasileirao)
        {
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.ApresentarPartidas(cbf);
            campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 1, 3, 4);
            campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 2, 1, 3);
            campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 3, 1, 1);
            campeonatoBrasileirao.InscreverResultadoDaPartida(cbf, 4, 0, 3);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Santos", 2);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Atheltico", "Leo Gomes", 1);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Goianiense", "Dudu", 4);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Athletico Mineiro", "Réver", 1);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Gilberto", 2);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bahia", "Nino Paraiba", 1); 
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Botafogo", "Pedro Raul", 1);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Bragantino", "Claudinho", 1);
            campeonatoBrasileirao.RegistrarJogadoresGoleadoresDaPartida(cbf, "Flamengo", "Pedro", 3);
            campeonatoBrasileirao.RegistrarRodada(cbf);
        }

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