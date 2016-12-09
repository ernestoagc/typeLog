using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Helper
{
    public class JobLogger
    {
        private string[] _typeLog;
        private string[] _typeMessage;

        public JobLogger(string[] typeLog, string[] typeMessage)
        {
            _typeLog = typeLog;
            _typeMessage = typeMessage;
        }

        private static void generarLogFile(string message, string[] typeMessage)
        {
            string l = "";
            if (System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("ddMMyyyyy") + ".txt"))
                l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("ddMMyyyyy") + ".txt");

            foreach (var tipoMensaje in typeMessage)
            {
                switch (tipoMensaje)
                {
                    case "ERROR": l = l + DateTime.Now.ToShortDateString() + "_ERROR " + message + Environment.NewLine;
                        break;
                    case "WARNING": l = l + DateTime.Now.ToShortDateString() + "_WARNING " + message + Environment.NewLine;
                        break;
                    case "MESSAGE": l = l + DateTime.Now.ToShortDateString() + "_MESSAGE " + message + Environment.NewLine;
                        break;
                }

                System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("ddMMyyyyy") + ".txt", l);

            }


        }
        private static void generarLogBd(string message, string[] typeMessage)
        {
            int t = 0;


            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
            {

                con.Open();
                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())
                {

                    foreach (var tipoMensaje in typeMessage)
                    {
                        switch (tipoMensaje)
                        {
                            case "ERROR": t = 2;
                                break;
                            case "WARNING": t = 3;
                                break;
                            case "MESSAGE": t = 1;
                                break;
                        }
                        command.Connection = con;
                        command.CommandText = "Insert into Log Values('" + message + "', " + t.ToString() + ")";
                        command.ExecuteNonQuery();
                    }

                }
            }
        }
        private static void generarLogConsole(string message, string[] typeMessage)
        {
            foreach (var tipoMensaje in typeMessage)
            {
                switch (tipoMensaje)
                {
                    case "ERROR": Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "WARNING": Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "MESSAGE": Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            }

        }

        public static bool LogMessage(string message, string[] typeLog, string[] typeMessage)
        {
            if (string.IsNullOrEmpty(message))
                return false;


            if (typeLog == null || typeLog.Count() == 0)
                throw new Exception("Invalid configuration");


            if (typeMessage == null || typeMessage.Count() == 0)
                throw new Exception("Invalid configuration");

            try
            {

                foreach (var tipoLog in typeLog)
                {
                    switch (tipoLog)
                    {
                        case "FILE": generarLogFile(message, typeMessage); ;
                            break;
                        case "BD": generarLogBd(message, typeMessage);
                            break;
                        case "CONSOLE": generarLogConsole(message, typeMessage);
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }

    }


}
