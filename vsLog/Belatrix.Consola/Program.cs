using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Belatrix.Helper;
namespace Belatrix.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tiposMensajes = { "ERROR", "WARNING", "MESSAGE" };
            string[] tiposLogs = { "CONSOLE","FILE","BD" };

            JobLogger.LogMessage("probando", tiposLogs, tiposMensajes);
            Console.ReadLine();
        }
    }
}
