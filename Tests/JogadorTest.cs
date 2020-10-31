using Domain;
using Domain.src;
using Xunit;

namespace Tests
{
    public class JogadorTests
    {
        [Fact]
        public void Should_Create_A_JogadorTime()
        {
            //Given
            Jogador marcos = new JogadorTime("Marcos");

            Assert.NotNull(marcos);
        }

        
        [Theory]
        [InlineData("Mar2cos alves")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Sa$@ni")]
        [InlineData("M,12.4 alves")]
        [InlineData(" Mlves")]
        [InlineData("Malves  ")]
        
        public void Should_return_false_Giving_Incorrect_Format_Names(string name)
        {
            //Given
            var jogador = new JogadorTime(name);

            //When
            var isValid = jogador.ValidarNomeJogador();

            //Then
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Marcos alves")]
        [InlineData("sabrina furtado")]
        public void Should_return_True_Giving_Correct_Format_Names(string name)
        {
            //Given
            var jogador = new JogadorTime(name);

            //When
            var isValid = jogador.ValidarNomeJogador();

            //Then
            Assert.True(isValid);
        }


        [Fact]
        public void Should_Put_One_Point_In_JogadorTime()
        {
            //Given
            Jogador anderson = new JogadorTime("Anderson");

            //When
            anderson.MarcarGol();

            //Then
            Assert.Equal(1, anderson.Gol);
        }

        [Fact]
        public void Shoud_Create_Two_Different_JogadoresTime_And_Compare_Their_ID()
        {
            //Given
            Jogador anderson = new JogadorTime("Anderson");
            Jogador fabio = new JogadorTime("Fábio");
                        
            //When
            var idAnderson = anderson.Id;
            var idFabio = fabio.Id;

            //Then
            Assert.NotEqual(idAnderson,idFabio);
        }
    }
}

