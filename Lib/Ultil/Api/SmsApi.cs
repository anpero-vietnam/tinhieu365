using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Ultil.Api
{
    public class SendSms
    {
       // const string APIKey = "192E0240FA51B48FC88F0B4C4569B3";//Dang ky tai khoan tai esms.vn de lay key
       // const string SecretKey = "2AC5311C50F0BF17823B88AA54B7DA";
        
       //<option value="-1">nhắn tin tới số điện thoại</option>
       //<option value="4">đầu số cố định 19001534 - 500 đ</option>
       //<option value="6">đầu số cố định 8755- 350 đ </option>
       //<option value="3">đầu số ngẫu nhiên- 280 đ</option>
       //<option value="1">BRANDNAME (not work now)</option>
                                                    
        String APIKey = System.Configuration.ConfigurationManager.AppSettings["SmsAPIKey"];
        string SecretKey = System.Configuration.ConfigurationManager.AppSettings["SmsSecretKey"];
        public int Send(string phone, string message, String SMSTYPE)
        {
            string url = "http://api.esms.vn/MainService.svc/xml/SendMultipleMessage_V2/";
            // declare ascii encoding
            UTF8Encoding encoding = new UTF8Encoding();

            string strResult = string.Empty;
            // sample xml sent to Service & this data is sent in POST


            // SampleXml = @"<RQST>"
            //                  + "<APIKEY>yourapikey</APIKEY>"
            //                  + "<SECRETKEY>yoursecretkey</SECRETKEY>"                                    
            //                  + "<ISFLASH>0</ISFLASH>"
            //                  + "<CONTENT>Welcome to esms.vn</CONTENT>"
            //                  + "<CONTACTS>"
            //                  + "<CUSTOMER>"
            //                  + "<PHONE>0902434340</PHONE>"
            //                  + "</CUSTOMER>"
            //                  + "<CUSTOMER>"
            //                  + "<PHONE>0902434341</PHONE>"
            //                  + "</CUSTOMER>"
            //                  + "</CONTACTS>"

            string customers = "";

            string[] lstPhone = phone.Split(',');

            for (int i = 0; i < lstPhone.Count(); i++)
            {
                customers = customers + @"<CUSTOMER>"
                                + "<PHONE>" + lstPhone[i] + "</PHONE>"
                                + "</CUSTOMER>";
            }

            //string SampleXml = @"<RQST>"
            //                    + "<APIKEY>" + APIKey + "</APIKEY>"
            //                    + "<SECRETKEY>" + SecretKey + "</SECRETKEY>"
            //                    + "<ISFLASH>0</ISFLASH>"
            //                    + "<UNICODE>1</UNICODE>"//=1 nếu muốn gửi có dấu, có dấu: 70 ký tự 1 tin, không dấu: 160 ký tự 1 tin
            //                    + "<CONTENT>" + Uri.EscapeDataString(message) + "</CONTENT>"//Nếu gửi tin có dấu thì cần escapeData, không dấu thì không cần
            //                    + "<CONTACTS>" + customers + "</CONTACTS>"


            //+ "</RQST>";

            string SampleXml = @"<RQST>"
                               + "<APIKEY>" + APIKey + "</APIKEY>"
                               + "<SECRETKEY>" + SecretKey + "</SECRETKEY>"
                               + "<ISFLASH>0</ISFLASH>"
                               + "<SMSTYPE>" + SMSTYPE + "</SMSTYPE>"
                               + "<CONTENT>" + message + "</CONTENT>"
                               + "<CONTACTS>" + customers + "</CONTACTS>"


           + "</RQST>";
            string postData = SampleXml.Trim().ToString();
            // convert xmlstring to byte using ascii encoding
            byte[] data = encoding.GetBytes(postData);
            // declare httpwebrequet wrt url defined above
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            // set method as post
            webrequest.Method = "POST";
            webrequest.Timeout = 500000;
            // set content type
            webrequest.ContentType = "application/x-www-form-urlencoded";
            // set content length
            webrequest.ContentLength = data.Length;
            // get stream data out of webrequest object
            Stream newStream = webrequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //// declare & read response from service
            //HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            //// set utf8 encoding
            //Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            //// read response stream from response object
            //StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            //// read string from stream data
            //strResult = loResponseStream.ReadToEnd();
            //// close the stream object
            //loResponseStream.Close();
            //// close the response object
            //webresponse.Close();
            //// below steps remove unwanted data from response string
            //strResult = strResult.Replace("</string>", "");
            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml(strResult); // suppose that myXmlString contains "<Names>...</Names>"

            //XmlNodeList xnList = xml.SelectNodes("/SmsResultModel");
            //foreach (XmlNode xn in xnList)
            //{
            //    string CodeResult = xn["CodeResult"].InnerText;///100 thanh cong, 101 dang nhao that bại, 103 khong du tien 104 ma bandName khong dung
            //    string ErrorMessage = xn["ErrorMessage"].InnerText;
            //    string SMSID = xn["SMSID"].InnerText;

            //}
            return 1;

        }
        public int SendBrandname(string phone, string message)
        {
            string url = "http://api.esms.vn/MainService.svc/xml/SendMultipleSMSBrandname/";
            // declare ascii encoding
            UTF8Encoding encoding = new UTF8Encoding();

            string strResult = string.Empty;
            // sample xml sent to Service & this data is sent in POST

            string customers = "";

            string[] lstPhone = phone.Split(',');

            for (int i = 0; i < lstPhone.Count(); i++)
            {
                customers = customers + @"<CUSTOMER>"
                                + "<PHONE>" + lstPhone[i] + "</PHONE>"
                                + "</CUSTOMER>";
            }


            string SampleXml = @"<RQST>"
                               + "<APIKEY>" + APIKey + "</APIKEY>"
                               + "<SECRETKEY>" + SecretKey + "</SECRETKEY>"
                               + "<SMSTYPE>2</SMSTYPE>"
                               + "<BRANDNAME>KIM.HOANG</BRANDNAME>"
                               + "<CONTENT>" + message + "</CONTENT>"
                               + "<CONTACTS>" + customers + "</CONTACTS>"


           + "</RQST>";
            string postData = SampleXml.Trim().ToString();
            // convert xmlstring to byte using ascii encoding
            byte[] data = encoding.GetBytes(postData);
            // declare httpwebrequet wrt url defined above
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            // set method as post
            webrequest.Method = "POST";
            webrequest.Timeout = 500000;
            // set content type
            webrequest.ContentType = "application/x-www-form-urlencoded";
            // set content length
            webrequest.ContentLength = data.Length;
            // get stream data out of webrequest object
            Stream newStream = webrequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            // declare & read response from service
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            // set utf8 encoding
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            // read response stream from response object
            StreamReader loResponseStream =
                new StreamReader(webresponse.GetResponseStream(), enc);
            // read string from stream data
            strResult = loResponseStream.ReadToEnd();
            // close the stream object
            loResponseStream.Close();
            // close the response object
            webresponse.Close();
            // below steps remove unwanted data from response string
            strResult = strResult.Replace("</string>", "");

            return 1;
        }
    }
    public class SendSmsV2Json
    {
        public SendSmsV2Json()
        {
            APIKey = System.Configuration.ConfigurationManager.AppSettings["SmsAPIKey"];
            SecretKey = System.Configuration.ConfigurationManager.AppSettings["SmsSecretKey"];
            SmsType = System.Configuration.ConfigurationManager.AppSettings["SmsType"]; 


        }
        string smsType;
        String aPIKey;
        string secretKey;

        public string APIKey
        {
            get
            {
                return aPIKey;
            }

            set
            {
                aPIKey = value;
            }
        }

        public string SecretKey
        {
            get
            {
                return secretKey;
            }

            set
            {
                secretKey = value;
            }
        }

        public string SmsType
        {
            get
            {
                return smsType;
            }

            set
            {
                smsType = value;
            }
        }

        public string SendJson(string phone, string message)
        {
            // Create URL, method 1:
            string URL = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone=" + phone + "&Content=" + message + "&ApiKey=" + APIKey + "&SecretKey=" + SecretKey + "&IsUnicode=0&SmsType="+ SmsType;
            string result = SendGetRequest(URL);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//100 is successfull
            return (string)ojb["SMSID"];//id of SMS            
        }

        private string SendGetRequest(string RequestUrl)
        {
            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            if (address == null) { throw new ArgumentNullException("address"); }
            try
            {
                request = WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = ".NET Sample";
                request.KeepAlive = false;
                request.Timeout = 15 * 1000;
                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    result = result.Replace("</string>", "");
                    return result;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                            "The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return null;
        }

        //Send SMS with Alpha Sender
        public string SendBrandnameJson(string phone, string message, string brandname)
        {
            //http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone={Phone}&Content={Content}&BrandnameCode={BrandnameCode}&ApiKey={ApiKey}&SecretKey={SecretKey}&SmsType={SmsType}&SendDate={SendDate}&SandBox={SandBox}
            //url vi du
            // sử dụng cách 1:
            string URL = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone=" + phone + "&Content=" + message + "&Brandname=" + brandname + "&ApiKey=" + APIKey + "&SecretKey=" + SecretKey + "&IsUnicode=0&SmsType=2";

            string result = SendGetRequest(URL);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//trả về 100 là thành công
            int CountRegenerate = (int)ojb["CountRegenerate"];
            return (string)ojb["SMSID"];//id của tin nhắn            
        }

        //Get Account Balance - Lay so du tai khoan
        public long GetBalance()
        {
            string data = "http://rest.esms.vn/MainService.svc/json/GetBalance/" + APIKey + "/" + SecretKey + "";
            string result = SendGetRequest(data);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResponse"];//trả về 100 là thành công
            int UserID = (int)ojb["UserID"];//id tài khoản
            return (long)ojb["Balance"];//tiền trong tài khoản            
        }

    }
}
