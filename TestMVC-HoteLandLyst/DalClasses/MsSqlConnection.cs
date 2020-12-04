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


                        try
                        {
                            conn.Open();
                            command.ExecuteNonQuery();

                            command.Parameters.Clear();
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
        /// <summary>
        /// Method for creating a customer in the database
        /// </summary>
        /// <param name="customer">The customer to be created</param>
        internal void CreateCustomer(Customer customer)
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

                    command.CommandText = "MakeCustomer";
                    command.Parameters.AddWithValue("@customerFName", customer.FName);
                    command.Parameters.AddWithValue("@customerLName", customer.LName);
                    command.Parameters.AddWithValue("@customerAddress", customer.Address);
                    command.Parameters.AddWithValue("@customerPhone", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@customerEmail", customer.Email);
                    command.Parameters.AddWithValue("@customerAreaCode", customer.CityAreaCode);


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
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                //Outer exception
                throw e;
            }
        }

        public bool FindCustomer(string phoneNumber)
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


                    SqlDataReader dataReader;

                    using (command = new SqlCommand("Exec GetCustomer @customerPhone", conn))
                    {
                        command.Parameters.AddWithValue("@customerPhone", phoneNumber);
                        try
                        {
                            conn.Open();
                            dataReader = command.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (SqlException se)
                        {
                            //Connection failed to open
                            //Inner exception 1
                            throw se;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
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
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        adapter.Dispose();
                        conn.Close();
                    }
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
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
