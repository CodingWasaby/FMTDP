using FocusMediaCommons.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FocusMediaCommons.Handler
{
    class RestfulHandler : IRestful
    {
        public TOut POST<TIn, TOut>(string url, TIn param)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            string data = JsonConvert.SerializeObject(param);

            byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());
            request.ContentLength = byteData.Length;
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                var resultStr = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<TOut>(resultStr);
            }
        }

        public T POST<T>(string url, string data)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            using (StreamWriter postStream = new StreamWriter(request.GetRequestStream()))
            {
                postStream.Write(data);
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                var resultStr = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(resultStr);
            }
        }

        public T GET<T>(string url, List<Tuple<string, string>> queryStrings = null)
        {
            if (queryStrings != null && queryStrings.Count > 0)
            {
                url += "?";
                foreach (var q in queryStrings)
                {
                    url += q.Item1 + "=" + q.Item2 + "&";
                }
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                var resultStr = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(resultStr);
            }
        }
    }
}
