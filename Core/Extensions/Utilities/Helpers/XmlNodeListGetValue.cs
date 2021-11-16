using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.Extensions.Utilities.Helpers
{
    public static class XmlNodeListGetValue
    {
        public static string GetValue(this XmlNodeList xml, string Tag)
        {
            for (int i = 0; i < xml.Count; i++)
            {
                if (xml[i].Name == Tag)
                {
                    return xml[i].ChildNodes[0]?.Value;
                }
            }
            return string.Empty;
        }
        public static List<string> GetValues(this XmlNodeList xml, string Tag)
        {
            List<string> getValues = new List<string>();

            for (int i = 0; i < xml.Count; i++)
            {
                if (xml[i].Name == Tag)
                {
                    if (!String.IsNullOrEmpty(xml[i].ChildNodes[0]?.InnerText))
                    {
                        getValues.Add(xml[i].ChildNodes[0].InnerText);
                    }
                    else
                    {
                        getValues.Add("");
                    }

                }
            }
            return getValues;
        }
        public static string GetClass(this XmlNode xml, string Tag)
        {

            for (int i = 0; i < xml.ChildNodes.Count; i++)
            {
                if (xml.ChildNodes[i].Name == Tag)
                {
                    return xml.ChildNodes[i].InnerXml;
                }
            }
            return string.Empty;
        }
        public static List<string> GetClassess(this XmlNode xml, string Tag)
        {

            List<string> getClassess = new List<string>();

            for (int i = 0; i < xml.ChildNodes.Count; i++)
            {
                if (xml.ChildNodes[i].Name == Tag)
                {
                    getClassess.Add(xml.ChildNodes[i].InnerXml);
                }
            }
            return getClassess;
        }
        public static XmlNode XmlStringToXmlNode(string xmlInputString)
        {
            if (String.IsNullOrEmpty(xmlInputString.Trim())) { throw new ArgumentNullException("xmlInputString"); }
            var xd = new XmlDocument();
            using (var sr = new StringReader(xmlInputString))
            {
                xd.Load(sr);
            }
            return xd;
        }
        public static XmlNode XmlStringToXmlNode2(string xmlInputString)
        {
            if (!String.IsNullOrEmpty(xmlInputString.Trim()))
            {
                var xd = new XmlDocument();
                using (var sr = new StringReader("<fatura>" + xmlInputString + "</fatura>"))
                {
                    xd.Load(sr);
                }
                return xd;
            }
            return null;
        }
    }
}
