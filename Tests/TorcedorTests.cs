using Domain.src.Users;
using Xunit;

namespace Tests
{
    public class TorcedorTests
    {
        [Fact]
        public void Should_Create_An_Torcedor_User()
        {
            //*Given
            Usuario carla = new Torcedor("Carlos");
            
            //*Then
            Assert.NotNull(carla);
        }
    }
}