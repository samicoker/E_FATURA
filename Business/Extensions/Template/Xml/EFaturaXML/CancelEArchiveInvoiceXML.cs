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
    public class CancelEArchiveInvoiceXML : ITemplate<RCancelEArchiveInvoiceRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public static readonly XNamespace xm = "http://www.w3.org/2005/05/xmlmime";

        public XElement Run(RCancelEArchiveInvoiceRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xm", xm.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement CancelEArchiveInvoiceRequest = new XElement(arc + "CancelEArchiveInvoiceRequest");

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.REQUEST_HEADER.SESSION_ID));

            CancelEArchiveInvoiceRequest.Add(REQUEST_HEADER);

            XElement CancelEArsivInvoiceContent = new XElement("CancelEArsivInvoiceContent");
            CancelEArsivInvoiceContent.Add(new XElement("FATURA_UUID", model.CancelEArsivInvoiceContent.FATURA_UUID));

            CancelEArchiveInvoiceRequest.Add(CancelEArsivInvoiceContent);

            Body.Add(CancelEArchiveInvoiceRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
