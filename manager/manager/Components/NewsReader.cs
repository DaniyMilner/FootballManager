using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace manager.Components
{
    public class NewsReader
    {
        public static void ReadNewsFromFootbalUA()
        {
            var xmlString = @"" +  GET("http://football.ua/rss2.ashx");
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            foreach (XmlNode noda in xmlDocument.DocumentElement)
            {
                foreach (XmlNode xmlNode in noda.SelectNodes("item"))
                {
                    foreach (XmlNode node in xmlNode)
                    {
                        var temm = 0;
                    }
                }
            }
        }

        private static string GET(string Url)
        {
            WebRequest req = System.Net.WebRequest.Create(Url);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
    }
}