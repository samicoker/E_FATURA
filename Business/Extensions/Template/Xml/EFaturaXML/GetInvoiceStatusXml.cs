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
    public class GetInvoiceStatusXml : ITemplate<RGetInvoiceStatusRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace wsdl = "http://schemas.i2i.com/ei/wsdl";
        public static readonly XNamespace xm = "http://www.w3.org/2005/05/xmlmime";
        public XElement Run(RGetInvoiceStatusRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "wsdl", wsdl.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xm", xm.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");
            XElement GetInvoiceStatusRequest = new XElement(wsdl + "GetInvoiceStatusRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");

            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.Body?.REQUEST_HEADER?.SESSION_ID));
            REQUEST_HEADER.Add(new XElement("COMPRESSED", model.Body?.REQUEST_HEADER?.COMPRESSED));
            
            GetInvoiceStatusRequest.Add(REQUEST_HEADER);

            XElement INVOICE = new XElement("INVOICE",
                    new XAttribute("ID", model.Body?.INVOICE?.ID),
                    new XAttribute("UUID", model.Body?.INVOICE?.UUID));
            XElement HEADER = new XElement("HEADER");
            HEADER.Add(new XElement("DIRECTION", model.Body?.INVOICE?.HEADER?.DIRECTION));
            INVOICE.Add(HEADER);

            GetInvoiceStatusRequest.Add(INVOICE);

            Body.Add(GetInvoiceStatusRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
