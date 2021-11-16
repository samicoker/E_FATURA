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
    public class GetEArchiveInvoiceStatusXML : ITemplate<RGetEArchiveInvoiceStatusRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public XElement Run(RGetEArchiveInvoiceStatusRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");
            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement GetEArchiveInvoiceStatusRequest = new XElement(arc + "GetEArchiveInvoiceStatusRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.Body.RequestHeader.SESSION_ID));
            GetEArchiveInvoiceStatusRequest.Add(REQUEST_HEADER);

            GetEArchiveInvoiceStatusRequest.Add(new XElement("UUID", model.Body.UUID));

            Body.Add(GetEArchiveInvoiceStatusRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
