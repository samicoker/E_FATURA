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
    public class LoginXml : ITemplate<RLoginRequest>
    {
        public static readonly XNamespace xmlns = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace xmlnsLoginReq = "http://schemas.i2i.com/ei/wsdl";
        public XElement Run(RLoginRequest model)
        {
            XElement xElement = new XElement(xmlns + "Envelope");
            var body = new XElement(xmlns + "Body");

            XElement LoginRequest = new XElement(xmlnsLoginReq + "LoginRequest");
            

            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.Request_Header?.SESSION_ID));
            REQUEST_HEADER.Add(new XElement("CLIENT_TXN_ID", model.Request_Header?.CLIENT_TXN_ID));
            REQUEST_HEADER.Add(new XElement("INTL_TXN_ID", model.Request_Header?.INTL_TXN_ID));
            REQUEST_HEADER.Add(new XElement("INTL_PARENT_TXN_ID", model.Request_Header?.INTL_PARENT_TXN_ID));
            REQUEST_HEADER.Add(new XElement("ACTION_DATE", model.Request_Header?.ACTION_DATE));

            XElement CHANGE_INFO = new XElement("CHANGE_INFO");
            CHANGE_INFO.Add(new XElement("CDATE", model.Request_Header?.CHANGE_INFO?.CDATE));
            CHANGE_INFO.Add(new XElement("CPOSITION_ID", model.Request_Header?.CHANGE_INFO?.CPOSITION_ID));
            CHANGE_INFO.Add(new XElement("CUSER_ID", model.Request_Header?.CHANGE_INFO?.CUSER_ID));
            CHANGE_INFO.Add(new XElement("UDATE", model.Request_Header?.CHANGE_INFO?.UDATE));
            CHANGE_INFO.Add(new XElement("UPOSITION_ID", model.Request_Header?.CHANGE_INFO?.UPOSITION_ID));
            CHANGE_INFO.Add(new XElement("UUSER_ID", model.Request_Header?.CHANGE_INFO?.UUSER_ID));

            REQUEST_HEADER.Add(CHANGE_INFO);

            REQUEST_HEADER.Add(new XElement("REASON", model.Request_Header?.REASON));
            REQUEST_HEADER.Add(new XElement("APPLICATION_NAME", model.Request_Header?.APPLICATION_NAME));
            REQUEST_HEADER.Add(new XElement("HOSTNAME", model.Request_Header?.HOSTNAME));
            REQUEST_HEADER.Add(new XElement("CHANNEL_NAME", model.Request_Header?.CHANNEL_NAME));
            REQUEST_HEADER.Add(new XElement("SIMULATION_FLAG", model.Request_Header?.SIMULATION_FLAG));
            REQUEST_HEADER.Add(new XElement("COMPRESSED", model.Request_Header?.COMPRESSED));
            REQUEST_HEADER.Add(new XElement("ATTRIBUTES", model.Request_Header?.ATTRIBUTES));

            LoginRequest.Add(REQUEST_HEADER);

            LoginRequest.Add(new XElement("USER_NAME", model.USER_NAME));
            LoginRequest.Add(new XElement("PASSWORD", model.PASSWORD));

            body.Add(LoginRequest);

            xElement.Add(body);

            return xElement;
        }
    }
}
