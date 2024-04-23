using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDB.App.Extensions
{
    internal static class StringExtention
    {
        internal static void SayHi(this string message)
        {
            Console.WriteLine("Hi " + message);
        }
    }
}
