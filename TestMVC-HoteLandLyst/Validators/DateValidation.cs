using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Interfaces;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Validators
{
    public class DateValidation
    {
        /// <summary>
        /// Checks the EndDate for <paramref name="booking"/> and sets it if needed
        /// </summary>
        /// <param name="booking">The room which you want to check the end date</param>
        public void CheckValidEndDate(BookingModel booking)
        {
            if (booking.EndDate.Hour >= 10 && booking.EndDate.Minute > 0)
            {
                booking.EndDate = new DateTime(booking.EndDate.Year, booking.EndDate.Month, booking.EndDate.Day, 10, 0, booking.EndDate.Second);
            }
        }

        /// <summary>
        /// Get all unavailable dates, callable from JS
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        public DateTime[] GetUnavailableDates(int roomNumber, ISqlServerAccess sqlServer)
        {
            DataTable dt = sqlServer.ExecuteSPParam("GetReservationDates", roomNumber);
            List<DateTime> dates = new List<DateTime>();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DateTime date in CalculateDateGap((DateTime)row[0], (DateTime)row[1]))
                    {
                        dates.Add(date);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return dates.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Returns a Datetime[] of all the dates in between <paramref name="start"/> and <paramref name="end"/></returns>
        private IEnumerable<DateTime> CalculateDateGap(DateTime start, DateTime end)
        {
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                yield return date;
            }
        }
    }
}
