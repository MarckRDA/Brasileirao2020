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
        public void Deve_Retornar_Resultado_Da_Rodada_Anfitrioes_Perdendo_Visitantes_Ganhando()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(3,4);
                        
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            var resultadoEsperado = new List<string>()
            {
                "Rodada 1 Resultado: Atheltico 3 X 4 Athletico Goianiense",
                "Rodada 1 Resultado: Athletico Mineiro 3 X 4 Bahia",
                "Rodada 1 Resultado: Botafogo 3 X 4 Bragantino",
                "Rodada 1 Resultado: Ceara 3 X 4 Flamengo"
            };

            Assert.Equal(resultadoEsperado, resultadoRodada);
            
        }

        public void Deve_Retornar_Resultado_Da_Rodada_Anfitrioes_Ganhando_Visitantes_Perdendo()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(4,3);
                        
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            var resultadoEsperado = new List<string>()
            {
                "Rodada 1 Resultado: Atheltico 4 X 3 Athletico Goianiense",
                "Rodada 1 Resultado: Athletico Mineiro 4 X 3 Bahia",
                "Rodada 1 Resultado: Botafogo 4 X 3 Bragantino",
                "Rodada 1 Resultado: Ceara 4 X 3 Flamengo"
            };

            Assert.Equal(resultadoEsperado, resultadoRodada);
            
        }

        [Fact]
        public void Deve_Retornar_Resultado_Rodada_Empate()
        {
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,1);
                        
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            var resultadoEsperado = new List<string>()
            {
                "Rodada 1 Resultado: Atheltico 1 X 1 Athletico Goianiense",
                "Rodada 1 Resultado: Athletico Mineiro 1 X 1 Bahia",
                "Rodada 1 Resultado: Botafogo 1 X 1 Bragantino",
                "Rodada 1 Resultado: Ceara 1 X 1 Flamengo"
            };

            Assert.Equal(resultadoEsperado, resultadoRodada);
        }

        [Fact]
        public void Deve_Retornar_Ceara_Perdendo()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(1,5);
            
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            var cearaPerde = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Ceara").Tabela.Derrotas;
            var cearaGolsRodada = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Ceara").Tabela.GolsPro;
            var cearaGolsRodadaContra = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Ceara").Tabela.GolsContra;
            
            //Then
            Assert.Equal(4, resultadoRodada.Count);
            Assert.Equal(1, cearaPerde);
            Assert.Equal(1, cearaGolsRodada);
            Assert.Equal(5, cearaGolsRodadaContra);
        }

        [Fact]
        public void Deve_Retornar_Resultado_Da_Rodada_Com_Flamengo_Ganhando()
        {
            //Given
            var cbf = new FabricaDeUsuarios().CriarUsuario("admin");
            var campeonatoBrasileirao = new FabricaDeCampeonato().CriarCampeonato();
            campeonatoBrasileirao.CadastrarTimes(cbf, GeradorDeTimesCompleto());
            campeonatoBrasileirao.GerarRodadaMockada(3,4);
            
            //When
            var resultadoRodada = campeonatoBrasileirao.ExibirResultadoDaRodada(cbf, 1);
            var flamengoGanha = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Flamengo").Tabela.Vitorias;
            var flamengoGolsRodada = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Flamengo").Tabela.GolsPro;
            var flamengoGolsRodadaContra = campeonatoBrasileirao.ObterListaTimes().First(x =>x.NomeTime== "Flamengo").Tabela.GolsContra;
            
            //Then
            Assert.Equal(4, resultadoRodada.Count);
            Assert.Equal(1, flamengoGanha);
            Assert.Equal(4, flamengoGolsRodada);
            Assert.Equal(3, flamengoGolsRodadaContra);
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
                "Atheltico | 10 | 6 | 3 | 1 | 2 | 16 | 6 | 55,56%",
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

        // public List<Jogador> GeradorDeJogadoresCorintians()
        // {
        //     List<Jogador> timeCorintians = new List<Jogador>()
        //    {
        //        new JogadorTime("Cassio"),
        //        new JogadorTime("Fagner"),
        //        new JogadorTime("Marllon"),
        //        new JogadorTime("Gil"),
        //        new JogadorTime("Lucas Piton"),
        //        new JogadorTime("Xavier"),
        //        new JogadorTime("Camacho"),
        //        new JogadorTime("Gabriel"),
        //        new JogadorTime("Otero"),
        //        new JogadorTime("Gustavo Mantuan"),
        //        new JogadorTime("Mateus Vital"),
        //        new JogadorTime("Cazares"),
        //        new JogadorTime("Everaldo"),
        //        new JogadorTime("Gustavo Mosquito"),
        //        new JogadorTime("Boselli"),
        //        new JogadorTime("Luan"),
        //        new JogadorTime("Walter"),
        //        new JogadorTime("Michel Macedo"),
        //        new JogadorTime("Sidcley"),
        //        new JogadorTime("Éderson"),
        //        new JogadorTime("Cantillo"),
        //        new JogadorTime("Roni"),
        //        new JogadorTime("Leo Natel"),
        //        new JogadorTime("Jô"),
        //    };
        //     return timeCorintians;
        // }

        // public List<Jogador> GeradorDeJogadoresCoritiba()
        // {

        //     List<Jogador> timeCoritiba = new List<Jogador>()
        //   {
        //     new JogadorTime("Wilson"),
        //     new JogadorTime("Natanael"),
        //     new JogadorTime("Matheus Galdezani"),
        //     new JogadorTime("Hanrique Vermudt"),
        //     new JogadorTime("Nathan Silva"),
        //     new JogadorTime("Willian Mateus"),
        //     new JogadorTime("Matheus Salles"),
        //     new JogadorTime("Matheus Bueno"),
        //     new JogadorTime("Hugo Moura"),
        //     new JogadorTime("Ricardo Oliveira"),
        //     new JogadorTime("Yan Sasse"),
        //     new JogadorTime("Nathan"),
        //     new JogadorTime("Giovanni Augusto"),
        //     new JogadorTime("Robson"),
        //     new JogadorTime("Rodrigo Muniz"),
        //     new JogadorTime("Neilton"),
        //     new JogadorTime("Alex Muralha"),
        //     new JogadorTime("Ramón Martinez"),
        //     new JogadorTime("Sarrafiore"),
        //     new JogadorTime("Gabriel"),
        //     new JogadorTime("Luiz Henrique"),
        //     new JogadorTime("Mattheus"),
        //     new JogadorTime("Cerutti"),
        //   };
        //     return timeCoritiba;
        // }
        // public List<Jogador> GeradorDeJogadoresFlamengo()
        // {


        //     List<Jogador> timeFlamengo = new List<Jogador>()
        //    {
        //      new JogadorTime("Hugo Souza"),
        //      new JogadorTime("Diego Alves"),
        //      new JogadorTime("Mauricio Isla"),
        //      new JogadorTime("Rodrigo Caio"),
        //      new JogadorTime("Gustavo Henrique"),
        //      new JogadorTime("Natan"),
        //      new JogadorTime("Gabriel Noga"),
        //      new JogadorTime("Filipe Luis"),
        //      new JogadorTime("Thiago Maia"),
        //      new JogadorTime("Willian Arão"),
        //      new JogadorTime("Gerson"),
        //      new JogadorTime("Everton Ribeiro"),
        //      new JogadorTime("Ramon"),
        //      new JogadorTime("De Arrascaeta"),
        //      new JogadorTime("Bruno Henrique"),
        //      new JogadorTime("Pedro"),
        //      new JogadorTime("Lincoln"),
        //      new JogadorTime("Vitinho"),
        //      new JogadorTime("Diego"),
        //      new JogadorTime("Gabigol"),
        //      new JogadorTime("Cézar"),
        //      new JogadorTime("Gabriel Batista"),
        //      new JogadorTime("Matheuzinho"),
        //      new JogadorTime("João Lucas"),
        //      new JogadorTime("Thuler"),
        //      new JogadorTime("Léo Pereira"),
        //      new JogadorTime("Renê"),
        //      new JogadorTime("Gomes"),
        //      new JogadorTime("Pepe"),
        //      new JogadorTime("Pedro Rocha"),
        //      new JogadorTime("Michael"),
        //    };
        //     return timeFlamengo;
        // }

        // public List<Jogador> GeradorDeJogadoresFluminense()
        // {



        //     List<Jogador> timeFluminense = new List<Jogador>()
        //    {
        //     new JogadorTime("Muriel Becker"),
        //     new JogadorTime("Igor Julião"),
        //     new JogadorTime("Nino"),
        //     new JogadorTime("Digão"),
        //     new JogadorTime("Danilo Barcelos"),
        //     new JogadorTime("Hudson"),
        //     new JogadorTime("Caio Paulista"),
        //     new JogadorTime("Dodi"),
        //     new JogadorTime("Nenê"),
        //     new JogadorTime("Ganso"),
        //     new JogadorTime("Yago Felipe"),
        //     new JogadorTime("André"),
        //     new JogadorTime("Felippe Cardoso"),
        //     new JogadorTime("Luiz Henrique"),
        //     new JogadorTime("Marcos Paulo"),
        //     new JogadorTime("Fred"),
        //     new JogadorTime("Marcos Felipe"),
        //     new JogadorTime("Callegari"),
        //     new JogadorTime("Matheus Ferraz"),
        //     new JogadorTime("Luccas Claro"),
        //     new JogadorTime("Egidio"),
        //     new JogadorTime("Miguel"),
        //     new JogadorTime("Lucca"),
        //     };
        //     return timeFluminense;
        // }
        // public List<Jogador> GeradorDeJogadoresFortaleza()
        // {

        //     List<Jogador> timeFortaleza = new List<Jogador>()
        //    {
        //        new JogadorTime("Felipe Alves"),
        //        new JogadorTime("Max Walef"),
        //        new JogadorTime("Tinga"),
        //        new JogadorTime("Gabriel Dias"),
        //        new JogadorTime("Paulão"),
        //        new JogadorTime("Roger Carvalho"),
        //        new JogadorTime("Bruno Melo"),
        //        new JogadorTime("felipe"),
        //        new JogadorTime("Marlon"),
        //        new JogadorTime("Ronald"),
        //        new JogadorTime("Romarinho"),
        //        new JogadorTime("Osvaldo"),
        //        new JogadorTime("Carlinhos"),
        //        new JogadorTime("David"),
        //        new JogadorTime("Juninho"),
        //        new JogadorTime("Yuri Cesar"),
        //        new JogadorTime("Wellington Paulista"),
        //        new JogadorTime("Jackson"),
        //        new JogadorTime("Derley"),
        //        new JogadorTime("Mariano Vazquez"),
        //        new JogadorTime("Bergson"),
        //        new JogadorTime("Igor Torres"),
        //        new JogadorTime("Éderson"),
        //    };
        //     return timeFortaleza;
        // }
        // public List<Jogador> GeradorDeJogadoresGoias()
        // {
        //     List<Jogador> timeGoias = new List<Jogador>()
        //    {
        //      new JogadorTime("Tadeu"),
        //      new JogadorTime("Edilson"),
        //      new JogadorTime("David Duarte"),
        //      new JogadorTime("Fábio Sanches"),
        //      new JogadorTime("Caju"),
        //      new JogadorTime("Breno"),
        //      new JogadorTime("Ariel Cabral"),
        //      new JogadorTime("Daniel Bessa"),
        //      new JogadorTime("Douglas Baggio"),
        //      new JogadorTime("Shaylon"),
        //      new JogadorTime("Fernandão"),
        //      new JogadorTime("Keko"),
        //      new JogadorTime("Ratinho"),
        //      new JogadorTime("Vinicius"),
        //      new JogadorTime("Matheus Alves"),
        //      new JogadorTime("Pintado"),
        //      new JogadorTime("Iago Mendonça"),
        //      new JogadorTime("Heron"),
        //      new JogadorTime("Jefferson"),
        //      new JogadorTime("Daniel Oliveira"),
        //      new JogadorTime("Salazar"),
        //      new JogadorTime("Sandrinho"),
        //    };
        //     return timeGoias;
        // }
        // public List<Jogador> GeradorDeJogadoresGremio()
        // {

        //     List<Jogador> timeGremio = new List<Jogador>()
        //    {
        //      new JogadorTime("Vanderlei"),
        //      new JogadorTime("Orejuela"),
        //      new JogadorTime("Pedro Geromel"),
        //      new JogadorTime("Kannemann"),
        //      new JogadorTime("Bruno Cortez"),
        //      new JogadorTime("Matheus Henrique"),
        //      new JogadorTime("Maicon"),
        //      new JogadorTime("Jean Pyerre"),
        //      new JogadorTime("Alison"),
        //      new JogadorTime("Thaciano"),
        //      new JogadorTime("Isaque"),
        //      new JogadorTime("lucas Silva"),
        //      new JogadorTime("Luiz Fernando"),
        //      new JogadorTime("Ferreira"),
        //      new JogadorTime("Pepê"),
        //      new JogadorTime("Paulo Victor"),
        //      new JogadorTime("Victor Ferraz"),
        //      new JogadorTime("David Braz"),
        //      new JogadorTime("Rodrigues"),
        //      new JogadorTime("Diogo Barbosa"),
        //      new JogadorTime("Everton"),
        //      new JogadorTime("Robinho"),
        //      new JogadorTime("Guilherme Azevedo"),
        //      new JogadorTime("Diego Souza"),
        //    };
        //     return timeGremio;
        // }
        // public List<Jogador> GeradorDeJogadoresInternacional()
        // {
        //     List<Jogador> timeInternacional = new List<Jogador>()
        //    {
        //      new JogadorTime("Marcelo Lomba"),
        //      new JogadorTime("Rodinei"),
        //      new JogadorTime("Zé Gabriel"),
        //      new JogadorTime("Victor Cuesta"),
        //      new JogadorTime("Uendel"),
        //      new JogadorTime("Rodrigo Lindoso"),
        //      new JogadorTime("Praxedes"),
        //      new JogadorTime("Edenilson"),
        //      new JogadorTime("Marcos Guilherme"),
        //      new JogadorTime("Rodrigo Moledo"),
        //      new JogadorTime("Patrick"),
        //      new JogadorTime("Rodrigo Dourado"),
        //      new JogadorTime("Thiago Galhardo"),
        //      new JogadorTime("D'alessandro"),
        //      new JogadorTime("Abel Hernandez"),
        //      new JogadorTime("Yuri Alberto"),
        //      new JogadorTime("Danilo Fernandes"),
        //      new JogadorTime("Mazetti"),
        //      new JogadorTime("Moisés"),
        //      new JogadorTime("Peglow"),
        //      new JogadorTime("Nonato"),
        //      new JogadorTime("William Pottker"),
        //      new JogadorTime("Leandro Fernandez"),
        //    };
        //     return timeInternacional;
        // }
        // public List<Jogador> GeradorDeJogadoresPalmeiras()
        // {
        //     List<Jogador> timePalmeiras = new List<Jogador>()
        //    {
        //      new JogadorTime("Weverton"),
        //      new JogadorTime("Mayke"),
        //      new JogadorTime("Wesley"),
        //      new JogadorTime("Emerson Santos"),
        //      new JogadorTime("Gustavo Gomez"),
        //      new JogadorTime("Vinã"),
        //      new JogadorTime("Gabriel Menino"),
        //      new JogadorTime("Patrick de Paula"),
        //      new JogadorTime("Willian Bigode"),
        //      new JogadorTime("Zé Rafael"),
        //      new JogadorTime("Raphael Veiga"),
        //      new JogadorTime("Danilo"),
        //      new JogadorTime("Lucas Lima"),
        //      new JogadorTime("luiz Adriano"),
        //      new JogadorTime("Gabriel Veron"),
        //      new JogadorTime("Jailson"),
        //      new JogadorTime("Vinicius Silvestre"),
        //      new JogadorTime("Renan"),
        //      new JogadorTime("Ramires"),
        //      new JogadorTime("Gustavo Scarpa"),
        //      new JogadorTime("Rony"),
        //    };
        //     return timePalmeiras;
        // }
        // public List<Jogador> GeradorDeJogadoresSantos()
        // {
        //     List<Jogador> timeSantos = new List<Jogador>()
        //    {
        //      new JogadorTime("João Paulo"),
        //      new JogadorTime("Madson"),
        //      new JogadorTime("Laércio"),
        //      new JogadorTime("Luan Peres"),
        //      new JogadorTime("Felipe Jonatan"),
        //      new JogadorTime("Jobson"),
        //      new JogadorTime("Sandry"),
        //      new JogadorTime("Diego Pituca"),
        //      new JogadorTime("Jean Mota"),
        //      new JogadorTime("Lucas Lourenço"),
        //      new JogadorTime("Lucas Braga"),
        //      new JogadorTime("Arthur Gomes"),
        //      new JogadorTime("Kaio Jorge"),
        //      new JogadorTime("Luiz Felipe"),
        //      new JogadorTime("Soteldo"),
        //      new JogadorTime("Wagner Leonardo"),
        //      new JogadorTime("John Victor"),
        //      new JogadorTime("Alex"),
        //      new JogadorTime("Ivonei"),
        //      new JogadorTime("Anderson Ceará"),
        //      new JogadorTime("Marcos Leonardo"),
        //      new JogadorTime("Tailson"),
        //     };
        //     return timeSantos;
        // }
        // public List<Jogador> GeradorDeJogadoresSaoPaulo()
        // {
        //     List<Jogador> timeSaoPaulo = new List<Jogador>()
        //    {
        //      new JogadorTime("Tiago Volpi"),
        //      new JogadorTime("Daniel Alves"),
        //      new JogadorTime("Diego"),
        //      new JogadorTime("Bruno Alves"),
        //      new JogadorTime("Reinaldo"),
        //      new JogadorTime("Luan"),
        //      new JogadorTime("Tchê Tchê"),
        //      new JogadorTime("Gabriel Sara"),
        //      new JogadorTime("Toró"),
        //      new JogadorTime("Igor Gomes"),
        //      new JogadorTime("Vitor Bueno"),
        //      new JogadorTime("Brenner"),
        //      new JogadorTime("Paulinho Boia"),
        //      new JogadorTime("Luciano"),
        //      new JogadorTime("Santiago Tréllez"),
        //      new JogadorTime("Thiago Couto"),
        //      new JogadorTime("Júnior"),
        //      new JogadorTime("Rodrigo Freitas"),
        //      new JogadorTime("Léo"),
        //      new JogadorTime("Rodrigo Nestor"),
        //      new JogadorTime("Pablo"),
        //      new JogadorTime("Gonzalo Carneiro"),
        //      new JogadorTime("Hernanes"),
        //      new JogadorTime("Liziero"),
        //      new JogadorTime("Juanfran"),
        //      new JogadorTime("Lucas Perri"),
        //      new JogadorTime("Igor Vinicius"),
        //      new JogadorTime("Rojas"),
        //      new JogadorTime("Walce"),
        //    };
        //     return timeSaoPaulo;
        // }
        // public List<Jogador> GeradorDeJogadoresSport()
        // {
        //     List<Jogador> timeSport = new List<Jogador>()
        //    {
        //      new JogadorTime("Luan Polli"),
        //      new JogadorTime("Patric"),
        //      new JogadorTime("Iago Maidana"),
        //      new JogadorTime("Adryelson"),
        //      new JogadorTime("Luciano Juba"),
        //      new JogadorTime("Marcão Silva"),
        //      new JogadorTime("Ronaldo Henrique"),
        //      new JogadorTime("Ricardinho"),
        //      new JogadorTime("Mikael"),
        //      new JogadorTime("Lucas Mugni"),
        //      new JogadorTime("Thiago Neves"),
        //      new JogadorTime("Jonathan Gomez"),
        //      new JogadorTime("Marquinhos"),
        //      new JogadorTime("Rogério"),
        //      new JogadorTime("Leandro Barcia"),
        //      new JogadorTime("Junior Tavares"),
        //      new JogadorTime("Mailson"),
        //      new JogadorTime("Ewerthon"),
        //      new JogadorTime("Rafael Thyere"),
        //      new JogadorTime("Chico"),
        //      new JogadorTime("Márcio Araujo"),
        //      new JogadorTime("Hernane Brocador"),
        //    };
        //     return timeSport;
        // }
        // public List<Jogador> GeradorDeJogadoresVasco()
        // {
        //   List<Jogador> timeVasco = new List<Jogador>()
        //   {
        //      new JogadorTime("Fernando Miguel"),
        //      new JogadorTime("Yago Pikachu"),
        //      new JogadorTime("Miranda"),
        //      new JogadorTime("Leandro Castan"),
        //      new JogadorTime("Henrique"),
        //      new JogadorTime("Andrey"),
        //      new JogadorTime("Felipe Bastos"),
        //      new JogadorTime("Marcos Junior"),
        //      new JogadorTime("Guilherme Parede"),
        //      new JogadorTime("Carlinhos"),
        //      new JogadorTime("Benitez"),
        //      new JogadorTime("Vinícius"),
        //      new JogadorTime("Tales Magno"),
        //      new JogadorTime("German Cano"),
        //      new JogadorTime("Lucão"),
        //      new JogadorTime("Ulisses"),
        //      new JogadorTime("Werley"),
        //      new JogadorTime("Neto Borges"),
        //      new JogadorTime("Gabriel Pec"),
        //      new JogadorTime("Lucas Santos"),
        //      new JogadorTime("Ribamar"),
        //      new JogadorTime("Tiago Reis"),
        //      new JogadorTime("Ygor Catatau"),
        //      new JogadorTime("Ricardo Graça"),
        //      new JogadorTime("Bruno Gomes"),
        //   };

        //     return timeVasco;
        // }

    }
}