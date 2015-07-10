using System;
using System.Configuration;
using System.Runtime.Caching;

namespace TripEBuy.Common
{
    public abstract class CachingProviderBase
    {
        #region Fields

        protected MemoryCache cache = new MemoryCache("CachingProvider");

        private static readonly object padlock = new object();

        #endregion Fields

        #region Constructors

        public CachingProviderBase()
        {
            DeleteLog();
        }

        #endregion Constructors

        #region Methods

        protected virtual void AddItem(string key, object value)
        {
            string SpanTime = ConfigurationManager.AppSettings["SpanTime"];
            if (SpanTime != null)
            {
                lock (padlock)
                {
                    cache.Add(key, value, new DateTimeOffset(DateTime.UtcNow + TimeSpan.Parse(SpanTime)));
                }
            }
            else
            {
                lock (padlock)
                {
                    cache.Add(key, value, DateTimeOffset.MaxValue);
                }
            }
        }

        protected virtual object GetItem(string key, bool remove)
        {
            lock (padlock)
            {
                var res = cache[key];

                if (res != null)
                {
                    if (remove == true)
                        cache.Remove(key);
                }
                else
                {
                    WriteToLog("CachingProvider-GetItem: Don't contains key: " + key);
                }

                return res;
            }
        }

        protected virtual void RemoveItem(string key)
        {
            lock (padlock)
            {
                cache.Remove(key);
            }
        }

        #endregion Methods

        #region Error Logs

        private string LogPath = System.Environment.GetEnvironmentVariable("TEMP");

        protected void DeleteLog()
        {
            System.IO.File.Delete(string.Format("{0}\\CachingProvider_Errors.txt", LogPath));
        }

        protected void WriteToLog(string text)
        {
            using (System.IO.TextWriter tw = System.IO.File.AppendText(string.Format("{0}\\CachingProvider_Errors.txt", LogPath)))
            {
                tw.WriteLine(text);
                tw.Close();
            }
        }

        #endregion Error Logs
    }
}