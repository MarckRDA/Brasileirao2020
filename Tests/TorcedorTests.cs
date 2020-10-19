using Domain;
using Xunit;

namespace Tests
{
    public class TorcedorTests
    {
        [Fact]
        public void Should_Create_An_Torcedor_User()
        {
            //*Given
            IUsuario carla = new Torcedor("Carla");
            
            //*Then
            Assert.NotNull(carla);
        }
    }
}