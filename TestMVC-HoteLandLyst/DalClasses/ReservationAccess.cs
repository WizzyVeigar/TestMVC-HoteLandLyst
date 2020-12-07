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
    public class ReservationAccess : SqlServerAccess
    {
        public ReservationAccess(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Method for inputting <paramref name="reservationModel"/> into the database
        /// </summary>
        /// <param name="reservationModel">The reservations to put into the DB</param>
        /// 
        [HttpPost]
        public void CreateReservation(FullReservationModel reservationModel)
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
            catch (Exception e)
            {
                throw e;
            }
        }       
    }
}
