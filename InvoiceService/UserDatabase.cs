using System;

namespace InvoiceService
{
    public class UserDatabase
    {
        public UserDatabase()
        {

            // Passwords are "admin"
            Users = new User[] {
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
                    Password = "jIF9WqjjwhRYsa/15npA3aKHgEtcyqiNOhISy+reusY0xgoQFZnTgUy/0keqgB5yGLseQIi9Wk1VEYF7uZVDr78qf6o=",
                    Authlevel = 50
                },
                new User()
                {
                    ID = new Guid("9853C204-6845-4372-914B-EF3A1327E534"),
                    Email = "domain@email.com",
                    Firstname = "Jens",
                    Lastname = "Andersen",
                    Password = "nkMGD0OvYY7GQD9cfLTOGH3BPkcndlPZ5ZLM31SE1JAD2eAoQux+FnVAcJ/eGueWKXEuOm4dIKsnibf9hTeY+ag6mZ0U9A==",
                    Authlevel = 30
                }
            };
        }

        public User[] Users;
    }
}
