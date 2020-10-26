using System.Collections.Generic;
using System.Linq;
using Domain;
using Xunit;

namespace Tests
{
    public class TimeTests
    {
        [Fact]
        public void Should_Create_A_Time()
        {
            //Given
            Time atletico = new TimeCampeonatoBrasileirao("Atletico");

            //When

            //Then
            Assert.NotNull(atletico);
        }

        
        [Theory]
        [InlineData("Inter2cional")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Sa$@ni")]
        [InlineData("M,12acpa")]
        [InlineData(" Mlves")]
        [InlineData("Malves  ")]
        
        public void Should_return_false_Giving_Incorrect_Format_Names(string name)
        {
            //Given
            var time = new TimeCampeonatoBrasileirao(name);

            //When
            var isValid = time.ValidarNomeTime();

            //Then
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Internacional")]
        [InlineData("Bragantino")]
        public void Should_return_True_Giving_Correct_Format_Names(string name)
        {
            //Given
            var time = new TimeCampeonatoBrasileirao(name);

            //When
            var isValid = time.ValidarNomeTime();

            //Then
            Assert.True(isValid);
        }

        [Fact]
        public void Should_ADD_Playes_In_Athletico()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAtheltico();

            //When
            var result = atlhetico.AdicionarListaDeJogadores(listaJogadores);

            //Then
            Assert.True(result);

        }

        [Fact]
        public void Should_Return_False_Trying_To_Remove_A_Player()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAtheltico();
            atlhetico.AdicionarListaDeJogadores(listaJogadores);
            var jogadorASerRemovido = new JogadorTime("Thanos");

            //When
            var result = atlhetico.RemoverJogador(jogadorASerRemovido);

            //Then
            Assert.False(result);
        }


        [Fact]
        public void Should_Return_True_Trying_To_Remove_A_Player()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAtheltico();
            atlhetico.AdicionarListaDeJogadores(listaJogadores);
            var jogadorASerRemovido = atlhetico.Jogadores.First(j => j.Nome == "Santos");

            //When
            var result = atlhetico.RemoverJogador(jogadorASerRemovido);

            //Then
            Assert.True(result);
        }

        [Fact]
        public void Should_Return_False_Trying_To_Add_A_Player_In_Team()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAtheltico();
            atlhetico.AdicionarListaDeJogadores(listaJogadores);
            var jogadorASerAdicionado = listaJogadores.FirstOrDefault(x => x.Nome == "Santos");

            //When
            var result = atlhetico.AdicionarJogador(jogadorASerAdicionado);

            //Then
            Assert.False(result);
        }


        [Fact]
        public void Should_Return_True_Trying_To_Add_A_Player_In_Team()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAtheltico();
            atlhetico.AdicionarListaDeJogadores(listaJogadores);
            var jogadorASerAdicionado = new JogadorTime("Garrincha");

            //When
            var result = atlhetico.AdicionarJogador(jogadorASerAdicionado);

            //Then
            Assert.True(result);
        }


        [Fact]
        public void Should_Create_Two_Different_Times()
        {
            //Given
            Time atheltico = new TimeCampeonatoBrasileirao("Atheltico");
            Time Bahia = new TimeCampeonatoBrasileirao("Bahia");

            //When
            var idAtheltico = atheltico.Id;
            var idBahia = Bahia.Id;

            //Then
            Assert.NotEqual(idAtheltico, idBahia);
        }

        [Fact]
        public void Should_Comparing_Jogadores_In_Two_Different_Times()
        {
            //Given
            Time atheltico = new TimeCampeonatoBrasileirao("Atheltico");
            Time Bahia = new TimeCampeonatoBrasileirao("Bahia");
            //When
            atheltico.AdicionarListaDeJogadores(GeradorDeJogadoresAtheltico());
            Bahia.AdicionarListaDeJogadores(GeradorDeJogadoresBahia());

            //Then
            Assert.NotEqual(atheltico.Jogadores, Bahia.Jogadores);
        }

        [Fact]
        public void Should_Return_False_When_Criate_A_Time_With_Less_Then_16_Jogadores()
        {
            //Given
            Time atheltico = new TimeCampeonatoBrasileirao("Atheltico");

            //When
            var result = atheltico.AdicionarListaDeJogadores(GeradorDeJogadoresAbaixoDoLimite());
            //Then
            Assert.False(result);
        }

        [Fact]
        public void Should_Return_False_When_Create_A_Time_With_More_Than_32_Jogadores()
        {
            //Given
            Time atheltico = new TimeCampeonatoBrasileirao("Atheltico");

            //When
            var result = atheltico.AdicionarListaDeJogadores(GeradorDeJogadoresAcimaDoLimite());
            //Then
            Assert.False(result);
        }



        //* Mocks

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
        public List<Jogador> GeradorDeJogadoresAbaixoDoLimite()
        {
            List<Jogador> timeAtleticoMineiro = new List<Jogador>()
            {
              new JogadorTime("Éverson"),
              new JogadorTime("Guga"),
              new JogadorTime("Réver"),
              new JogadorTime("junior Alonso"),
              new JogadorTime("Guilherme Arana"),
              new JogadorTime("Jair"),

            };

            return timeAtleticoMineiro;
        }

        public List<Jogador> GeradorDeJogadoresAcimaDoLimite()
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
              new JogadorTime("Franco"),
              new JogadorTime("Nathan"),
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
    };
}

