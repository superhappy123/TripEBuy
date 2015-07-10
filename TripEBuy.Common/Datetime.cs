using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Common
{
  public  class Datetimehelp
    {
      private DateTime datetime;
      public Datetimehelp(string time)
      {

          datetime = Convert.ToDateTime(time);
      }
      public int TotalDay()
      {
          int totalday = 0;
          
          totalday = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(datetime.Year, datetime.Month);
          return totalday;
      }
      public DateTime Mintime()
      {
          string time = datetime.ToString("yyyy-MM-dd");
          DateTime dt = DateTime.Parse(time + " 00:00:00");
          return dt;

      }

      public DateTime Maxtime()
      {
          string time = datetime.ToString("yyyy-MM-dd");
          DateTime dt = DateTime.Parse(time + " 23:59:59");
          dt = dt.AddDays(TotalDay() - 1);
          return dt;

      }


      
    }
}
