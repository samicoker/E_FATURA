
using Core.Extensions.Utilities.Constants;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Core.Extensions.Utilities.WebAccess.SOAP
{
    public static class CallWebService
    {
        /// <summary>
        /// Execute a Soap WebService call
        /// </summary>
        public static IDataResult<XmlDocument> Execute(string url, string xml, string method = "Get", Dictionary<string, string> header = null,string TestResponse = default)
        {
            if (!string.IsNullOrEmpty(TestResponse))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(TestResponse);

                return new SuccessDataResult<XmlDocument>(document);
            }
            try
            {
                HttpWebRequest request = CreateWebRequest(url, method, header);
                XmlDocument soapEnvelopeXml = new XmlDocument();

                soapEnvelopeXml.LoadXml(xml);

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }


                using (WebResponse response = request.GetResponse())
                {
                    if (((System.Net.HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(CoreMessages.ErrorService + ((System.Net.HttpWebResponse)response).StatusCode);
                    }

                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(soapResult);

                        return new SuccessDataResult<XmlDocument>(document);
                    }
                }
            }
            catch (WebException ex)
            {
                if(ex.Response is not null)
                { 
                    using (StreamReader rd = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(soapResult);
                    }
                }

                throw new Exception("Gönderilirken hata oluştu." + ex.Message);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Gönderilirken hata oluştu." + ex.Message);
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string method, Dictionary<string, string> header = null)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        
            if (header != null)
            {
                foreach (var item in header)
                {
                    webRequest.Headers.Add(item.Key, item.Value);
                }
            }
            else
            {
                webRequest.Headers.Add(@"SOAP:Action");
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
            }
            webRequest.Method = method;
            return webRequest;
        }
    }
}
