using POD.Entities;
using POD.Interfaces;

namespace POD.Data.Services
{
    public class AccountDataService : IAccountDataService
    {
        public User Login(string userName, string password)
        {
            //User user = dbConnection.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            //return user;

            User user = new User() {  UserID = 1, UserName = "Metasqya"  };

            return user;
        }
    }
}
