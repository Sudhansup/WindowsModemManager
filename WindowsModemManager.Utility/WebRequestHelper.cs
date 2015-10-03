using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace WindowsModemManager.Utility
{
    public class WebRequestHelper
    {
        private static string FormContentType = "application/x-www-form-urlencoded";
        private static string XMLContentType = "text/xml";
        private static string JSONContentType = "application/json";
        private static string POST = "POST";
        private static string GET = "GET";

        private static string FormatPostParameters(NameValueCollection nameValueCollection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in nameValueCollection)
            {
                sb.Append(key + "=" + HttpUtility.UrlEncode(nameValueCollection[key]) + "&");
            }
            sb.Length = sb.Length - 1;
            return sb.ToString();
        }

        private static string FormatPostParameters(Dictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                sb.Append(HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(parameters[key]) + "&");
            }
            sb.Length = sb.Length - 1;
            return sb.ToString();
        }

        public static HttpWebResponse SendFormPostRequest(string url, Dictionary<string, string> parameters)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = FormContentType;
                httpWebRequest.Method = POST;

                string postData = FormatPostParameters(parameters);

                byte[] requestBytes = Encoding.UTF8.GetBytes(postData);

                httpWebRequest.ContentLength = requestBytes.Length;

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }

                Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);
                return (HttpWebResponse)responseTask.Result;
            }
            catch (Exception Ex)
            {
                ErrorAndLogUtility.WriteError(Ex);
                throw;
            }

            
        }

        public static HttpWebResponse SendNonFormPostRequest(string data, string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = XMLContentType;
            httpWebRequest.Method = POST;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.Timeout = 500000;

            StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream());
            sw.WriteLine(data);
            sw.Close();

            Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);

            return (HttpWebResponse)responseTask.Result;
        
        }

        public static HttpWebResponse SendJSONPostRequest(string jsonData, string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = JSONContentType;
                httpWebRequest.Method = POST;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.Timeout = 500000;

                StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream());
                sw.WriteLine(jsonData);
                sw.Close();

                Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);

                return (HttpWebResponse)responseTask.Result;
            }
            catch (Exception Ex)
            {
                ErrorAndLogUtility.WriteError(Ex);
                throw;
            }

        }

        public static bool SendJSONGetRequest(string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = JSONContentType;
                httpWebRequest.Method = GET;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.Timeout = 500000;

                HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (objResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception Ex)
            {
                ErrorAndLogUtility.WriteError(Ex);
                return false;
            }
            return false;

        }

        public static string GetHttpWebResponseData(HttpWebResponse response)
        {
            string data = string.Empty;
            if (response != null)
            {
                StreamReader incomingStreamReader = new StreamReader(response.GetResponseStream());
                data = incomingStreamReader.ReadToEnd();
                incomingStreamReader.Close();
                response.GetResponseStream().Close();
            }
            return data;
        }

    }
}
