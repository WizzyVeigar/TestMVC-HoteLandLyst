using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using Microsoft.Extensions.Configuration;
using TestMVC_HoteLandLyst.Interfaces;
using System.Text.Json;

namespace TestMVC_HoteLandLyst.DalClasses
{
    public class MsSqlConnection : IDataAccess
    {
        private readonly string connectionstring;

        public MsSqlConnection(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("Default");
        }

        /// <summary>
        /// Method for inputting <paramref name="reservationModel"/> into the database
        /// </summary>
        /// <param name="reservationModel">The reservations to put into the DB</param>
        /// 
        [HttpPost]
        internal void MakeReservation(FullReservationModel reservationModel)
        {
            try
            {
                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    conn.ConnectionString = connectionstring;
                    SqlCommand command = GetSqlCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = "MakeReservation";

                    foreach (BookingModel booking in reservationModel.RoomsToBook)
                    {
                        command.Parameters.AddWithValue("@RoomNumber", booking.Room.RoomNumber);
                        command.Parameters.AddWithValue("@customerPhone", reservationModel.Customer.PhoneNumber);
                        command.Parameters.AddWithValue("@StartDate", booking.StartDate);
                        command.Parameters.AddWithValue("@EndDate", booking.EndDate);

                        command.Parameters.Clear();

                        try
                        {
                            conn.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                            //Log exception
                            //throw;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        internal void MakeReservation(string reservationModel)
        {
            FullReservationModel json = JsonSerializer.Deserialize<FullReservationModel>(reservationModel);
            try
            {
                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    conn.ConnectionString = connectionstring;
                    SqlCommand command = GetSqlCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = "MakeReservation";

                    foreach (BookingModel booking in json.RoomsToBook)
                    {
                        command.Parameters.AddWithValue("@RoomNumber", booking.Room.RoomNumber);
                        command.Parameters.AddWithValue("@customerPhone", json.Customer.PhoneNumber);
                        command.Parameters.AddWithValue("@StartDate", booking.StartDate);
                        command.Parameters.AddWithValue("@EndDate", booking.EndDate);

                        command.Parameters.Clear();

                        try
                        {
                            conn.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                            //Log exception
                            //throw;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //This is way too much like the other method, how fix?
        public DataRow GetCustomer(string phoneNumber)
        {
            try
            {

                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    conn.ConnectionString = connectionstring;
                    SqlCommand command = GetSqlCommand();
                    command.Connection = conn;
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
                            dataReader = command.ExecuteReader();
                            dataReader.Close();
                            adapter.Fill(dt);
                            adapter.Dispose();
                            return dt.Rows[0];
                        }
                        catch (SqlException se)
                        {
                            //Connection failed to open
                            //Inner exception 1
                            throw se;
                        }
                        catch (Exception e)
                        {
                            throw e;
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
            try
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

                    conn.ConnectionString = connectionstring;
                    try
                    {
                        conn.Open();
                        adapter.SelectCommand = command;
                        dataReader = command.ExecuteReader();
                        dataReader.Close();
                        adapter.Fill(dt);

                    }
                    catch (SqlException)
                    {

                        throw;
                    }
                    finally
                    {
                        adapter.Dispose();
                        conn.Close();
                    }
                    return dt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable ExecuteSPParam(string query, int paramId)
        {
            try
            {

                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    SqlCommand command = GetSqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = query;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@Id", paramId);
                    DataTable dt = GetDataTable();
                    SqlDataAdapter dataAdapter = GetAdapter();
                    SqlDataReader dataReader;

                    conn.ConnectionString = connectionstring;
                    try
                    {
                        conn.Open();
                        dataAdapter.SelectCommand = command;
                        dataReader = command.ExecuteReader();
                        dataReader.Close();
                        dataAdapter.Fill(dt);
                        return dt;

                    }
                    catch (SqlException e)
                    {

                        throw e;
                    }
                    finally
                    {
                        dataAdapter.Dispose();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
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

        private SqlDataAdapter GetAdapter()
        {
            return new SqlDataAdapter();
        }

    }
}
