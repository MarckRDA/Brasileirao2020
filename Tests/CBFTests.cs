using Domain;
using Xunit;

namespace Tests
{
    public class CBFTests
    {
        [Fact]
        public void Should_Create_An_CBF_User()
        {
            //*Given
            IUsuario cbf = new CBF("CBF");

            Assert.NotNull(cbf); 
        }
    }
}