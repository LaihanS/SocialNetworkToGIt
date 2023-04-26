using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Helpers
{
    public class PasswordEncrypter
    {
        public static string PassHasher(string password)
        {
            using (SHA256 newsHA256 = SHA256.Create())
            {
                byte[] bytes = newsHA256.ComputeHash(Encoding.UTF8.GetBytes(password));


                StringBuilder stringBuilder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }

        }
    }
}
