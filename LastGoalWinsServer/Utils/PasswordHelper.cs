using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace LastGoalWinsServer.Utils
{
    public class PasswordHelper
    {
        //private readonly IPasswordHasher<object> _passwordHasher;

        //public PasswordHelper(IPasswordHasher<object> passwordHasher)
        //{
        //    _passwordHasher = passwordHasher;
        //}

        //public string GeneratePassword(string password)
        //{
        //    return _passwordHasher.HashPassword(null, password);
        //}

        //public bool VerifyPassword(string hashedPassword, string providedPassword)
        //{

        //    Console.WriteLine($"new pass = {_passwordHasher.HashPassword(null,providedPassword)}\nhashed pass = {hashedPassword}");
        //    return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword)
        //           != PasswordVerificationResult.Failed;
        //}
        public string GeneratePassword(string password)
        {
            return password;
            using (var sha = SHA256.Create())
            {
                var asByteArray = Encoding.Default.GetBytes(password);
                var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));
                Console.WriteLine(hashedPassword);
                return hashedPassword;
            }
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            //Console.WriteLine($"new pass = {GeneratePassword(providedPassword)}\nhashed pass = {hashedPassword}");
            return providedPassword.Equals(hashedPassword);
        }

    }
}
