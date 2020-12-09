using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HotelLandLystService
{
    public class SqlExecuter
    {
        /// <summary>
        /// runs a stored procedure to remove old reservations from the Db
        /// </summary>
        public void RemoveOldReservations()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            try
            {
                SqlConnection conn;
                using (conn = new SqlConnection(connectionstring))
                {
                    SqlCommand command = new SqlCommand("CheckForOldReservations", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
