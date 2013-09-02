using Northwind.MusicStore.BusinessInterfaces;

namespace Northwind.Infrastructure.WebMatrixSecurity
{
    public class WebMatrixWebSecurityProvider : IWebSecurityProvider
    {
        public string CreateUserAndAccount(string userName, string password)
        {
            return WebMatrix.WebData.WebSecurity.CreateUserAndAccount(userName, password);
        }

        public bool Login(string userName, string password)
        {
             return WebMatrix.WebData.WebSecurity.Login(userName, password);
        }
    }
}