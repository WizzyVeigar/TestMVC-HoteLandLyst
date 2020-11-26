using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.ApiController
{
    public class MsSqlManager
    {

        //Lav sql component factory til oprettelse af objekter
        const string connectionstring = @"Server = (localdb)\MSSQLLocalDB; Database=HotelLandLyst;User Id = sa; Password=Qwert12345!";

        private SqlConnection conn;
        SqlCommand command;
        SqlDataReader dataReader;
        DataTable dt;

        private static MsSqlManager instance;
        public static MsSqlManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MsSqlManager();
                }
                return instance;
            }
        }

        private MsSqlManager()
        {
            conn = new SqlConnection(connectionstring);
        }



        //Rewrite method to return a dataset, make it a customer in logic
        public Customer GetCustomer(string phoneNumber)
        {
            //Make in factory in logic layer
            Customer cust = new Customer();

            using (command = new SqlCommand("USE HotelLandLyst Select * FROM Customer WHERE customerPhone=@phone", conn))
            {
                command.Parameters.AddWithValue("@phone", phoneNumber);
                conn.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    cust.PhoneNumber = dataReader["customerPhone"].ToString();
                    cust.FName = dataReader["customerFName"].ToString();
                    cust.LName = dataReader["customerLName"].ToString();
                    cust.Address = dataReader["customerAddress"].ToString();
                    cust.CityAreaCode = Convert.ToInt32(dataReader["CityAreaCode"]);
                    cust.Email = dataReader["customerEmail"].ToString();
                }
                return cust;
            }
        }

        public DataTable GetAllRooms()
        {
            using (command = new SqlCommand("USE HotelLandLyst Select * FROM Room", conn))
            {
                dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                conn.Open();
                adapter.SelectCommand = command;
                dataReader = command.ExecuteReader();
                dataReader.Close();
                adapter.Fill(dt);
                conn.Close();
                adapter.Dispose();
                return dt;
            }
        }

        public DataTable GetRoomAccessories()
        {
            using (
                command = new SqlCommand("Exec GetRoomAccessories", conn))
            {
                dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                conn.Open();
                adapter.SelectCommand = command;
                dataReader = command.ExecuteReader();
                dataReader.Close();
                adapter.Fill(dt);
                conn.Close();
                adapter.Dispose();
                return dt;
            }
        }

    }
}
