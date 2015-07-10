using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Common
{
    public class RandomCode
    {
        public string ProduceRndCode(int length)
        {

            return GetRandomint(length);
        }

        private String GetRandomint(int codeCount)
        {
            Random random = new Random();
            string min = "";
            string max = "";
            for (int i = 0; i < codeCount; i++)
            {
                min += "1";
                max += "9";
            }
            return (random.Next(Convert.ToInt32(min), Convert.ToInt32(max)).ToString());
        }

    }
       
   
}
