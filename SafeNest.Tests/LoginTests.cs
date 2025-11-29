using Xunit;
using SafeNest.Models;

namespace SafeNest.Tests
{
    public class LoginTests
    {
        [Fact]
        public void CorrectPin_ShouldLogin()
        {
            var login = new LoginModel();
            login.Pin = "1234";
            bool result = login.ValidatePin();
            Assert.True(result);
        }

        [Fact]
        public void WrongPin_ShouldFail()
        {
            var login = new LoginModel();
            login.Pin = "5555";
            bool result = login.ValidatePin();
            Assert.False(result);
        }
    }
}
