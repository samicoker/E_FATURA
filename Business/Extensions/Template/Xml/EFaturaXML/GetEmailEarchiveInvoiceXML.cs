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
    public class GetEmailEarchiveInvoiceXML : ITemplate<RGetEmailEarchiveInvoiceRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public XElement Run(RGetEmailEarchiveInvoiceRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");
            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement GetEmailEarchiveInvoiceRequest = new XElement(arc + "GetEmailEarchiveInvoiceRequest");

            XElement RequestHeader = new XElement("REQUEST_HEADER");
            RequestHeader.Add(new XElement("SESSION_ID", model.RequestHeader.SESSION_ID));
            GetEmailEarchiveInvoiceRequest.Add(RequestHeader);

            GetEmailEarchiveInvoiceRequest.Add(new XElement("FATURA_UUID", model.FATURA_UUID));
            GetEmailEarchiveInvoiceRequest.Add(new XElement("EMAIL", model.EMAIL));

            Body.Add(GetEmailEarchiveInvoiceRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
