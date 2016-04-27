using POD.Entities;

namespace POD.Interfaces
{
    public interface IAccountDataService
    {
        User Login(string userName, string password);
    }
}
