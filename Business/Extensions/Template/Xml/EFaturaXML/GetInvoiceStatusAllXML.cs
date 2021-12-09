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
    public class GetInvoiceStatusAllXML : ITemplate<RGetInvoiceStatusAllRequest>
    {
        public static readonly XNamespace xmlns = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace SOAPENV = "http://schemas.xmlsoap.org/soap/envelope/";
        public XElement Run(RGetInvoiceStatusAllRequest model)
        {
            XElement xElement = new XElement(xmlns + "Envelope",
                new XAttribute("SOAPENV", SOAPENV.NamespaceName)
                );

            XElement SoapHeader = new XElement(SOAPENV + "Header");

            xElement.Add(SoapHeader);

            return xElement;
        }
    }
}
