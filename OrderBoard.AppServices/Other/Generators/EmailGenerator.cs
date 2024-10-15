using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Generators
{
    public class EmailGenerator
    {
        public static string generateEmail(int size)
        {
            var email = "";
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789^*()_+|/?";

            for (int i = 0; i < size-2; i++)
            {
                // Выбор рандомного символа из допустимого
                int x = res.Next(str.Length);
                email += str[x];
            }
            email += "@2";
            return email;
        }
    }
}
