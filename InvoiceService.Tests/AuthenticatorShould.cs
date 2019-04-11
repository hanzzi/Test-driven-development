using System;
using Xunit;

namespace InvoiceService.Tests
{
    public class InvoiceService_AuthenticatorShould
    {
        public InvoiceService_AuthenticatorShould()
        {
            sut = new Authenticator(new DbContext());
        }

        private Authenticator CreateDefaultAuthenticator()
        {
            return new Authenticator(new DbContext());
        }

        private Authenticator sut;

        [Theory]
        [InlineData("MegetGodtPa$$W0rd")]
        [InlineData("AndetG0dtPassw¤rd")]
        public void ReturnTrueIfPasswordMeetsRequirements_ON_ValidatePasswordRequirements(string value)
        {
            bool result = sut.ValidatePasswordRequirements(value);

            Assert.False(result == false);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("")]
        [InlineData("short")]
        [InlineData("LongerWithCapitals")]
        [InlineData("pa$$w0rd")]
        public void ReturnTrueIfPasswordDoesNotMeetRequirements_ON_ValidatePasswordRequirements(string value)
        {
            bool result = sut.ValidatePasswordRequirements(value);

            Assert.False(result == true);
        }

        [Theory]
        [InlineData("email@domain.com")]
        [InlineData("firstname.lastname@domain.com")]
        [InlineData("email@subdomain.domain.com")]
        [InlineData("firstname+lastname@domain.com	")]
        [InlineData("1234567890@domain.com")]
        [InlineData("email@domain-one.com")]
        [InlineData("_______@domain.com")]
        [InlineData("email@domain.name")]
        [InlineData("email@domain.co.jp")]
        [InlineData("firstname-lastname@domain.com")]
        public void ReturnTrueIfEmailMeetsRequirements_ON_ValidateEmailRequirements(string value)
        {
            bool result = sut.ValidateEmailRequirements(value);

            Assert.False(result == false);
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("#@%^%#$@#$@#.com")]
        [InlineData("@domain.com")]
        [InlineData("email.domain.com")]
        [InlineData("email.@domain.com")]
        [InlineData("あいうえお@domain.com")]
        [InlineData("email@domain")]
        [InlineData("email@-domain.com")]
        [InlineData("email@domain..com")]
        public void ReturnTrueIfEmailDoesNotMeetRequirements_ON_ValidateEmailRequirements(string value)
        {
            bool result = sut.ValidateEmailRequirements(value);

            Assert.False(result == true, $"Value: {value} failed to compute");
        }

        [Theory]
        [ClassData(typeof(NonExistingEmailDataGenerator))]
        public void ReturnTrueIfEmailDoesNotExist_ON_ValidateExistingEmail(string email)
        {
            bool result = sut.ValidateExistingEmail(email);

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(ExistingEmailDataGenerator))]        
        public void ReturnTrueIfEmailExists_ON_ValidateExistingEmail(string email)
        {
            bool result = sut.ValidateExistingEmail(email);

            Assert.False(result);
        }

        [Theory]
        [ClassData(typeof(ValidLoginData))]
        public void ReturnTrueIfUserIsReturnedWithCorrectCredentials_OnAuthenticate(string email, string password)
        {
            User user = sut.Authenticate(email, password);

            Assert.IsType(typeof(User), user);

            // Check if user exists
            Assert.NotNull(user);
            Assert.True(user.ID != null || user.ID != Guid.Empty);
            Assert.True(user.Email != null || user.Email != string.Empty);
        }

        [Theory]
        [ClassData(typeof(InvalidLoginData))]
        public void ReturnTrueIfUserIsReturnedWithIncorrectCredentials_OnAuthenticate(string email, string password)
        {
            User user = sut.Authenticate(email, password);

            Assert.Null(user);
        }

        [Theory]
        [ClassData(typeof(PasswordWithSalts))]
        public void CheckIfStringIsHashed_OnComputeHash(string password, byte[] salt)
        {
            string hash = sut.ComputeHash(password, salt);

            int HASHLOWERBOUND = 736;
            int HASHUPPERBOUND = 768;

            // Check if hash is in possible range.
            Assert.InRange(hash.Length * 8, HASHLOWERBOUND, HASHUPPERBOUND);
        }

        public void CheckForArgumentException_OnComputeHash(string password, byte[] salt)
        {
            Action act = () => sut.ComputeHash(password, salt);

            Assert.Throws<ArgumentException>(act);
        }

        [ClassData(typeof(AuthenticateHash_CorrectValues))]
        public void CheckCorrectHashValidation_OnAuthenticateHash(string plaintext, string hash)
        {
            bool act = sut.AuthenticateHash(plaintext, hash);

            Assert.True(act);
        }

        [Theory]
        [ClassData(typeof(AuthenticateHash_IncorrectValues))]
        public void CheckInvalidHashValidation_OnAuthenticateHash(string plaintext, string hash)
        {
            bool act = sut.AuthenticateHash(plaintext, hash);

            Assert.False(act);
        }
    }
}
