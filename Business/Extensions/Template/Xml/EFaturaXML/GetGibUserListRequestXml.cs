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
    public class GetGibUserListRequestXml:ITemplate<RGetGibUserListRequest>
    {
        public static readonly XNamespace xmlns = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace xmlnsLoginReq = "http://schemas.i2i.com/ei/wsdl";
        public XElement Run(RGetGibUserListRequest model)
        {
            XElement xElement = new XElement(xmlns + "Envelope");
            var body = new XElement(xmlns + "Body");

            XElement GetGibUserListRequest = new XElement(xmlnsLoginReq + "GetGibUserListRequest");


            XElement REQUEST_HEADER = new XElement("REQUEST_HEADER");
            REQUEST_HEADER.Add(new XElement("SESSION_ID", model.REQUEST_HEADER?.SESSION_ID));
            REQUEST_HEADER.Add(new XElement("CLIENT_TXN_ID", model.REQUEST_HEADER?.CLIENT_TXN_ID));
            REQUEST_HEADER.Add(new XElement("INTL_TXN_ID", model.REQUEST_HEADER?.INTL_TXN_ID));
            REQUEST_HEADER.Add(new XElement("INTL_PARENT_TXN_ID", model.REQUEST_HEADER?.INTL_PARENT_TXN_ID));
            REQUEST_HEADER.Add(new XElement("ACTION_DATE", model.REQUEST_HEADER?.ACTION_DATE));

            XElement CHANGE_INFO = new XElement("CHANGE_INFO");
            CHANGE_INFO.Add(new XElement("CDATE", model.REQUEST_HEADER?.CHANGE_INFO?.CDATE));
            CHANGE_INFO.Add(new XElement("CPOSITION_ID", model.REQUEST_HEADER?.CHANGE_INFO?.CPOSITION_ID));
            CHANGE_INFO.Add(new XElement("CUSER_ID", model.REQUEST_HEADER?.CHANGE_INFO?.CUSER_ID));
            CHANGE_INFO.Add(new XElement("UDATE", model.REQUEST_HEADER?.CHANGE_INFO?.UDATE));
            CHANGE_INFO.Add(new XElement("UPOSITION_ID", model.REQUEST_HEADER?.CHANGE_INFO?.UPOSITION_ID));
            CHANGE_INFO.Add(new XElement("UUSER_ID", model.REQUEST_HEADER?.CHANGE_INFO?.UUSER_ID));

            REQUEST_HEADER.Add(CHANGE_INFO);

            REQUEST_HEADER.Add(new XElement("REASON", model.REQUEST_HEADER?.REASON));
            REQUEST_HEADER.Add(new XElement("APPLICATION_NAME", model.REQUEST_HEADER?.APPLICATION_NAME));
            REQUEST_HEADER.Add(new XElement("HOSTNAME", model.REQUEST_HEADER?.HOSTNAME));
            REQUEST_HEADER.Add(new XElement("CHANNEL_NAME", model.REQUEST_HEADER?.CHANNEL_NAME));
            REQUEST_HEADER.Add(new XElement("SIMULATION_FLAG", model.REQUEST_HEADER?.SIMULATION_FLAG));
            REQUEST_HEADER.Add(new XElement("COMPRESSED", model.REQUEST_HEADER?.COMPRESSED));
            REQUEST_HEADER.Add(new XElement("ATTRIBUTES", model.REQUEST_HEADER?.ATTRIBUTES));

            GetGibUserListRequest.Add(REQUEST_HEADER);

            GetGibUserListRequest.Add(new XElement("TYPE", model.TYPE));
            GetGibUserListRequest.Add(new XElement("DOCUMENT_TYPE", model.DOCUMENT_TYPE));
            GetGibUserListRequest.Add(new XElement("REGISTER_TIME_START", model.REGISTER_TIME_START));
            GetGibUserListRequest.Add(new XElement("ALIAS_TYPE", model.ALIAS_TYPE));
            GetGibUserListRequest.Add(new XElement("ALIAS_MODIFY_DATE", model.ALIAS_MODIFY_DATE));


            body.Add(GetGibUserListRequest);

            xElement.Add(body);

            return xElement;
        }
    }
}
