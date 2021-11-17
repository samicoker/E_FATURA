using Business.Abstract;
using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Extensions.Template.Xml.EFaturaXML
{
    public class ReadEArchiveReportXML:ITemplate<RReadEArchiveReportRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";

        public XElement Run(RReadEArchiveReportRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");
            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement ReadEArchiveReportRequest = new XElement(arc + "ReadEArchiveReportRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.BodyRead.RequestHeader.SESSION_ID));

            ReadEArchiveReportRequest.Add(REQUEST_HEADER);

            ReadEArchiveReportRequest.Add(new XElement("RAPOR_NO", model.BodyRead.RaporNo));

            Body.Add(ReadEArchiveReportRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
