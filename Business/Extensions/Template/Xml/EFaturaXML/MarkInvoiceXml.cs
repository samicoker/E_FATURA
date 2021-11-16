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
    public class MarkInvoiceXml:ITemplate<RMarkInvoiceRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace wsdl = "http://schemas.i2i.com/ei/wsdl";
        public static readonly XNamespace xm = "http://www.w3.org/2005/05/xmlmime";
        public static readonly XNamespace value = "READ";

        public XElement Run(RMarkInvoiceRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope", 
                new XAttribute(XNamespace.Xmlns + "soapenv",soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "wsdl",wsdl.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xm", xm.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement MarkInvoiceRequest = new XElement(wsdl + "MarkInvoiceRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");

            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.Body?.MarkInvoiceRequest?.RequestHeader?.SESSION_ID));
            REQUEST_HEADER.Add(new XElement("COMPRESSED", model.Body?.MarkInvoiceRequest?.RequestHeader?.COMPRESSED));

            MarkInvoiceRequest.Add(REQUEST_HEADER);

            XElement MARK = new XElement("MARK", new XAttribute("value", value.NamespaceName));

            for (int i = 0; i < model.Body?.MarkInvoiceRequest?.Mark?.Invoices.Count; i++)
            {
                XElement INVOICE = new XElement("INVOICE",
                    new XAttribute("ID", model.Body?.MarkInvoiceRequest?.Mark?.Invoices[i].ID),
                    new XAttribute("UUID", model.Body?.MarkInvoiceRequest?.Mark?.Invoices[i].UUID));
                XElement HEADER = new XElement("HEADER");
                INVOICE.Add(HEADER);
                MARK.Add(INVOICE);
            }

            MarkInvoiceRequest.Add(MARK);

            

            Body.Add(MarkInvoiceRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
