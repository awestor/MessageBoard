using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Generators
{
    public class PasswordGenerator
    {
        public static string generatePassword(int size)
        {
            var login = "";
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+|/?.,=-";

            for (int i = 0; i < size; i++)
            {
                // Выбор рандомного символа из допустимого
                int x = res.Next(str.Length);
                login += str[x];
            }
            return login;
        }
    }
}
