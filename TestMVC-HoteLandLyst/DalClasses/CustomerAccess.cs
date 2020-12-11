using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.DalClasses
{
    public class CustomerAccess : SqlServerAccess, ICustomerAccess
    {
        public CustomerAccess(IConfiguration configuration) : base(configuration)
        {
        }


        /// <summary>
        /// Method for creating a customer in the database
        /// </summary>
        /// <param name="customer">The customer to be created</param>
        public void CreateCustomer(Customer customer)
        {
            try
            {
                SqlConnection conn;
                using (conn = GetSqlConnection())
                {
                    conn.ConnectionString = Connectionstring;
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
                    conn.ConnectionString = Connectionstring;
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
                    conn.ConnectionString = Connectionstring;
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


    }
}
