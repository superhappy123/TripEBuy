using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;




namespace TripEBuy.Common
{

    // 摘要: 
    //     枚举 HTTP 谓词。
    [Flags]
    public enum HttpVerbs
    {
        // 摘要: 
        //     检索由请求的 URI 标识的信息或实体。
        Get = 1,
        //
        // 摘要: 
        //     发布新实体作为对 URI 的补充。
        Post = 2,
        //
        // 摘要: 
        //     替换由 URI 标识的实体。
        Put = 4,
        //
        // 摘要: 
        //     请求删除指定的 URI。
        Delete = 8,
        //
        // 摘要: 
        //     检索由请求的 URI 标识的信息或实体的消息头。
        Head = 16,
        //
        // 摘要: 
        //     请求将请求实体中描述的一组更改应用于请求 URI 所标识的资源。
        Patch = 32,
        //
        // 摘要: 
        //     表示由请求 URI 标识的请求/响应链上提供的通信选项的相关信息请求。
        Options = 64,
    }
    public class myHttpClientHandler : HttpClientHandler
    {

        private static Mutex _mu = new Mutex();
        private static myHttpClientHandler _myHttpClientHandler;


        private HttpClient myHttpClient;
        private string BaselURl;
        public static myHttpClientHandler GetInstance()
        {
            _mu.WaitOne();
            try
            {
                if (_myHttpClientHandler == null) _myHttpClientHandler = new myHttpClientHandler();
            }
            finally
            {
                _mu.ReleaseMutex();
            }
            return _myHttpClientHandler;
        }


        public myHttpClientHandler()
        {
            this.InitializeHttpClient();
        }

        private void InitializeHttpClient()
        {
            this.BaselURl = ConfigurationManager.AppSettings["BaseURL"].ToString();
            this.myHttpClient = new HttpClient();
            this.myHttpClient.BaseAddress = new Uri(this.BaselURl);
            this.myHttpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //this.myHttpClient.Timeout = System.TimeSpan.FromSeconds(3000);要根据实际调整
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var contentType = response.Content.Headers.ContentType;
            contentType.CharSet = await getCharSetAsync(response.Content);

            return response;
        }

        private async Task<string> getCharSetAsync(HttpContent httpContent)
        {
            var charset = httpContent.Headers.ContentType.CharSet;
            if (!string.IsNullOrEmpty(charset))
                return charset;

            var content = await httpContent.ReadAsStringAsync();
            var match = Regex.Match(content, @"charset=(?<charset>.+?)""", RegexOptions.IgnoreCase);
            if (!match.Success)
                return charset;

            return match.Groups["charset"].Value;
        }



        public Task<T> GetHttpResponseContent<T>(string requestUri)
        {
            Logger.GetInstance().WriteLog("开始发起Web请求.Web请求的URL:" + (this.BaselURl + requestUri));
            HttpResponseMessage response = null;

            try
            {
           
                response = this.GetAsync(requestUri).Result;
                this.setContentCharSet(ref response);

                if (response.IsSuccessStatusCode)
                {
                    var list = response.Content.ReadAsStringAsync().Result;
                    string JsonString = JsonConvert.SerializeObject(list);
                    Logger.GetInstance().WriteLog("Web请求的信息:" + response.RequestMessage);
                    Logger.GetInstance().WriteLog("Web返回的信息:" + JsonString);
                    return Task.FromResult<T>((T)((object)list));

                }
                else
                {

                    Logger.GetInstance().WriteLog("发起Web请求出错.Web请求的信息:" + response.RequestMessage);
                    Logger.GetInstance().WriteLog("发起Web请求出错.Web返回的信息:" + response.Headers.ToString());
                    Logger.GetInstance().WriteLog("Error Code:" + response.StatusCode + " ; Message:" + response.ReasonPhrase);
                    var list = response.Content.ReadAsStringAsync().Result;
                    return Task.FromResult<T>((T)((object)list));


                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().WriteLog("发起Web请求出错.Web请求的URL:" + (this.BaselURl + requestUri));
                Logger.GetInstance().WriteLog("错误信息:" + ex.Message);
                return null;

            }

            finally
            {
                Logger.GetInstance().WriteLog("结束发起Web请求.Web请求的URL:" + (this.BaselURl + requestUri));

            }



        }
        public Task<T> GetHttpResponseContent<T>(string requestUri, HttpVerbs httpVerb, object item)
        {
            //requestUri = this.BaselURl + requestUri;
            Logger.GetInstance().WriteLog("开始发起Web请求.Web请求的URL:" + (this.BaselURl + requestUri));
            HttpResponseMessage response = null;

            try
            {

                switch (httpVerb)
                {
                    case HttpVerbs.Get:
                        response = this.GetAsync(requestUri).Result;
                        break;
                    case HttpVerbs.Put:
                        response = this.PutAsJsonAsync(requestUri, item).Result;
                        break;
                    case HttpVerbs.Delete:
                        response = this.PutAsJsonAsync(requestUri, item).Result;
                        break;
                    case HttpVerbs.Post:
                        response = this.PostAsJsonAsync(requestUri, item).Result;
                        break;
                }
                this.setContentCharSet(ref response);

                if (response.IsSuccessStatusCode)
                {

                   
                    var list = response.Content.ReadAsAsync<T>().Result;
                    string JsonString = JsonConvert.SerializeObject(list);
                    Logger.GetInstance().WriteLog("Web请求的信息:" + response.RequestMessage);
                    Logger.GetInstance().WriteLog("Web返回的信息:" + JsonString);
                    return Task.FromResult<T>((T)((object)list));
                   
                }

                {
                  
                    Logger.GetInstance().WriteLog("发起Web请求出错.Web请求的信息:" + response.RequestMessage);
                    Logger.GetInstance().WriteLog("发起Web请求出错.Web返回的信息:" + response.Headers.ToString());
                    Logger.GetInstance().WriteLog("Error Code:" + response.StatusCode + " ; Message:" + response.ReasonPhrase);
                    var list = response.Content.ReadAsAsync(typeof(T)).Result;
                    return Task.FromResult<T>((T)((object)list));


                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().WriteLog("发起Web请求出错.Web请求的URL:" + (this.BaselURl + requestUri));
                Logger.GetInstance().WriteLog("错误信息:" + ex.Message);
                return null;

            }

            finally
            {
                Logger.GetInstance().WriteLog("结束发起Web请求.Web请求的URL:" + (this.BaselURl + requestUri));

            }



        }

        private void setContentCharSet(ref HttpResponseMessage response)
        {
            var contentType = response.Content.Headers.ContentType;
            if (contentType == null) { return; }
            contentType.CharSet = getCharSetAsync(response.Content).Result;
        }


        private Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (this.myHttpClient == null)
            {
                this.InitializeHttpClient();
            }
            return this.myHttpClient.GetAsync(requestUri);
        }

        private Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
        {
            if (this.myHttpClient == null)
            {
                this.InitializeHttpClient();
            }
            return this.myHttpClient.PostAsJsonAsync(requestUri, value);
        }

        private Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
        {
            if (this.myHttpClient == null)
            {
                this.InitializeHttpClient();
            }
            return this.myHttpClient.PutAsJsonAsync(requestUri, value);
        }



    }
}