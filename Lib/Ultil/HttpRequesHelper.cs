using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//thang.td
namespace Ultil
{
    public class HttpRequesHelper<T>
    {
        private static readonly HttpClient client = new HttpClient();
        async public static Task<string> PostImage(string url, List<HttpPostedFileBase> mediaFile, object paramObject)
        {
            var requestContent = new MultipartFormDataContent();
            // here you can specify boundary if you need---
            foreach (var item in mediaFile)
            {

                if (item.ContentLength > 0)
                {
                    MemoryStream target = new MemoryStream();
                    item.InputStream.CopyTo(target);
                    var imageContent = new ByteArrayContent(target.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "mediaFile", item.FileName);
                }
            }
            var content = ObjectToDictionary(paramObject);
            foreach (KeyValuePair<string, string> entry in content)
            {
                requestContent.Add(new StringContent(entry.Value, Encoding.UTF8), entry.Key);
            }

            var response = await client.PostAsync(url, requestContent);
            return response.Content.ReadAsStringAsync().Result;
        }

        async public static Task<string> PostImage(string url, List<byte[]> ImageData, string fileName, object paramObject)
        {
            var requestContent = new MultipartFormDataContent();
            // here you can specify boundary if you need---
            foreach (var item in ImageData)
            {
                var imageContent = new ByteArrayContent(item);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                requestContent.Add(imageContent, "mediaFile", fileName);
            }
            var content = ObjectToDictionary(paramObject);
            foreach (KeyValuePair<string, string> entry in content)
            {
                requestContent.Add(new StringContent(entry.Value, Encoding.UTF8), entry.Key);
            }

            var response = await client.PostAsync(url, requestContent);
            return response.Content.ReadAsStringAsync().Result;
        }
        async public static Task<string> PostImage(string url, byte[] ImageData, string fileName, object paramObject)
        {
            var requestContent = new MultipartFormDataContent();
            // here you can specify boundary if you need---
            var imageContent = new ByteArrayContent(ImageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");            
            requestContent.Add(imageContent, "mediaFile", fileName);

            var content = ObjectToDictionary(paramObject);
            
            foreach (KeyValuePair<string, string> entry in content)
            {
                requestContent.Add(new StringContent(entry.Value,Encoding.UTF8),entry.Key);                
            }
            var response = await client.PostAsync(url, requestContent);
            return response.Content.ReadAsStringAsync().Result;
        }
        public static T Post(string url,object paramObject)
        {

            string json = "";            
            var content = new FormUrlEncodedContent(ObjectToDictionary(paramObject));
            
            
            var response = client.PostAsync(url, content).Result;
            json = response.Content.ReadAsStringAsync().Result;          
            if (json != "")
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }        

        public static string PostAndReturnJson(string url, object paramObject)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new FormUrlEncodedContent(ObjectToDictionary(paramObject));
            var response = client.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
            
        }
        public static T Get(string url, object paramObject=null)
        {
            string json = "";
            if (paramObject != null)
            {
                var content = ObjectToDictionary(paramObject);
                var condition = "?";
                foreach (KeyValuePair<string, string> entry in content)
                {
                    url += condition + entry.Key + "=" + System.Web.HttpUtility.UrlEncode(entry.Value);
                    condition = "&";
                }
            }
            var response = client.GetAsync(url).Result;            
            json = response.Content.ReadAsStringAsync().Result;
            try
            {
                if (json != "")
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {

                return default(T);
            }
          
        }
        public static T Get(string url, object paramObject, object Client)
        {
            string json = "";
            var content = ObjectToDictionary(paramObject);
            var condition = "?";
            foreach (KeyValuePair<string, string> entry in content)
            {
                url += condition + entry.Key + "=" + entry.Value;
                condition = "&";
            }
            var content2 = ObjectToDictionary(Client);
            foreach (KeyValuePair<string, string> entry2 in content2)
            {
                url += condition + entry2.Key + "=" + System.Web.HttpUtility.UrlEncode(entry2.Value);
                condition = "&";
            }                 
            var response = client.GetAsync(url).Result;

            json = response.Content.ReadAsStringAsync().Result;
            try
            {
                if (json != "")
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {

                return default(T);
            }

        }
        public static string GetAndReturnJson(string url,object paramObject=null)
        {
            var content = ObjectToDictionary(paramObject);
            var condition = "?";
            foreach (KeyValuePair<string, string> entry in content)
            {
                url += condition + entry.Key + "=" + entry.Value;
                condition = "&";
            }
            var response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
            
        }
        public static Dictionary<string,string> ObjectToDictionary(object obj)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (obj != null)
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    string propName = prop.Name;
                    var val = obj.GetType().GetProperty(propName).GetValue(obj, null);
                    if (val != null)
                    {
                        ret.Add(propName, val.ToString());
                    }
                    else
                    {
                        ret.Add(propName, "");
                    }
                }
            }
            return ret;
        }
    }
}
