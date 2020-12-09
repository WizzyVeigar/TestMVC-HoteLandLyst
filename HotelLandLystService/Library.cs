using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HotelLandLystService
{
    public static class Library
    {
        /// <summary>
        /// Writes an exception to the error log
        /// </summary>
        /// <param name="e"></param>
        public static void WriteErrorLog(Exception e)
        {
            
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + e.Source.ToString().Trim() + "; " + e.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Writes a custom error log message
        /// </summary>
        /// <param name="message"></param>
        public static void WriteErrorLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Calls the <paramref name="executer"/> to run a method, checking for Old reservations
        /// </summary>
        /// <param name="executer"></param>
        public static void CheckForOldReservations(SqlExecuter executer)
        {
            if (executer == null)
            {
                WriteErrorLog("Execute object was null in library.cs " + DateTime.Now);
            }
            else
            {
                executer.RemoveOldReservations();
            }
        }
    }
}
