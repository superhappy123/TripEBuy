using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TripEBuy.Common;

namespace TripEBuy.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        #region Properties
        /// <summary>
        /// 验证消息结果
        /// </summary>
        public string ValidaTionMessage { get; set; }
        public RequestMode reqMod { get; set; }
        public Stopwatch Stopwatch { get; set; } 

        #endregion Properties

        #region Methods
        public BaseController()
        {
            Stopwatch = new Stopwatch();
        }

        protected bool ModelVerification()
        {
            if (ModelState.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string ValidateFailMessage()
        {
            string VerificaError = null;
            var ErrorCollection = this.ActionContext.ModelState;
            List<string> keylist = ErrorCollection.Keys.ToList();
            List<System.Web.Http.ModelBinding.ModelState> keyvalues = ErrorCollection.Values.ToList();
            var Valueslist = keyvalues.ToList();
            for (int i = 0; i < Valueslist.Count; i++)
            {
                for (int j = 0; j < Valueslist[i].Errors.Count; j++)
                {
                    string TempString = null;
                    TempString = ConvertHelper.ToString2(Valueslist[i].Errors[j].ErrorMessage);
                    if (TempString != "")
                    {
                        VerificaError = VerificaError + Valueslist[i].Errors[j].ErrorMessage.ToString() + ";";
                    }
                }
            }
            if (VerificaError == null)
            {
                VerificaError = TripEBuy.Common.StatusCode.SYSTEM_EXCEPTION.Description;
            }
            return VerificaError;
        }

        /// <summary></summary>
        /// <param name="context"></param>
        /// <param name="resultEntity"></param>
        /// <returns></returns>
        protected bool Vertify(HttpControllerContext context, BaseResponseModel resultEntity)
        {
            if (ConfigHelper.GetInstance().SignVerificationInd == "N")
            {
                return true;
            }

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            if (context.Request.Method == HttpMethod.Get)
            {
                List<KeyValuePair<string, string>> list = context.Request.GetQueryNameValuePairs().ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    dicParams.Add(list[i].Key, list[i].Value);
                }
            }
            else if (context.Request.Method == HttpMethod.Post)
            {
                HttpContextBase contextbase = (HttpContextBase)context.Request.Properties["MS_HttpContext"];
                if (contextbase.Request.Form != null)
                {
                    foreach (string key in contextbase.Request.Form.AllKeys)
                    {
                        dicParams.Add(key, contextbase.Request.Form[key]);
                    }
                }
            }

            return Vertify(dicParams, resultEntity);
        }

        /// <summary></summary>
        /// <param name="JsonData"></param>
        /// <param name="resultEntity"></param>
        /// <returns></returns>
        protected bool ValidateRequest(string JsonData, BaseResponseModel resultEntity)
        {
            if (ConfigHelper.GetInstance().SignVerificationInd == "N")
            {
                return true;
            }
            Dictionary<string, object> dicParams = new Dictionary<string, object>();
            dicParams = ConvertHelper.ToDictionary(JsonData);

            return Vertify(dicParams, resultEntity);
        }

        /// <summary></summary>
        /// <param name="JsonData"></param>
        /// <param name="resultEntity"></param>
        /// <returns></returns>
        protected bool Vertify(string JsonData, BaseResponseModel resultEntity)
        {
            if (ValidateRequest(JsonData, resultEntity) == true) //签名验证
            {
                if (ModelVerification() == true)
                {
                    return true;
                }
                else
                {
                    resultEntity._StatusCode = TripEBuy.Common.StatusCode.SYSTEM_EXCEPTION;
                    resultEntity._StatusCode.Description = ValidateFailMessage();
                    return false;

                }
            }
            else
            {
                resultEntity._StatusCode = TripEBuy.Common.StatusCode.SIGN_EXCEPTION;
                return false;
            }
        }



        /// <summary></summary>
        /// <param name="dicParams"></param>
        /// <param name="resultEntity"></param>
        /// <returns></returns>
        protected bool Vertify(Dictionary<string, object> dicParams, BaseResponseModel resultEntity)
        {
            bool result = true;

            if (dicParams == null || dicParams.Count == 0)
            {
                result = false;
                resultEntity._StatusCode = TripEBuy.Common.StatusCode.SYSTEM_EXCEPTION;
            }
            else
            {
                SortedDictionary<string, string> sortedDicParams = new SortedDictionary<string, string>();
                string sign = string.Empty;
                foreach (string key in dicParams.Keys)
                {
                    if (key != "sign")
                    {
                        if ((dicParams[key].ToString2() != string.Empty)&&(dicParams[key].ToString2() !="0001-01-01T00:00:00"))
                        {
                            sortedDicParams.Add(key, dicParams[key].ToString2());
                        }
                    }
                    else
                    {
                        sign = dicParams[key].ToString2();
                    }
                }

                string linkString = string.Empty;
                string localSign = TripEBuy.Common.Sign.GetSign(sortedDicParams, ref linkString);
                if (localSign != sign.ToUpper())
                {
                    //验签失败
                    result = false;
                    //resultEntity.ReturnCode = "SIGN ERROR";
                    //resultEntity.ReturnMsg = "签名错误！";

                    //resultEntity._StatusCode = TopOne.Web.APIs.EnterpriseAdmin.Common.StatusCode.SIGN_EXCEPTION;
                }
            }

            return result;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name=""></param>
        /// <param name="dicParams"></param>
        /// <param name="resultEntity"></param>
        /// <returns></returns>
        protected bool Vertify(Dictionary<string, string> dicParams, BaseResponseModel resultEntity)
        {
            bool result = true;

            if (dicParams == null || dicParams.Count == 0)
            {
                result = false;
                resultEntity._StatusCode = TripEBuy.Common.StatusCode.SYSTEM_EXCEPTION;
            }
            else
            {
                SortedDictionary<string, string> sortedDicParams = new SortedDictionary<string, string>();
                string sign = string.Empty;
                foreach (string key in dicParams.Keys)
                {
                    if (key != "sign")
                    {
                        sortedDicParams.Add(key, dicParams[key].ToString2());
                    }
                    else
                    {
                        sign = dicParams[key].ToString2();
                    }
                }

                string linkString = string.Empty;
                string localSign = TripEBuy.Common.Sign.GetSign(sortedDicParams, ref linkString);
                if (localSign != sign.ToUpper().Trim())
                {
                    //验签失败
                    result = false;
                    //resultEntity.ReturnCode = "SIGN ERROR";
                    //resultEntity.ReturnMsg = "签名错误！";

                    resultEntity._StatusCode = TripEBuy.Common.StatusCode.SYSTEM_EXCEPTION;
                }
            }

            return result;
        }

        #endregion Methods

        /// <summary> 查看Model层的参数验证是否通过</summary>
        /// <returns></returns>
        /// <summary> 验证失败消息列表 </summary>
        /// <returns></returns>
    }
}