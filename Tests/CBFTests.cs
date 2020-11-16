using Domain.src.Users;
using Xunit;

namespace Tests
{
    public class CBFTests
    {
        [Fact]
        public void Should_Create_An_CBF_User()
        {
            //*Given
            Usuario cbf = new CBF("Carlos", "hahba", "cbf");

            Assert.NotNull(cbf); 
        }
    }
}