using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.DalClasses
{
    public class SqlServerAccess : ISqlServerAccess
    {
        private string connectionString;
        public string Connectionstring
        {
            get
            {
                return connectionString;
            }
            private set
            {
                connectionString = value;
            }
        }

        public SqlServerAccess(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Home");
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

                    conn.ConnectionString = Connectionstring;
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
        /// <summary>
        /// Method for running a stored procedure with referencing @Id as parameter
        /// </summary>
        /// <param name="query">The SP you wish to run</param>
        /// <param name="paramId">value of @Id in the where condition</param>
        /// <returns></returns>
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

                    conn.ConnectionString = Connectionstring;
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
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection();
        }
        public SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }
        public DataTable GetDataTable()
        {
            return new DataTable();
        }

        public SqlDataAdapter GetAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
