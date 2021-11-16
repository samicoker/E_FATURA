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
    public class ReadFromArchiveRequestXML : ITemplate<RReadFromArchiveRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public XElement Run(RReadFromArchiveRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement ArchiveInvoiceReadRequest = new XElement(arc + "ArchiveInvoiceReadRequest");
            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.REQUEST_HEADER?.SESSION_ID));

            ArchiveInvoiceReadRequest.Add(REQUEST_HEADER);
            ArchiveInvoiceReadRequest.Add(new XElement("INVOICEID", model.INVOICEID));
            ArchiveInvoiceReadRequest.Add(new XElement("PORTAL_DIRECTION", model.PORTAL_DIRECTION));
            ArchiveInvoiceReadRequest.Add(new XElement("PROFILE", model.PROFILE));

            Body.Add(ArchiveInvoiceReadRequest);
            xElement.Add(Body);

            return xElement;
        }
    }
}
