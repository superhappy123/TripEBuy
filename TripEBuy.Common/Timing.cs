using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TripEBuy.Common
{
    public class Timing
    {

        private Stopwatch sw;
        public int used_time { get; set; }
        public Timing()
        {
            sw = new System.Diagnostics.Stopwatch();
        }
        public void Stop()    //停止计时
        {
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            used_time = ts.Milliseconds;
        }
        public void Start()   //开始计时
        {
            sw.Start();
        }

    }
}