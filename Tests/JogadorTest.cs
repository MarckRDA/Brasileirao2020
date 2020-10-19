using Domain;
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

