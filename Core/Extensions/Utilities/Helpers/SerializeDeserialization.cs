using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Core.Extensions.Utilities.Helpers
{
    public static class SerializeDeserialization
    {


        public static string ObjectToSoapXml<T>(this T a)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var xml = "";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, a);
                    xml = sww.ToString();
                }
            }

            return xml;
        }


    }
}
