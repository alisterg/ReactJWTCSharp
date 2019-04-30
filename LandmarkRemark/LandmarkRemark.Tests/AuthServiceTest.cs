using System;
using LandmarkRemarks.Services;
using Xunit;

namespace LandmarkRemark.Tests
{
    /// <summary>
    /// Tests for LandmarkRemarks.Services.AuthService
    /// </summary>
    public class AuthServiceTest
    {
        private AuthService _auth;
        
        public AuthServiceTest()
        {
            _auth = new AuthService("testsecret");
        }
        
        [Fact]
        public void TestGenerateUserToken()
        {
            Assert.NotNull(_auth.GenerateHash("testpassword"));
        }

        [Fact]
        public void TestCompareHash()
        {
            string hashed = _auth.GenerateHash("testpassword");
            bool same = _auth.CompareHash("testpassword", hashed);
            
            Assert.True(same);
        }

        [Fact]
        public void TestUserToken()
        {
            var generated = _auth.GenerateUserToken("testuser");

            var username = _auth.UserFromToken(generated);
            
            Assert.Equal("testuser", username);
        }
    }
}
