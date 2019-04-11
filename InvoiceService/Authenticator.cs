using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace InvoiceService
{
    public class Authenticator
    {
        private DbContext _databaseContext;

        public Authenticator(DbContext db)
        {
            _databaseContext = db;
        }

        public bool ValidatePasswordRequirements(string password)
        {
            if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$"))
                return true;
            return false;
        }

        public bool ValidateEmailRequirements(string email)
        {
            if (Regex.IsMatch(email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
                return true;
            return false;
        }

        public bool ValidateExistingEmail(string email)
        {
            string[] emails = _databaseContext.GetAllUsers().ToList().Select(x => x.Email).ToArray();

            foreach (var e in emails)
            {
                if (e == email)
                    return false;
            }            
            return true;
        }

        public User Authenticate(string email, string password)
        {
            
            // check if any users exist
            if (_databaseContext.GetAllUsers().Count() == 0)
                return null;

            List<User> useracc = _databaseContext.GetAllUsers().Where(n => n.Email == email).ToList();

            // list has more than one object we cannot determine which user is correct
            // this should hopefully never happen
            bool userexists = useracc.Count() != 0 || useracc.Count() > 1;



            if (userexists && AuthenticateHash(password, useracc.First().Password))
            {
                return useracc.First();
            }

            return null;
        }

        public bool AuthenticateHash(string plaintext, string hashvalue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashvalue);

            int hashSizeInBits, hashSizeInBytes;

            hashSizeInBits = 512;

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltbytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (int i = 0; i < saltbytes.Length; i++)
                saltbytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString = ComputeHash(plaintext, saltbytes);

            return (hashvalue == expectedHashString);
        }

        public byte[] GenerateSalt()
        {
            throw new NotImplementedException();
        }

        public string ComputeHash(string password, byte[] saltbytes)
        {
            if (saltbytes == null)
            {
                throw new ArgumentException("Missing salt");
            }
            if (password == null)
            {
                throw new ArgumentException("Missing password");
            }

            byte[] plaintextbytes = Encoding.UTF8.GetBytes(password);

            byte[] plaintextWithSaltBytes = new byte[plaintextbytes.Length + saltbytes.Length];

            for (int i = 0; i < saltbytes.Length; i++)
            {
                plaintextWithSaltBytes[plaintextbytes.Length + i] = saltbytes[i];
            }

            HashAlgorithm hash = new SHA512Managed();

            byte[] hashbytes = hash.ComputeHash(plaintextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashbytes.Length + saltbytes.Length];

            for (int i = 0; i < hashbytes.Length; i++)
                hashWithSaltBytes[i] = hashbytes[i];

            for (int i = 0; i < saltbytes.Length; i++)
                hashWithSaltBytes[hashbytes.Length + i] = saltbytes[i];

            string hashvalue = Convert.ToBase64String(hashWithSaltBytes);

            return hashvalue;
        }

    }
}
