using System;
using Xunit;

namespace InvoiceService.Tests
{
    public class InvoiceService_DbContextShould
    {
        private readonly DbContext sut;

        public InvoiceService_DbContextShould()
        {
            sut = CreateDefaultDbContext();
        }

        private DbContext CreateDefaultDbContext()
        {
            return new DbContext();
        }

        [Theory]
        [InlineData("B7E5D19B-E86F-401A-B85D-49F86FBC7D95")]
        [InlineData("EAE316E6-5871-4704-B9C3-2C82CF5F2225")]
        public void ReturnTrueWhenValidIdIsSupplied(string value)
        {
            Guid uid = new Guid(value);
            User result = sut.GetUser(uid);

            Assert.IsType<User>(result);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("08c0aa91-36d5-43db-a183-6f9ccaa8665f")]
        [InlineData("6dbf036b-52d5-4ba0-80a0-844f4a3258fe")]
        [InlineData("b73184cb-2fec-4d8f-911b-974493832ddd")]
        [InlineData("7419e5e0-bd51-42ee-97cd-a2fafc41a299")]
        [InlineData("d2fb8975-1e37-4ee3-b33e-fae84f200464")]
        public void ReturnTrueWhenInvalidUserIsSupplied(string value)
        {
            Guid uid = new Guid(value);

            User result = sut.GetUser(uid);

            // Should be null when no such user is found
            Assert.Null(result);
        }

        [Fact]
        public void ReturnTrueWhenAllUsersAreReturned_OnGetAllUsers()
        {
            User[] users = sut.GetAllUsers();

            Assert.NotEmpty(users);
        }
    }
}