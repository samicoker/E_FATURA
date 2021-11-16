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
    public class EArchiveReportXML : ITemplate<RGetEArchiveReportRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public XElement Run(RGetEArchiveReportRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");
            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement GetEArchiveReportRequest = new XElement(arc + "GetEArchiveReportRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.RequestHeader.SESSION_ID));
            GetEArchiveReportRequest.Add(REQUEST_HEADER);

            GetEArchiveReportRequest.Add(new XElement("REPORT_PERIOD", model.REPORT_PERIOD));
            GetEArchiveReportRequest.Add(new XElement("REPORT_STATUS_FLAG", model.REPORT_STATUS_FLAG));

            Body.Add(GetEArchiveReportRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
