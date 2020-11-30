using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Managers
{
    public class MsSqlConnection
    {

        private static MsSqlConnection instance;
        public static MsSqlConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MsSqlConnection();
                }
                return instance;
            }
        }

        /// <summary>
        /// Method for inputting <paramref name="reservationModel"/> into the database
        /// </summary>
        /// <param name="reservationModel">The reservations to put into the DB</param>
        internal void MakeReservation(FullReservationModel reservationModel)
        {
            throw new NotImplementedException();
        }

        //This is way too much like the other method, how fix?
        public DataRow GetCustomer(string phoneNumber)
        {
            try
            {

                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    SqlCommand command = GetSqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    DataTable dt = GetDataTable();
                    SqlDataAdapter adapter = GetAdapter();
                    SqlDataReader dataReader;

                    using (command = new SqlCommand("Exec GetCustomer @customerPhone", conn))
                    {
                        command.Parameters.AddWithValue("@customerPhone", phoneNumber);
                        try
                        {
                            conn.Open();
                        }
                        catch (Exception)
                        {
                            //Connection failed to open
                            //Inner exception 1
                            throw;
                        }
                        try
                        {
                            dataReader = command.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            //Inner exception 2
                            // Query tried to execute command.CommandText;
                            //dataReader.Close();
                            throw;
                        }
                        try
                        {
                            dataReader.Close();
                            adapter.Fill(dt);
                            adapter.Dispose();
                            return dt.Rows[0];
                        }
                        catch (Exception)
                        {
                            //Inner exception 3
                            //error filling the datatable
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Outer exception
                throw;
            }
        }

        /// <summary>
        /// Executes a specified stores procedure
        /// </summary>
        /// <param name="query">What query to run</param>
        /// <returns>Returns a datatable of the query result</returns>
        public DataTable ExecuteSP(string query)
        {
            SqlConnection conn;
            using (conn = GetSqlConnection())
            {
                SqlCommand command = GetSqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query;
                command.Connection = conn;
                DataTable dt = GetDataTable();
                SqlDataAdapter adapter = GetAdapter();
                SqlDataReader dataReader;

                conn.ConnectionString = GetConnectionString();
                conn.Open();
                adapter.SelectCommand = command;
                dataReader = command.ExecuteReader();
                dataReader.Close();
                adapter.Fill(dt);
                adapter.Dispose();
                return dt;
            }
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection();
        }
        private SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }
        private DataTable GetDataTable()
        {
            return new DataTable();
        }

        private string GetConnectionString()
        {
            return @"Server = (localdb)\MSSQLLocalDB; Database=HotelLandLyst;User Id = sa; Password=Qwert12345!";
        }

        private SqlDataAdapter GetAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
