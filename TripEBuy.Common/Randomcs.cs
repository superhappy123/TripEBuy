using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Common
{
  public   class Randomcs
    {
      public  string CreateRandomCode(int codeCount)
      {
          string allChar = "0,1,2,3,4,5,6,7,8,9";
          string[] allCharArray = allChar.Split(',');
          string randomCode = "";
          int temp = -1;
          Random rand = new Random();
          for (int i = 0; i < codeCount; i++)
          {
              if (temp != -1)
              {
                  rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
              }
              int t = rand.Next(9);
              if (temp == t)
              {
                  return CreateRandomCode(codeCount);
              }
              temp = t;
              randomCode += allCharArray[t];
          }
          return randomCode;
      }
    }
}
