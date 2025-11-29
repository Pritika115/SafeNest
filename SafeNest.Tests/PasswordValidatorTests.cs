using Xunit;
using SafeNest.Models;

namespace SafeNest.Tests
{
    public class PasswordValidatorTests
    {
        [Fact]
        public void ValidPassword_ShouldReturnTrue()
        {
            string password = "ABCdef0123";
            bool result = PasswordValidator.IsValid(password);
            Assert.True(result);
        }

        [Fact]
        public void InvalidPassword_NoUpperCase_ShouldFail()
        {
            string password = "abc012345";
            bool result = PasswordValidator.IsValid(password);
            Assert.False(result);
        }
    }
}
