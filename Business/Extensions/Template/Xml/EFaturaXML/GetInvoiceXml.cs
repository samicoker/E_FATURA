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
    public class GetInvoiceXml:ITemplate<RGetInvoiceRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace wsdl = "http://schemas.i2i.com/ei/wsdl";

        public XElement Run(RGetInvoiceRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
             new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
             new XAttribute(XNamespace.Xmlns + "wsdl", wsdl.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement GetInvoiceRequest = new XElement(wsdl + "GetInvoiceRequest");

            XElement RequestHeader = new XElement("REQUEST_HEADER");

            RequestHeader.Add(new XElement("SESSION_ID", model.Body?.GetInvoiceRequest?.RequestHeader.SESSION_ID));
            RequestHeader.Add(new XElement("COMPRESSED", model.Body?.GetInvoiceRequest?.RequestHeader.COMPRESSED));

            GetInvoiceRequest.Add(RequestHeader);

            XElement INVOICE_SEARCH_KEY = new XElement("INVOICE_SEARCH_KEY");
            INVOICE_SEARCH_KEY.Add(new XElement("LIMIT", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.LIMIT));
            INVOICE_SEARCH_KEY.Add(new XElement("DATE_TYPE", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.DATE_TYPE));
            INVOICE_SEARCH_KEY.Add(new XElement("START_DATE", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.START_DATE));
            INVOICE_SEARCH_KEY.Add(new XElement("END_DATE", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.END_DATE));
            INVOICE_SEARCH_KEY.Add(new XElement("READ_INCLUDED", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.READ_INCLUDED));
            INVOICE_SEARCH_KEY.Add(new XElement("DIRECTION", model.Body?.GetInvoiceRequest?.INVOICE_SEARCH_KEY?.DIRECTION));

            GetInvoiceRequest.Add(INVOICE_SEARCH_KEY);

            GetInvoiceRequest.Add(new XElement("HEADER_ONLY", model.Body?.GetInvoiceRequest?.HEADER_ONLY));

            Body.Add(GetInvoiceRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
