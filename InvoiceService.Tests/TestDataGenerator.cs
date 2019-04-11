using System;
using System.Collections;
using System.Collections.Generic;

namespace InvoiceService.Tests
{
    public class UserDataGenerator 
    {
        public static User[] GetValidUsers()
        {
            return new User[] {
                new User()
                {
                    ID = new Guid("B7E5D19B-E86F-401A-B85D-49F86FBC7D95"),
                    Email = "hans-byager@live.dk",
                    Firstname = "Hans",
                    Lastname = "Byager",
                    Password = "YUUMp0aoEd4fY2n/qW8xFWCNfMs/ZNt6BMSNld3bYtbp6NCuNaoHE9F9HPwIlM6SPm0fXYnN/VJO3CZyAZo4AVqZfqsC7Q==",
                    Authlevel = 1000
                },
                new User()
                {
                    ID = new Guid("EAE316E6-5871-4704-B9C3-2C82CF5F2225"),
                    Email = "email@domain.com",
                    Firstname = "Anders",
                    Lastname = "Byager",
                    Password = "+mpdPtQBT+u1Pyh5mLJK1xSHOtqtKxLNDkVQqB1is6pMS0tC81d164j7W9/QTp+KiHP9Lx3Pr6BONSFv7uvqaXYpvR8E",
                    Authlevel = 50
                },
                new User()
                {
                    ID = new Guid("9853C204-6845-4372-914B-EF3A1327E534"),
                    Email = "domain@email.com",
                    Firstname = "Jens",
                    Lastname = "Andersen",
                    Password = "0ntRmo6Tt4DhgsZ1stdeN5P+2apNiIF4Vh0Wpl0jwANzHmIjSuPsO9QHeO50t/cBgFYMTGwMt6OtKqemwYUnDAnfw87hV5M=",
                    Authlevel = 30
                }
            };
        }

        public User[] GetInvalidUsers()
        {
            return new User[] {
                new User()
                {
                    ID = new Guid("B7E5D19B-A7A6-401A-2222-49F86FBC7D95"),
                    Email = "hans-byager@clive.dk",
                    Firstname = "snah",
                    Lastname = "regayb",
                    Password = "BOOMp0aoEd4fY2n/qW8xFWCNfMs/ZNt6BMSNld3bYtbp6NCuNaoHE9F9HPwIlM6SPm0fXYnN/VJO3CZyAZo4AVqZfqsC7Q==",
                    Authlevel = 1000
                },
                new User()
                {
                    ID = new Guid("9853C204-6875-4704-B9C3-2C82CF5F2225"),
                    Email = "Admin@domain.com",
                    Firstname = "Martin",
                    Lastname = "Jensen",
                    Password = "000dPtQBT+u1Pyh5mLJK1xSHOtqtKxLNDkVQqB1is6pMS0tC81d164j7W9/QTp+KiHP9Lx3Pr6BONSFv7uvqaXYpvR8E",
                    Authlevel = 50
                },
                new User()
                {
                    ID = new Guid("EAE316E6-5871-4372-914B-EF3A1327E534"),
                    Email = "User@email.com",
                    Firstname = "Jens",
                    Lastname = "Andersen",
                    Password = "000Rmo6Tt4DhgsZ1stdeN5P+2apNiIF4Vh0Wpl0jwANzHmIjSuPsO9QHeO50t/cBgFYMTGwMt6OtKqemwYUnDAnfw87hV5M=",
                    Authlevel = 30
                }
            };
        }
    }

    public class NonExistingEmailDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new string[] {"212i93@domain.com"},
            new string[] {"nonexisting@domain.com"},
            new string[] {"hello@world.com"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ExistingEmailDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new string[] {"hans-byager@live.dk"},
            new string[] {"email@domain.com"},
            new string[] {"domain@email.com"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ValidLoginData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new string[] {"domain@email.com", "admin"},
            new string[] {"email@domain.com", "admin"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidLoginData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new string[] {"Wrong@email.com", "admin"},
            new string[] {"email@domain.com", "wrong"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class PasswordWithSalts : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"Pa$$w0rd", new byte[] {0x10, 0x40, 0x5F, 0x2D, 0x12}},
            new object[] {"L3tM3!n", new byte[] {0x25, 0xDF, 0x23, 0xFF}},
            new object[] {"5U213&Wop", new byte[] {0x50, 0x70, 0x40, 0xEE, 0x27, 0x24, 0x33}}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ComputeHash_ArgumentTest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"Pa$$w0rd", null},
            new object[] {null, new byte[] {0x25, 0xDF, 0x23, 0xFF}},
            new object[] {null, null}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class AuthenticateHash_CorrectValues : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"admin", "jIF9WqjjwhRYsa/15npA3aKHgEtcyqiNOhISy+reusY0xgoQFZnTgUy/0keqgB5yGLseQIi9Wk1VEYF7uZVDr78qf6o="},
            new object[] {"admin", "nkMGD0OvYY7GQD9cfLTOGH3BPkcndlPZ5ZLM31SE1JAD2eAoQux+FnVAcJ/eGueWKXEuOm4dIKsnibf9hTeY+ag6mZ0U9A=="},
            new object[] {"admin", "YUUMp0aoEd4fY2n/qW8xFWCNfMs/ZNt6BMSNld3bYtbp6NCuNaoHE9F9HPwIlM6SPm0fXYnN/VJO3CZyAZo4AVqZfqsC7Q=="}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class AuthenticateHash_IncorrectValues : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"IncorrectPassword", "jIF9WqjjwhRYsa/15npA3aKHgEtcyqiNOhISy+reusY0xgoQFZnTgUy/0keqgB5yGLseQIi9Wk1VEYF7uZVDr78qf6o="},
            // Incorrect Hash
            new object[] {"admin", "hnMGD0OvYY7GQD9cfLTOGH3BPkcndlPZ5ZLM31SE1JAD2eAoQux+FnVAcJ/eGueWKXEuOm4dIKsnibf9hTeY+ag6mZ0U9A=="},
            new object[] {"NoneIsCorrect", "YAAMp0aoEd4fY2n/qW8xFWCNfMs/ZNt6BMSNld3bYtbp6NCuNaoHE9F9HPwIlM6SPm0fXYnN/VJO3CZyAZo4AVqZfqsC7Q=="}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    
}