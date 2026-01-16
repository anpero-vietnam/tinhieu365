using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Dal
{
 
    public class FacebookMng
    {
        
        private string appId= System.Configuration.ConfigurationManager.AppSettings["FaceBookAppId"];
        private string apiKey;
        private string appSecret= System.Configuration.ConfigurationManager.AppSettings["FaceBookAppSecret"];

        #region Application Settings Properties
        /// <summary>
        /// Get And Set Application ID
        /// </summary>
        public String ApplicationID
        {
            get
            {
                
                return appId;
            }
            set
            {
                appId = value;
            }
        }

        /// <summary>
        /// Get And Set API Key
        /// </summary>
        public String APIKey
        {
            get
            {
                return apiKey;
            }
            set
            {
                apiKey = value;
            }
        }

        /// <summary>
        /// Get And Set Application Secret
        /// </summary>
        public String ApplicationSecret
        {
            get
            {
                return appSecret;
            }
            set
            {
                appSecret = value;
            }
        }
        string accessToken;
        /// <summary>
        /// Get And Set Access Token
        /// </summary>
        public String AccessToken
        {
            get
            {
                string redirect_uri = System.Configuration.ConfigurationManager.AppSettings["FaceBookredirect_uri"];
                return GetAccessToken(redirect_uri);
            }
            set
            {
                accessToken = value;
            }
        }
        #endregion
        
        

        public String GetAccessToken(string redirect_uri)
        {
            //create the constructor with post type and few data
            MakeWebRequest myRequest = new MakeWebRequest("https://graph.facebook.com/oauth/access_token", "GET", "client_id=" + this.ApplicationID + "&client_secret=" + this.ApplicationSecret + "&redirect_uri="+ redirect_uri);
            string accessToken = myRequest.GetResponse().Split('&')[0];
            accessToken = accessToken.Split('=')[1];
            return accessToken;
        }

    }
    public class MakeWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public MakeWebRequest(string url)
        {
            // Create a request using a URL that can receive a post.

            request = WebRequest.Create(url);
        }

        public MakeWebRequest(string url, string method): this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public MakeWebRequest(string url, string method, string data)
            : this(url, method)
        {

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

    }
}
