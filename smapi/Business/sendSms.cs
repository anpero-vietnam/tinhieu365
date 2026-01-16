using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace eSMSAPI_Demo.Business
{
    public class sendSms
    {
        const string APIKey = "192E0240FA51B48FC88F0B4C4569B3";//Dang ky tai khoan tai esms.vn de lay key
        const string SecretKey = "2AC5311C50F0BF17823B88AA54B7DA";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="SMSTYPE">3 ngẫu nhiên, 4 1900xxxx,6</param>
        /// <returns></returns>
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
}