using System.Text.RegularExpressions;

namespace Northwind.Common
{
    public static class StringValidation
    {
        public static bool IsValidEmailAdress(this string email)
        {
            Regex regex = new Regex(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }
}
