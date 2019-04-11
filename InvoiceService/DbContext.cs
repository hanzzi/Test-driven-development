using System;

namespace InvoiceService
{
    public class DbContext
    {
        private UserDatabase Database;

        public DbContext()
        {
            Database = new UserDatabase();
        }

        public User GetUser(Guid id)
        {
            foreach (User u in Database.Users)
            {
                if (u.ID == id)
                    return u;
            }
            return null;
        }

        public User[] GetAllUsers()
        {
            return Database.Users;
        }
    }
}
