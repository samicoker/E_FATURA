using Business.Abstract;
using Business.Constantcs;
using Business.Extensions.Template.Xml.EFaturaXML;
using Core.Extensions.Utilities.Helpers;
using Core.Extensions.Utilities.WebAccess.SOAP;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Core.Extensions.Utilities.Helpers.XmlNodeListGetValue;

namespace Business.Concrete
{
    public class InvoiceManager : IInvoiceService
    {
        IInvoiceDal _iInvoiceDal;

        public InvoiceManager(IInvoiceDal invoiceDal)
        {
            _iInvoiceDal = invoiceDal;
        }

        #region AUTHENTICATION

        public IDataResult<LoginResponse> GetLogin(string userName, string password)
        {
            try
            {
                var res = _iInvoiceDal.GetLogin(userName, password);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RLoginRequest> template = new LoginXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();


                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/AuthenticationWS", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    //var sessionID = res_.Data.ChildNodes[1]?.ChildNodes[0]?.ChildNodes[0]?.ChildNodes[0]?.InnerText;
                    var node = res_.Data.ChildNodes[1]?.ChildNodes[0].ChildNodes[0];
                    LoginResponse loginResponse = new()
                    {
                        SessionId = node.ChildNodes.GetValue("SESSION_ID"),
                        ErrorType = new ERROR_TYPE
                        {
                            INTL_TXN_ID = node.ChildNodes[0].ChildNodes.GetValue("INTL_TXN_ID"),
                            ERROR_CODE = node.ChildNodes[0].ChildNodes.GetValue("ERROR_CODE"),
                            ERROR_SHORT_DES = node.ChildNodes[0].ChildNodes.GetValue("ERROR_SHORT_DES")
                        }
                    };

                    return new SuccessDataResult<LoginResponse>(loginResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<LoginResponse>(null, ex.Message);
            }
        }

        public IDataResult<LogOutResponse> GetLogOut(string sessionId)
        {
            try
            {
                var res = _iInvoiceDal.GetLogOut(sessionId);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RLogOutRequest> template = new LogOutXML();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();


                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/AuthenticationWS", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1]?.ChildNodes[0]?.ChildNodes[0]?.ChildNodes[0];

                    LogOutResponse logOutResponse = new()
                    {
                        RequestReturn = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = node.ChildNodes.GetValue("INTL_TXN_ID"),
                            CLIENT_TXN_ID = node.ChildNodes.GetValue("CLIENT_TXN_ID"),
                            RETURN_CODE = node.ChildNodes.GetValue("RETURN_CODE"),
                        },
                        ErrorType = new ERROR_TYPE
                        {
                            INTL_TXN_ID = node.ChildNodes.GetValue("INTL_TXN_ID"),
                            ERROR_CODE = node.ChildNodes.GetValue("ERROR_CODE"),
                            ERROR_SHORT_DES = node.ChildNodes.GetValue("ERROR_SHORT_DES"),
                        }
                    };

                    return new SuccessDataResult<LogOutResponse>(logOutResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<LogOutResponse>(null, ex.Message);
            }
        }

        #endregion

        #region E-INVOICE

        public IDataResult<SendInvoiceResponse> SendInvoice(string sessionId)
        {
            try
            {
                var res = _iInvoiceDal.SendInvoice(sessionId);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RSendInvoiceRequest> template = new SendInvoiceXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                SendInvoiceResponse sendInvoiceResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EFaturaOIB", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    sendInvoiceResponse = new SendInvoiceResponse
                    {
                        INTL_TXN_ID = node.ChildNodes.GetValue("INTL_TXN_ID"),
                        RETURN_CODE = node.ChildNodes.GetValue("RETURN_CODE"),
                        ERROR_CODE = node.ChildNodes.GetValue("ERROR_CODE"),
                        ERROR_SHORT_DES = node.ChildNodes.GetValue("ERROR_SHORT_DES"),
                        ERROR_LONG_DES = node.ChildNodes.GetValue("ERROR_LONG_DES")
                    };

                    return new SuccessDataResult<SendInvoiceResponse>(sendInvoiceResponse);
                }
                catch (Exception ex)
                {
                    var getresponseXmlConvertString = string.Empty;
                    if (res_?.Success == true)
                    {
                        getresponseXmlConvertString = res_.Data.InnerText;
                    }

                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SendInvoiceResponse>(null, ex.Message);
            }
        }

        public IDataResult<GetInvoiceResponse> GetInvoice(string sessionId, byte limit)
        {
            try
            {
                var res = _iInvoiceDal.GetInvoice(sessionId, limit);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RGetInvoiceRequest> template = new GetInvoiceXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                GetInvoiceResponse getInvoiceResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EFaturaOIB", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var baseInvo = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];
                    var node = baseInvo.ChildNodes[0];

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    getInvoiceResponse = new GetInvoiceResponse
                    {
                        Invoices = new List<Invoice>(),
                    };
                    var invoiceCount = baseInvo.ChildNodes.Count;

                    for (int i = 0; i < invoiceCount; i++)
                    {
                        var nodeInvoice = baseInvo.ChildNodes[i];

                        var nodeHeaderStr = nodeInvoice.GetClass("HEADER");
                        var nodeHeader = XmlStringToXmlNode2(nodeHeaderStr);


                        //XmlDocument doc = new XmlDocument();
                        //doc.LoadXml(nodeHeaderStr);
                        //XmlNode nodeHeader = doc.DocumentElement;

                        var nodeContentBase64 = nodeInvoice.ChildNodes.GetValue("CONTENT");
                        var nodeContentByte = Convert.FromBase64String(nodeContentBase64);
                        var nodeContentString = Encoding.UTF8.GetString(nodeContentByte);
                        var nodeContent = XmlStringToXmlNode(nodeContentString);

                        var nodeUBLExtensionsStr = nodeContent.ChildNodes[0].GetRegexClass("ext:UBLExtensions");
                        var nodeUBLExtensions = XmlStringToXmlNode2(nodeUBLExtensionsStr);

                        var uBLExtensionStr = nodeUBLExtensions.ChildNodes[0].GetRegexClass("ext:UBLExtension");
                        var uBLExtensionNode = XmlStringToXmlNode2(uBLExtensionStr);

                        var extensionContentStr = uBLExtensionNode.ChildNodes[0].GetRegexClass("ext:ExtensionContent");
                        var extensionContent = XmlStringToXmlNode2(extensionContentStr);

                        var uBLSignatureStr = extensionContent.ChildNodes[0].GetRegexClass("ds:Signature");
                        var uBLSignature = XmlStringToXmlNode2(uBLSignatureStr);

                        var uBLSignedInfoStr = uBLSignature.ChildNodes[0].GetRegexClass("ds:SignedInfo");
                        var uBLSignedInfo = XmlStringToXmlNode2(uBLSignedInfoStr);

                        var nodeAdditionalDocumentReferenceStr =
                            nodeContent.ChildNodes[0].GetRegexClass("cac:AdditionalDocumentReference");
                        var nodeAdditionalDocumentReference = XmlStringToXmlNode2(nodeAdditionalDocumentReferenceStr);

                        var signatureStr = nodeContent.ChildNodes[0].GetRegexClass("cac:Signature");
                        var signature = XmlStringToXmlNode2(signatureStr);

                        var signatoryParyStr = signature?.ChildNodes[0]?.GetRegexClass("cac:SignatoryParty");
                        var signatoryParty = XmlStringToXmlNode2(signatoryParyStr);


                        var paryIdentificationStr =
                            signatoryParty?.ChildNodes[0]?.GetRegexClass("cac:PartyIdentification");
                        var partyIdentification = XmlStringToXmlNode2(paryIdentificationStr);

                        var postalAddressStr = signatoryParty?.ChildNodes[0]?.GetRegexClass("cac:PostalAddress");
                        var postalAddress = XmlStringToXmlNode2(postalAddressStr);

                        var countryStr = postalAddress?.ChildNodes[0]?.GetRegexClass("cac:Country");
                        var country = XmlStringToXmlNode2(countryStr);

                        //cac:PartyTaxScheme
                        var partyTaxSchemeStr = signatoryParty?.ChildNodes[0]?.GetRegexClass("cac:PartyTaxScheme");
                        var partyTaxScheme = XmlStringToXmlNode2(partyTaxSchemeStr);

                        // AccountingSupplierParty //
                        var accountingSupplierPartyStr =
                            nodeContent.ChildNodes[0]?.GetRegexClass("cac:AccountingSupplierParty");
                        var accountingSupplierParty = XmlStringToXmlNode2(accountingSupplierPartyStr);

                        var paryAccountingStr = accountingSupplierParty?.ChildNodes[0]?.GetRegexClass("cac:Party");
                        var partyAccounting = XmlStringToXmlNode2(paryAccountingStr);

                        var partyIdentificationAccountingStr =
                            partyAccounting?.ChildNodes[0]?.GetRegexClass("cac:PartyIdentification");
                        var partyIdentificationAccounting = XmlStringToXmlNode2(partyIdentificationAccountingStr);

                        var partyNameAccountingStr = partyAccounting?.ChildNodes[0]?.GetRegexClass("cac:PartyName");
                        var partyNameAccounting = XmlStringToXmlNode2(partyNameAccountingStr);

                        var postalAddressAccountingStr =
                            partyAccounting?.ChildNodes[0]?.GetRegexClass("cac:PostalAddress");
                        var postalAddressAccounting = XmlStringToXmlNode2(postalAddressAccountingStr);

                        var countryAccountingStr = postalAddressAccounting?.ChildNodes[0]?.GetRegexClass("cac:Country");
                        var countryAccounting = XmlStringToXmlNode2(countryAccountingStr);

                        var partyTaxChemeAccountingStr =
                            partyAccounting?.ChildNodes[0]?.GetRegexClass("cac:PartyTaxScheme");
                        var partyTaxChemeAccounting = XmlStringToXmlNode2(partyTaxChemeAccountingStr);

                        var contactAccountingStr = partyAccounting?.ChildNodes[0]?.GetRegexClass("cac:Contact");
                        var contactAccounting = XmlStringToXmlNode2(contactAccountingStr);
                        // AccountingSupplierParty //

                        //AccountingCustomerParty

                        var accountingCustomerPartyStr =
                            nodeContent.ChildNodes[0]?.GetRegexClass("cac:AccountingCustomerParty");
                        var accountingCustomerParty = XmlStringToXmlNode2(accountingCustomerPartyStr);

                        var paryCustomerStr = accountingCustomerParty?.ChildNodes[0]?.GetRegexClass("cac:Party");
                        var partyCustomer = XmlStringToXmlNode2(paryCustomerStr);


                        var partyIdentificationCustomerStr =
                            partyCustomer?.ChildNodes[0]?.GetRegexClass("cac:PartyIdentification");
                        var partyIdentificationCustomer = XmlStringToXmlNode2(partyIdentificationCustomerStr);

                        var partyNameCustomerStr = partyCustomer?.ChildNodes[0]?.GetRegexClass("cac:PartyName");
                        var partyNameCustomer = XmlStringToXmlNode2(partyNameCustomerStr);

                        var postalAddressCustomerStr = partyCustomer?.ChildNodes[0]?.GetRegexClass("cac:PostalAddress");
                        var postalAddressCustomer = XmlStringToXmlNode2(postalAddressCustomerStr);

                        var countryCustomerStr = postalAddressCustomer?.ChildNodes[0]?.GetRegexClass("cac:Country");
                        var countryCustomer = XmlStringToXmlNode2(countryCustomerStr);

                        var partyTaxChemeCustomerStr =
                            partyCustomer?.ChildNodes[0]?.GetRegexClass("cac:PartyTaxScheme");
                        var partyTaxChemeCustomer = XmlStringToXmlNode2(partyTaxChemeCustomerStr);

                        var contactCustomerStr = partyCustomer?.ChildNodes[0]?.GetRegexClass("cac:Contact");
                        var contactCustomer = XmlStringToXmlNode2(contactCustomerStr);

                        //AccountingCustomerParty

                        // AllowanceCharge

                        var allowanceChargeStr = nodeContent.ChildNodes[0]?.GetClasses("cac:AllowanceCharge");

                        // AllowanceCharge

                        // TaxTotal
                        var taxTotalStr = nodeContent.ChildNodes[0]?.GetRegexClass("cac:TaxTotal");
                        var taxTotal = XmlStringToXmlNode2(taxTotalStr);

                        var taxSubTotalStr = taxTotal?.ChildNodes[0]?.GetRegexClass("cac:TaxSubtotal");
                        var taxSubTotal = XmlStringToXmlNode2(taxSubTotalStr);

                        var taxCategoryStr = taxSubTotal?.ChildNodes[0]?.GetRegexClass("cac:TaxCategory");
                        var taxCategory = XmlStringToXmlNode2(taxCategoryStr);

                        var taxShemeStr = taxCategory?.ChildNodes[0]?.GetRegexClass("cac:TaxScheme");
                        var taxSheme = XmlStringToXmlNode2(taxShemeStr);


                        var legalMonetaryTotalStr = nodeContent.ChildNodes[0].GetRegexClass("cac:LegalMonetaryTotal");
                        var legalMonetaryTotal = XmlStringToXmlNode2(legalMonetaryTotalStr);

                        //var invoiceLineStr = nodeContent.ChildNodes[0]?.GetClass("cac:InvoiceLine");
                        //var invoiceLine = XmlStringToXmlNode2(invoiceLineStr); // dizi olacak getClassess

                        var invoiceLinesStr = nodeContent.ChildNodes[0]?.GetRegexClasses("cac:InvoiceLine");


                        //List<string> notes = new List<string>();
                        //notes = invoiceLine?.ChildNodes[0]?.ChildNodes.GetValues("cbc:Note");


                        //var orderLineReferenceStr = invoiceLine?.ChildNodes[0]?.GetClass("cac:OrderLineReference");
                        //var orderLineReference = XmlStringToXmlNode2(orderLineReferenceStr);

                        //var taxTotalInvoiceLineStr = invoiceLine?.ChildNodes[0]?.GetClass("cac:TaxTotal");
                        //var taxTotalInvoiceLine = XmlStringToXmlNode2(taxTotalInvoiceLineStr);

                        //var taxSubTotalInvoiceLineStr = taxTotalInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxSubtotal");
                        //var taxSubTotalInvoiceLine = XmlStringToXmlNode2(taxSubTotalInvoiceLineStr);

                        //var taxCategoryInvoiceLineStr = taxSubTotalInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxCategory");
                        //var taxCategoryInvoiceLine = XmlStringToXmlNode2(taxCategoryInvoiceLineStr);

                        //var taxSchemeInvoiceLineStr = taxCategoryInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxScheme");
                        //var taxSchemeInvoiceLine = XmlStringToXmlNode2(taxSchemeInvoiceLineStr);

                        //// TaxTotal

                        //var itemStr = invoiceLine?.ChildNodes[0]?.GetClass("cac:Item");
                        //var item = XmlStringToXmlNode2(itemStr);

                        //var sellersItemIdentificationStr = item?.ChildNodes[0]?.GetClass("cac:SellersItemIdentification");
                        //var sellersItemIdentification = XmlStringToXmlNode2(sellersItemIdentificationStr);

                        //var originCountryStr = item?.ChildNodes[0]?.GetClass("cac:OriginCountry");
                        //var originCountry = XmlStringToXmlNode2(originCountryStr);

                        //var commodityClassificationStr = item?.ChildNodes[0]?.GetClassess("cac:CommodityClassification");

                        //var priceStr = invoiceLine?.ChildNodes[0]?.GetClass("cac:Price");
                        //var price = XmlStringToXmlNode2(priceStr);


                        Invoice invoice = new Invoice
                        {
                            Header = new Header
                            {
                                SENDER = nodeHeader?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("SENDER"), //res_.Data.GetElementsByTagName("SENDER")[0].InnerText,
                                RECEIVER = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("RECEIVER"),
                                SUPPLIER = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("SUPPLIER"),
                                CUSTOMER = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("CUSTOMER"),
                                ISSUE_DATE = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("ISSUE_DATE"),
                                PAYABLE_AMOUNT = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("PAYABLE_AMOUNT"),
                                FROM = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("FROM"),
                                TO = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("TO"),
                                PROFILEID = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("PROFILEID"),
                                INVOICE_TYPE_CODE =
                                    nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("INVOICE_TYPE_CODE"),
                                STATUS = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("STATUS"),
                                STATUS_DESCRIPTION =
                                    nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("STATUS_DESCRIPTION"),
                                GIB_STATUS_CODE = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("GIB_STATUS_CODE"),
                                GIB_STATUS_DESCRIPTION = nodeHeader?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("GIB_STATUS_DESCRIPTION"),
                                CDATE = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("CDATE"),
                                ENVELOPE_IDENTIFIER = nodeHeader?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("ENVELOPE_IDENTIFIER"),
                                STATUS_CODE = nodeHeader?.ChildNodes[0]?.ChildNodes?.GetValue("STATUS_CODE"),
                            },
                            CONTENT = new CONTENT
                            {
                                UBLExtensions = new List<UBLExtension>() { },
                                UBLVersionID = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:UBLVersionID"),
                                CustomizationID = nodeContent?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:CustomizationID"),
                                ProfileID = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ProfileID"),
                                ID = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                CopyIndicator = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:CopyIndicator"),
                                UUID = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:UUID"),
                                IssueDate = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:IssueDate"),
                                IssueTime = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:IssueTime"),
                                InvoiceTypeCode = nodeContent?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:InvoiceTypeCode"),
                                Note = nodeContent?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Note"),
                                DocumentCurrencyCode = nodeContent?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:DocumentCurrencyCode"),
                                LineCountNumeric = nodeContent?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:LineCountNumeric"),
                                AdditionalDocumentReference = new AdditionalDocumentReference
                                {
                                    ID = nodeAdditionalDocumentReference?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                    IssueDate = nodeAdditionalDocumentReference?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:IssueDate"),
                                    DocumentType = nodeAdditionalDocumentReference?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:DocumentType"),
                                    Attachment = new Attachment(),
                                },
                                Signature = new Signature
                                {
                                    ID = signature?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                    SignatoryParty = new SignatoryParty
                                    {
                                        PartyIdentification = new PartyIdentification
                                        {
                                            ID = partyIdentification?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID")
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            ID = postalAddress?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                            Room = postalAddress?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Room"),
                                            StreetName = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingNumber = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                            CitySubdivisionName = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            District = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:District"),
                                            BuildingName = postalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            Region = postalAddress?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = country?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                            }
                                        }
                                    }
                                },
                                AccountingSupplierParty = new AccountingSupplierParty
                                {
                                    Party = new Party
                                    {
                                        WebsiteURI = partyAccounting?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:WebsiteURI"),
                                        PartyIdentifications = new List<PartyIdentification>
                                        {
                                            new PartyIdentification
                                            { 
                                                ID = partyIdentificationAccounting?.ChildNodes[0]?.ChildNodes
                                                    .GetValue("cbc:ID"),
                                            }
                                        },
                                        PartyName = new PartyName
                                        {
                                            Name = partyNameAccounting?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            ID = postalAddressAccounting?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                            Room = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Room"),
                                            StreetName = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingNumber = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                            CitySubdivisionName = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            District = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:District"),
                                            BuildingName = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            Region = postalAddressAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = countryAccounting?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:Name"),
                                            }
                                        },
                                        PartyTaxSheme = new PartyTaxScheme
                                        {
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = partyTaxChemeAccounting?.ChildNodes[0]?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:Name"),
                                            }
                                        },
                                        Contact = new ContactInv
                                        {
                                            Telephone = contactAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telephone"),
                                            Telefax = contactAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telefax"),
                                            ElectronicMail = contactAccounting?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ElectronicMail"),
                                            Note = contactAccounting?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Note"),
                                        }
                                    }
                                },
                                AccountingCustomerParty = new AccountingCustomerParty
                                {
                                    Party = new Party
                                    {
                                        WebsiteURI = partyCustomer?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:WebsiteURI"),
                                        PartyIdentifications = new List<PartyIdentification>()
                                        {
                                            new PartyIdentification
                                            {
                                                ID = partyIdentificationCustomer?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:ID"),
                                            }
                                        },
                                        PartyName = new PartyName
                                        {
                                            Name = partyNameCustomer?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            ID = postalAddressCustomer?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                            Room = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Room"),
                                            StreetName = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingNumber = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                            CitySubdivisionName = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            District = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:District"),
                                            BuildingName = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            Region = postalAddressCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = countryCustomer?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                            }
                                        },
                                        PartyTaxSheme = new PartyTaxScheme
                                        {
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = partyTaxChemeCustomer?.ChildNodes[0]?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:Name"),
                                            }
                                        },
                                        Contact = new ContactInv
                                        {
                                            Telephone = contactCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telephone"),
                                            Telefax = contactCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telefax"),
                                            ElectronicMail = contactCustomer?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ElectronicMail"),
                                            Note = contactCustomer?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Note"),
                                            Name = contactCustomer?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name")
                                        }
                                    }
                                },
                                AllowanceCharges = new List<AllowanceCharge>(),
                                TaxTotal = new TaxTotal
                                {
                                    TaxAmount = taxTotal?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                    TaxSubtotal = new TaxSubtotal
                                    {
                                        TaxableAmount = taxSubTotal?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:TaxableAmount"),
                                        TaxAmount = taxSubTotal?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                        CalculationSequenceNumeric = taxSubTotal?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:CalculationSequenceNumeric"),
                                        Percent = taxSubTotal?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Percent"),
                                        TaxCategory = new TaxCategory
                                        {
                                            TaxExemptionReasonCode = taxCategory?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxExemptionReasonCode"),
                                            TaxExemptionReason = taxCategory?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxExemptionReason"),
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = taxSheme?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                                TaxTypeCode = taxSheme?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:TaxTypeCode"),
                                            }
                                        }
                                    }
                                },
                                LegalMonetaryTotal = new LegalMonetaryTotal
                                {
                                    LineExtensionAmount = legalMonetaryTotal?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:LineExtensionAmount"),
                                    TaxExclusiveAmount = legalMonetaryTotal?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:TaxExclusiveAmount"),
                                    TaxInclusiveAmount = legalMonetaryTotal?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:TaxInclusiveAmount"),
                                    PayableAmount = legalMonetaryTotal?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:PayableAmount"),
                                },
                                InvoiceLines = new List<InvoiceLine>()
                                //InvoiceLine = new InvoiceLine
                                //{
                                //    ID = invoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                //    Notes = new List<string>(),
                                //    InvoicedQuantity = invoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:InvoicedQuantity"),
                                //    LineExtensionAmount = invoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:LineExtensionAmount"),
                                //    OrderLineReference = new OrderLineReference
                                //    {
                                //        LineID = orderLineReference?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:LineID")
                                //    },
                                //    TaxTotal = new TaxTotal
                                //    {
                                //        TaxAmount = taxTotalInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                //        TaxSubtotal = new TaxSubtotal
                                //        {
                                //            TaxableAmount = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxableAmount"),
                                //            TaxAmount = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                //            CalculationSequenceNumeric = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:CalculationSequenceNumeric"),
                                //            Percent = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Percent"),
                                //            TaxCategory = new TaxCategory
                                //            {
                                //                TaxExemptionReasonCode = taxCategoryInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxExemptionReasonCode"),
                                //                TaxExemptionReason = taxCategoryInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxExemptionReason"),
                                //                TaxScheme = new TaxScheme
                                //                {
                                //                    Name = taxSchemeInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                //                    TaxTypeCode = taxSchemeInvoiceLine?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxTypeCode")
                                //                }
                                //            }
                                //        }
                                //    },
                                //    Item = new Item
                                //    {
                                //        Name = item?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                //        SellersItemIdentification = new SellersItemIdentification
                                //        {
                                //            ID = sellersItemIdentification?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                //        },
                                //        OriginCountry = new OriginCountry
                                //        {
                                //            IdentificationCode = originCountry?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:IdentificationCode"),
                                //            Name = originCountry?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                //        },
                                //        CommodityClassifications = new List<CommodityClassification> { }
                                //    },
                                //    Price = new Price
                                //    {
                                //        PriceAmount = price?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:PriceAmount"),
                                //    }
                                //}
                            }
                        };

                        for (int j = 0; j < nodeUBLExtensions.ChildNodes.Count; j++)
                        {
                            UBLExtension uBLExtension = new UBLExtension
                            {
                                ExtensionContent = new ExtensionContent
                                {
                                    SignatureContent = new SignatureContent
                                    {
                                        SignedInfo = new SignedInfo
                                        {
                                            CanonicalizationMethod = uBLSignedInfo?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("ds:CanonicalizationMethod"),
                                            SignatureMethod = uBLSignedInfo?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("ds:SignatureMethod"),
                                        }
                                    }
                                }
                            };
                            invoice.CONTENT.UBLExtensions.Add(uBLExtension);
                        }

                        for (int j = 0; j < allowanceChargeStr.Count; j++)
                        {
                            var allowanceCharge = XmlStringToXmlNode2(allowanceChargeStr[j]);
                            AllowanceCharge model = new AllowanceCharge
                            {
                                ChargeIndicator = allowanceCharge?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:ChargeIndicator"),
                                AllowanceChargeReason = allowanceCharge?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:AllowanceChargeReason"),
                                MultiplierFactorNumeric = allowanceCharge?.ChildNodes[0].ChildNodes
                                    ?.GetValue("cbc:MultiplierFactorNumeric"),
                                SequenceNumeric = allowanceCharge?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:SequenceNumeric"),
                                Amount = allowanceCharge?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Amount"),
                            };
                            invoice.CONTENT.AllowanceCharges.Add(model);
                        }

                        //if (notes != null)
                        //{
                        //    for (int j = 0; j < notes.Count; j++)
                        //    {
                        //        var note = XmlStringToXmlNode2(notes[j]);
                        //        //var strValue = node == null ? "" : node.ChildNodes[0]?.ChildNodes[0]?.Value;

                        //        if (note != null)
                        //        {
                        //            invoice.CONTENT.InvoiceLine.Notes.Add(note?.ChildNodes[0]?.ChildNodes[0]?.Value);
                        //        }

                        //    }
                        //}

                        //if (commodityClassificationStr != null)
                        //{
                        //    for (int j = 0; j < commodityClassificationStr.Count; j++)
                        //    {
                        //        var commodityClassification = XmlStringToXmlNode2(commodityClassificationStr[j]);
                        //        CommodityClassification model = new CommodityClassification
                        //        {
                        //            ItemClassificationCode = commodityClassification?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ItemClassificationCode")
                        //        };
                        //        invoice.CONTENT.InvoiceLine.Item.CommodityClassifications.Add(model);
                        //    }
                        //}

                        if (invoiceLinesStr != null)
                        {
                            for (int j = 0; j < invoiceLinesStr.Count; j++)
                            {
                                var invoiceLines = XmlStringToXmlNode2(invoiceLinesStr[j]);

                                List<string> notes = new List<string>();
                                notes = invoiceLines?.ChildNodes[0]?.ChildNodes.GetValues("cbc:Note");


                                var orderLineReferenceStr =
                                    invoiceLines?.ChildNodes[0]?.GetClass("cac:OrderLineReference");
                                var orderLineReference = XmlStringToXmlNode2(orderLineReferenceStr);

                                var taxTotalInvoiceLineStr = invoiceLines?.ChildNodes[0]?.GetClass("cac:TaxTotal");
                                var taxTotalInvoiceLine = XmlStringToXmlNode2(taxTotalInvoiceLineStr);

                                var taxSubTotalInvoiceLineStr =
                                    taxTotalInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxSubtotal");
                                var taxSubTotalInvoiceLine = XmlStringToXmlNode2(taxSubTotalInvoiceLineStr);

                                var taxCategoryInvoiceLineStr =
                                    taxSubTotalInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxCategory");
                                var taxCategoryInvoiceLine = XmlStringToXmlNode2(taxCategoryInvoiceLineStr);

                                var taxSchemeInvoiceLineStr =
                                    taxCategoryInvoiceLine?.ChildNodes[0]?.GetClass("cac:TaxScheme");
                                var taxSchemeInvoiceLine = XmlStringToXmlNode2(taxSchemeInvoiceLineStr);

                                // TaxTotal

                                var itemStr = invoiceLines?.ChildNodes[0]?.GetClass("cac:Item");
                                var item = XmlStringToXmlNode2(itemStr);

                                var sellersItemIdentificationStr =
                                    item?.ChildNodes[0]?.GetClass("cac:SellersItemIdentification");
                                var sellersItemIdentification = XmlStringToXmlNode2(sellersItemIdentificationStr);

                                var originCountryStr = item?.ChildNodes[0]?.GetClass("cac:OriginCountry");
                                var originCountry = XmlStringToXmlNode2(originCountryStr);

                                var commodityClassificationStr =
                                    item?.ChildNodes[0]?.GetClasses("cac:CommodityClassification");

                                var priceStr = invoiceLines?.ChildNodes[0]?.GetClass("cac:Price");
                                var price = XmlStringToXmlNode2(priceStr);

                                InvoiceLine invoiceLine = new InvoiceLine
                                {
                                    ID = invoiceLines?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                    Notes = new List<string>(),
                                    InvoicedQuantity = invoiceLines?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:InvoicedQuantity"),
                                    LineExtensionAmount = invoiceLines?.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:LineExtensionAmount"),
                                    OrderLineReference = new OrderLineReference
                                    {
                                        LineID = orderLineReference?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:LineID")
                                    },
                                    TaxTotal = new TaxTotal
                                    {
                                        TaxAmount = taxTotalInvoiceLine?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:TaxAmount"),
                                        TaxSubtotal = new TaxSubtotal
                                        {
                                            TaxableAmount = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxableAmount"),
                                            TaxAmount = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxAmount"),
                                            CalculationSequenceNumeric = taxSubTotalInvoiceLine?.ChildNodes[0]
                                                ?.ChildNodes?.GetValue("cbc:CalculationSequenceNumeric"),
                                            Percent = taxSubTotalInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Percent"),
                                            TaxCategory = new TaxCategory
                                            {
                                                TaxExemptionReasonCode = taxCategoryInvoiceLine?.ChildNodes[0]
                                                    ?.ChildNodes?.GetValue("cbc:TaxExemptionReasonCode"),
                                                TaxExemptionReason = taxCategoryInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:TaxExemptionReason"),
                                                TaxScheme = new TaxScheme
                                                {
                                                    Name = taxSchemeInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                        ?.GetValue("cbc:Name"),
                                                    TaxTypeCode = taxSchemeInvoiceLine?.ChildNodes[0]?.ChildNodes
                                                        ?.GetValue("cbc:TaxTypeCode")
                                                }
                                            }
                                        }
                                    },
                                    Item = new Item
                                    {
                                        Name = item?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                        SellersItemIdentification = new SellersItemIdentification
                                        {
                                            ID = sellersItemIdentification?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ID"),
                                        },
                                        OriginCountry = new OriginCountry
                                        {
                                            IdentificationCode = originCountry?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:IdentificationCode"),
                                            Name = originCountry?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                        },
                                        CommodityClassifications = new List<CommodityClassification> { }
                                    },
                                    Price = new Price
                                    {
                                        PriceAmount = price?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:PriceAmount"),
                                    }
                                };
                                if (notes != null)
                                {
                                    for (int k = 0; k < notes.Count; k++)
                                    {
                                        var note = XmlStringToXmlNode2(notes[k]);
                                        //var strValue = node == null ? "" : node.ChildNodes[0]?.ChildNodes[0]?.Value;

                                        if (note != null)
                                        {
                                            invoiceLine.Notes.Add(note?.ChildNodes[0]?.ChildNodes[0]?.Value);
                                        }
                                    }
                                }

                                if (commodityClassificationStr != null)
                                {
                                    for (int k = 0; k < commodityClassificationStr.Count; k++)
                                    {
                                        var commodityClassification =
                                            XmlStringToXmlNode2(commodityClassificationStr[k]);
                                        CommodityClassification model = new CommodityClassification
                                        {
                                            ItemClassificationCode = commodityClassification?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ItemClassificationCode")
                                        };
                                        invoiceLine.Item.CommodityClassifications.Add(model);
                                    }
                                }

                                invoice.CONTENT.InvoiceLines.Add(invoiceLine);
                            }
                        }

                        getInvoiceResponse.Invoices.Add(invoice);
                    }

                    return new SuccessDataResult<GetInvoiceResponse>(getInvoiceResponse);
                }
                catch (Exception ex)
                {
                    var getresponseXmlConvertString = string.Empty;
                    if (res_?.Success == true)
                    {
                        getresponseXmlConvertString = res_.Data.InnerText;
                    }


                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetInvoiceResponse>(null, ex.Message);
            }
            //throw new NotImplementedException();
        }

        public IDataResult<MarkInvoiceResponse> MarkInvoice(string sessionId, List<InvoiceMark> invoices)
        {
            try
            {
                var res = _iInvoiceDal.GetMarkInvoice(sessionId, invoices);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RMarkInvoiceRequest> template = new MarkInvoiceXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                MarkInvoiceResponse markInvoiceResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EFaturaOIB", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }


                    markInvoiceResponse = new MarkInvoiceResponse
                    {
                        REQUEST_RETURN = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = node.ChildNodes.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = node.ChildNodes.GetValue("RETURN_CODE")
                        }
                    };

                    return new SuccessDataResult<MarkInvoiceResponse>(markInvoiceResponse);
                }
                catch (Exception ex)
                {
                    var getresponseXmlConvertString = string.Empty;
                    if (res_?.Success == true)
                    {
                        getresponseXmlConvertString = res_.Data.InnerText;
                    }


                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<MarkInvoiceResponse>(null, ex.Message);
            }
        }

        public IDataResult<SendInvoiceResponseWithServerSignResponse> SendInvoiceResponseWithServerSign(
            string sessionId, INVOICE invoice, bool status)
        {
            try
            {
                var res = _iInvoiceDal.GetSendInvoiceResponseWithServerSign(sessionId, invoice, status);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RSendInvoiceResponseWithServerSignRequest> template =
                    new SendInvoiceResponseWithServerSignRequestXML();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                SendInvoiceResponseWithServerSignResponse sendInvoiceResponseWithServerSignResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EFaturaOIB", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    sendInvoiceResponseWithServerSignResponse = new SendInvoiceResponseWithServerSignResponse
                    {
                        INTL_TXN_ID = node.ChildNodes.GetValue("INTL_TXN_ID"),
                        CLIENT_TXN_ID = node.ChildNodes.GetValue("CLIENT_TXN_ID"),
                        RETURN_CODE = node.ChildNodes.GetValue("RETURN_CODE"),
                        ERROR_CODE = node.ChildNodes.GetValue("ERROR_CODE"),
                        ERROR_SHORT_DES = node.ChildNodes.GetValue("ERROR_SHORT_DES"),
                        ERROR_LONG_DES = node.ChildNodes.GetValue("ERROR_LONG_DES")
                    };
                    return new SuccessDataResult<SendInvoiceResponseWithServerSignResponse>(
                        sendInvoiceResponseWithServerSignResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<SendInvoiceResponseWithServerSignResponse>(null, ex.Message);
            }
        }

        public IDataResult<GetInvoiceStatusResponse> GetInvoiceStatus(string sessionId, INVOICE invoice)
        {
            try
            {
                var res = _iInvoiceDal.GetInvoiceStatus(sessionId, invoice);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RGetInvoiceStatusRequest> template = new GetInvoiceStatusXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                GetInvoiceStatusResponse getInvoiceStatusResponse;
                ;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EFaturaOIB", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    getInvoiceStatusResponse = new GetInvoiceStatusResponse
                    {
                        STATUS = node.ChildNodes.GetValue("STATUS"),
                        STATUS_DESCRIPTION = node.ChildNodes.GetValue("STATUS_DESCRIPTION"),
                        GIB_STATUS_CODE = node.ChildNodes.GetValue("GIB_STATUS_CODE"),
                        GIB_STATUS_DESCRIPTION = node.ChildNodes.GetValue("GIB_STATUS_DESCRIPTION"),
                        CDATE = node.ChildNodes.GetValue("CDATE"),
                        ENVELOPE_IDENTIFIER = node.ChildNodes.GetValue("ENVELOPE_IDENTIFIER"),
                        STATUS_CODE = node.ChildNodes.GetValue("STATUS_CODE"),
                        DIRECTION = node.ChildNodes.GetValue("DIRECTION"),
                    };
                    return new SuccessDataResult<GetInvoiceStatusResponse>(getInvoiceStatusResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<GetInvoiceStatusResponse>(null, ex.Message);
            }
        }

        public IDataResult<GetGibUserListResponse> GetGibUserList(string sessionId)
        {
            try
            {
                var res = _iInvoiceDal.GetGibUserListRequest(sessionId);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RGetGibUserListRequest> template = new GetGibUserListRequestXml();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                GetGibUserListResponse getGibUserListResponse;
                ;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/AuthenticationWS", xml, "POST",
                        header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    getGibUserListResponse = new GetGibUserListResponse
                    {
                        CONTENT = node.ChildNodes.GetValue("CONTENT"),
                        ErrorType = new ERROR_TYPE
                        {
                            INTL_TXN_ID = node.ChildNodes[0].ChildNodes.GetValue("INTL_TXN_ID"),
                            ERROR_CODE = node.ChildNodes[0].ChildNodes.GetValue("ERROR_CODE"),
                            ERROR_SHORT_DES = node.ChildNodes[0].ChildNodes.GetValue("ERROR_SHORT_DES"),
                        }
                    };
                    return new SuccessDataResult<GetGibUserListResponse>(getGibUserListResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<GetGibUserListResponse>(null, ex.Message);
            }
        }

        #endregion

        #region E-ARCHIVE

        public IDataResult<WriteToArchieveExtendedResponse> WriteToArchieveExtended(string sessionId)
        {
            try
            {
                var res = _iInvoiceDal.GetWriteToArchieveExtendedRequest(sessionId);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RWriteToArchieveExtendedRequest> template = new WriteToArchieveExtendedRequestXML();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                WriteToArchieveExtendedResponse rWriteToArchieveExtendedResponse;
                ;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1]?.ChildNodes[0]?.ChildNodes[0];
                    var requestReturnStr = node.GetClass("REQUEST_RETURN");
                    var requestReturn = XmlStringToXmlNode2(requestReturnStr);

                    var errorTypeStr = node.GetClass("ERROR_TYPE");
                    var errorType = XmlStringToXmlNode2(errorTypeStr);
                    //var uBLExtensionStr = nodeUBLExtensions.ChildNodes[0].GetClass("ext:UBLExtension");
                    //var uBLExtensionNode = XmlStringToXmlNode2(uBLExtensionStr);

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    rWriteToArchieveExtendedResponse = new()
                    {
                        REQUEST_RETURN = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = requestReturn?.ChildNodes[0]?.ChildNodes.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = requestReturn?.ChildNodes[0]?.ChildNodes.GetValue("RETURN_CODE")
                        },
                        INVOICE_ID = node.ChildNodes.GetValue("INVOICE_ID"),
                        ErrorType = new ERROR_TYPE
                        {
                            INTL_TXN_ID = errorType?.ChildNodes[0]?.ChildNodes.GetValue("INTL_TXN_ID"),
                            ERROR_CODE = errorType?.ChildNodes[0]?.ChildNodes.GetValue("ERROR_CODE"),
                            ERROR_SHORT_DES = errorType?.ChildNodes[0]?.ChildNodes.GetValue("ERROR_SHORT_DES"),
                        }
                    };

                    return new SuccessDataResult<WriteToArchieveExtendedResponse>(rWriteToArchieveExtendedResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<WriteToArchieveExtendedResponse>(null, ex.Message);
            }
        }

        public IDataResult<ReadFromArchiveResponse> ReadFromArchive(string sessionId, INVOICE Invoice)
        {
            try
            {
                var res = _iInvoiceDal.GetReadFromArchive(sessionId, Invoice.UUID);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RReadFromArchiveRequest> template = new ReadFromArchiveRequestXML();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                ReadFromArchiveResponse readFromArchiveResponse;
                ;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1]?.ChildNodes[0]?.ChildNodes[0];
                    var invoiceBase64 = node.ChildNodes.GetValue("INVOICE");
                    var invoiceByte = Convert.FromBase64String(invoiceBase64);
                    var invoiceString = Encoding.UTF8.GetString(invoiceByte);
                    var invoice = XmlStringToXmlNode(invoiceString);

                    var RequestReturnStr = node.GetClass("REQUEST_RETURN");
                    var RequestReturn = XmlStringToXmlNode2(RequestReturnStr);

                    if (node == null)
                    {
                        throw new Exception(Messages.CantGetInformationFromSOAP);
                    }

                    var DespatchDocumentReferenceStr = invoice.ChildNodes[1]?.GetClass("cac:DespatchDocumentReference");
                    var DespatchDocumentReference = XmlStringToXmlNode2(DespatchDocumentReferenceStr);

                    var AdditionalDocumentReferences =
                        invoice.ChildNodes[1]?.GetClasses("cac:AdditionalDocumentReference");

                    var SignatureStr = invoice.ChildNodes[1]?.GetClass("cac:Signature");
                    var Signature = XmlStringToXmlNode2(SignatureStr);

                    var SignatoryPartyStr = Signature.ChildNodes[0]?.GetClass("cac:SignatoryParty");
                    var SignatoryParty = XmlStringToXmlNode2(SignatoryPartyStr);

                    var SignatoryPartyIdentificationStr =
                        SignatoryParty?.ChildNodes[0]?.GetClass("cac:PartyIdentification");
                    var SignatoryPartyIdentification = XmlStringToXmlNode2(SignatoryPartyIdentificationStr);

                    var SignatoryPartyPostalAddressStr = SignatoryParty?.ChildNodes[0]?.GetClass("cac:PostalAddress");
                    var SignatoryPartyPostalAddress = XmlStringToXmlNode2(SignatoryPartyPostalAddressStr);

                    var SignatoryPartyCountryStr = SignatoryPartyPostalAddress?.ChildNodes[0]?.GetClass("cac:Country");
                    var SignatoryPartyCountry = XmlStringToXmlNode2(SignatoryPartyCountryStr);

                    var DigitalSignatureAttachmentStr =
                        Signature.ChildNodes[0]?.GetClass("cac:DigitalSignatureAttachment");
                    var DigitalSignatureAttachment = XmlStringToXmlNode2(DigitalSignatureAttachmentStr);

                    var ExternalReferenceStr =
                        DigitalSignatureAttachment.ChildNodes[0]?.GetClass("cac:ExternalReference");
                    var ExternalReference = XmlStringToXmlNode2(ExternalReferenceStr);

                    var AccountingSupplierPartyStr = invoice.ChildNodes[1]?.GetClass("cac:AccountingSupplierParty");
                    var AccountingSupplierParty = XmlStringToXmlNode2(AccountingSupplierPartyStr);

                    var SupplierPartyStr = AccountingSupplierParty.ChildNodes[0]?.GetClass("cac:Party");
                    var SupplierParty = XmlStringToXmlNode2(SupplierPartyStr);

                    var SupplierPartyIdentificationLst =
                        SupplierParty.ChildNodes[0].GetClasses("cac:PartyIdentification");

                    var SupplierPartyNameStr = SupplierParty.ChildNodes[0]?.GetClass("cac:PartyName");
                    var SupplierPartyName = XmlStringToXmlNode2(SupplierPartyNameStr);

                    var SupplierPostalAddressStr = SupplierParty.ChildNodes[0]?.GetClass("cac:PostalAddress");
                    var SupplierPostalAddress = XmlStringToXmlNode2(SupplierPostalAddressStr);

                    var SupplierCountryStr = SupplierPostalAddress.ChildNodes[0].GetClass("cac:Country");
                    var SupplierCountry = XmlStringToXmlNode2(SupplierCountryStr);

                    var SupplierPartyTaxSchemeStr = SupplierParty.ChildNodes[0]?.GetClass("cac:PartyTaxScheme");
                    var SupplierPartyTaxScheme = XmlStringToXmlNode2(SupplierPartyTaxSchemeStr);

                    var SupplierTaxSchemeStr = SupplierPartyTaxScheme.ChildNodes[0]?.GetClass("cac:TaxScheme");
                    var SupplierTaxScheme = XmlStringToXmlNode2(SupplierTaxSchemeStr);

                    var SupplierContactStr = SupplierParty.ChildNodes[0]?.GetClass("cac:Contact");
                    var SupplierContact = XmlStringToXmlNode2(SupplierContactStr);

                    var AccountingCustomerPartyStr = invoice.ChildNodes[1]?.GetClass("cac:AccountingCustomerParty");
                    var AccountingCustomerParty = XmlStringToXmlNode2(AccountingCustomerPartyStr);

                    var CustomerPartyStr = AccountingCustomerParty.ChildNodes[0]?.GetClass("cac:Party");
                    var CustomerParty = XmlStringToXmlNode2(CustomerPartyStr);

                    var CustomerPartyIdentificationLst =
                        CustomerParty.ChildNodes[0].GetClasses("cac:PartyIdentification");

                    var CustomerPartyNameStr = CustomerParty.ChildNodes[0].GetClass("cac:PartyName");
                    var CustomerPartyName = XmlStringToXmlNode2(CustomerPartyNameStr);

                    var CustomerPostalAddressStr = CustomerParty.ChildNodes[0].GetClass("cac:PostalAddress");
                    var CustomerPostalAddress = XmlStringToXmlNode2(CustomerPostalAddressStr);

                    var CustomerCountryStr = CustomerPostalAddress.ChildNodes[0].GetClass("cac:Country");
                    var CustomerCountry = XmlStringToXmlNode2(CustomerCountryStr);

                    var CustomerPartyTaxShemeStr = CustomerParty.ChildNodes[0].GetClass("cac:PartyTaxScheme");
                    var CustomerPartyTaxSheme = XmlStringToXmlNode2(CustomerPartyTaxShemeStr);

                    var CustomerTaxShemeStr = CustomerPartyTaxSheme.ChildNodes[0]?.GetClass("cac:TaxScheme");
                    var CustomerTaxSheme = XmlStringToXmlNode2(CustomerTaxShemeStr);

                    var AllowanceChargeLst = invoice.ChildNodes[1].GetClasses("cac:AllowanceCharge");

                    var PricingExchangeRateStr = invoice.ChildNodes[1].GetClass("cac:PricingExchangeRate");
                    var PricingExchangeRate = XmlStringToXmlNode2(PricingExchangeRateStr);

                    var TaxTotalStr = invoice.ChildNodes[1].GetClass("cac:TaxTotal");
                    var TaxTotal = XmlStringToXmlNode2(TaxTotalStr);

                    var TaxSubtotalStr = TaxTotal.ChildNodes[0].GetClass("cac:TaxSubtotal");
                    var TaxSubtotal = XmlStringToXmlNode2(TaxSubtotalStr);

                    var TaxCategoryStr = TaxSubtotal.ChildNodes[0].GetClass("cac:TaxCategory");
                    var TaxCategory = XmlStringToXmlNode2(TaxCategoryStr);

                    var TaxSchemeStr = TaxCategory.ChildNodes[0].GetClass("cac:TaxScheme");
                    var TaxScheme = XmlStringToXmlNode2(TaxSchemeStr);

                    var LegalMonetaryTotalStr = invoice.ChildNodes[1].GetClass("cac:LegalMonetaryTotal");
                    var LegalMonetaryTotal = XmlStringToXmlNode2(LegalMonetaryTotalStr);

                    var InvoiceLineStr = invoice.ChildNodes[1].GetClass("cac:InvoiceLine");
                    var InvoiceLine = XmlStringToXmlNode2(InvoiceLineStr);

                    var noteLst = InvoiceLine.ChildNodes[0].ChildNodes.GetValues("cbc:Note");

                    var InvoTaxTotalStr = InvoiceLine.ChildNodes[0].GetClass("cac:TaxTotal");
                    var InvoTaxTotal = XmlStringToXmlNode2(InvoTaxTotalStr);

                    var InvoTaxSubTotalStr = InvoTaxTotal.ChildNodes[0].GetClass("cac:TaxSubtotal");
                    var InvoTaxSubtotal = XmlStringToXmlNode2(InvoTaxSubTotalStr);

                    var InvoTaxCategoryStr = InvoTaxSubtotal.ChildNodes[0].GetClass("cac:TaxCategory");
                    var InvoTaxCategory = XmlStringToXmlNode2(InvoTaxCategoryStr);

                    var InvoTaxSchemeStr = InvoTaxCategory.ChildNodes[0].GetClass("cac:TaxScheme");
                    var InvoTaxScheme = XmlStringToXmlNode2(InvoTaxSchemeStr);

                    var ItemStr = InvoiceLine.ChildNodes[0].GetClass("cac:Item");
                    var Item = XmlStringToXmlNode2(ItemStr);

                    var PriceStr = InvoiceLine.ChildNodes[0].GetClass("cac:Price");
                    var Price = XmlStringToXmlNode2(PriceStr);

                    readFromArchiveResponse = new ReadFromArchiveResponse
                    {
                        Invoice = new INVOICE
                        {
                            ID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:ID"),
                            UUID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:UUID"),
                            CONTENT = new CONTENT
                            {
                                UBLVersionID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:UBLVersionID"),
                                CustomizationID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:CustomizationID"),
                                ProfileID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:ProfileID"),
                                ID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:ID"),
                                CopyIndicator = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:CopyIndicator"),
                                UUID = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:UUID"),
                                IssueDate = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:IssueDate"),
                                IssueTime = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:IssueTime"),
                                InvoiceTypeCode = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:InvoiceTypeCode"),
                                DocumentCurrencyCode = invoice.ChildNodes[1]?.ChildNodes
                                    ?.GetValue("cbc:DocumentCurrencyCode"),
                                LineCountNumeric = invoice.ChildNodes[1]?.ChildNodes?.GetValue("cbc:LineCountNumeric"),
                                DespatchDocumentReference = new DespatchDocumentReference
                                {
                                    ID = DespatchDocumentReference.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                    IssueDate = DespatchDocumentReference.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:IssueDate")
                                },
                                Signature = new Signature
                                {
                                    ID = Signature.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                                    SignatoryParty = new SignatoryParty
                                    {
                                        PartyIdentification = new PartyIdentification
                                        {
                                            ID = SignatoryPartyIdentification.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ID")
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            StreetName = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingName = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            BuildingNumber = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                            CitySubdivisionName = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            Region = SignatoryPartyPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = SignatoryPartyCountry.ChildNodes[0].ChildNodes
                                                    ?.GetValue("cbc:Name")
                                            }
                                        }
                                    },
                                    DigitalSignatureAttachment = new DigitalSignatureAttachment
                                    {
                                        ExternalReference = new ExternalReference
                                        {
                                            URI = ExternalReference.ChildNodes[0]?.ChildNodes?.GetValue("cbc:URI")
                                        }
                                    }
                                },
                                AccountingSupplierParty = new AccountingSupplierParty
                                {
                                    Party = new Party
                                    {
                                        WebsiteURI = SupplierParty?.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:WebsiteURI"),
                                        PartyName = new PartyName
                                        {
                                            Name = SupplierPartyName?.ChildNodes[0]?.ChildNodes.GetValue("cbc:Name")
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            StreetName = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingName = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            BuildingNumber = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                            CitySubdivisionName = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            Region = SupplierPostalAddress?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = SupplierCountry?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name")
                                            }
                                        },
                                        PartyTaxSheme = new PartyTaxScheme
                                        {
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = SupplierTaxScheme?.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:Name"),
                                            }
                                        },
                                        Contact = new ContactInv
                                        {
                                            Telephone = SupplierContact?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telephone"),
                                            ElectronicMail = SupplierContact?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:ElectronicMail"),
                                            Telefax = SupplierContact?.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Telefax"),
                                            Note = SupplierContact?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Note"),
                                            Name = SupplierContact?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                        }
                                    }
                                },
                                AccountingCustomerParty = new AccountingCustomerParty
                                {
                                    Party = new Party
                                    {
                                        PartyName = new PartyName
                                        {
                                            Name = CustomerPartyName.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name")
                                        },
                                        PostalAddress = new PostalAddress
                                        {
                                            StreetName = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:StreetName"),
                                            BuildingName = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingName"),
                                            CitySubdivisionName = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CitySubdivisionName"),
                                            CityName = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:CityName"),
                                            PostalZone = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:PostalZone"),
                                            Region = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:Region"),
                                            Country = new Country
                                            {
                                                Name = CustomerCountry.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name")
                                            },
                                            BuildingNumber = CustomerPostalAddress.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:BuildingNumber"),
                                        },
                                        PartyTaxSheme = new PartyTaxScheme
                                        {
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = CustomerTaxSheme.ChildNodes[0]?.ChildNodes.GetValue("cbc:Name")
                                            }
                                        }
                                    }
                                },
                                PricingExchangeRate = new PricingExchangeRate
                                {
                                    CalculationRate = PricingExchangeRate.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:CalculationRate"),
                                    SourceCurrencyCode = PricingExchangeRate.ChildNodes[0].ChildNodes
                                        ?.GetValue("cbc:SourceCurrencyCode"),
                                    Date = PricingExchangeRate.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Date"),
                                    TargetCurrencyCode = PricingExchangeRate.ChildNodes[0]?.ChildNodes
                                        .GetValue("cbc:TargetCurrencyCode")
                                },
                                TaxTotal = new TaxTotal
                                {
                                    TaxAmount = TaxTotal.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                    TaxSubtotal = new TaxSubtotal
                                    {
                                        TaxableAmount = TaxSubtotal.ChildNodes[0]?.ChildNodes
                                            ?.GetValue("cbc:TaxableAmount"),
                                        TaxAmount = TaxSubtotal.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                        Percent = TaxSubtotal.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Percent"),
                                        TaxCategory = new TaxCategory
                                        {
                                            TaxScheme = new TaxScheme
                                            {
                                                Name = TaxScheme.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                                TaxTypeCode = TaxScheme.ChildNodes[0]?.ChildNodes
                                                    ?.GetValue("cbc:TaxTypeCode"),
                                            }
                                        }
                                    }
                                },
                                LegalMonetaryTotal = new LegalMonetaryTotal
                                {
                                    LineExtensionAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:LineExtensionAmount"),
                                    TaxExclusiveAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:TaxExclusiveAmount"),
                                    TaxInclusiveAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:AllowanceTotalAmount"),
                                    AllowanceTotalAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:LineExtensionAmount"),
                                    PayableAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:PayableAmount"),
                                    ChargeTotalAmount = LegalMonetaryTotal.ChildNodes[0]?.ChildNodes
                                        ?.GetValue("cbc:ChargeTotalAmount")
                                },
                                InvoiceLine = new InvoiceLine
                                {
                                    ID = InvoiceLine.ChildNodes[0]?.ChildNodes.GetValue("cbc:ID"),
                                    InvoicedQuantity = InvoiceLine.ChildNodes[0]?.ChildNodes
                                        .GetValue("cbc:InvoicedQuantity"),
                                    LineExtensionAmount = InvoiceLine.ChildNodes[0]?.ChildNodes
                                        .GetValue("cbc:LineExtensionAmount"),
                                    TaxTotal = new TaxTotal
                                    {
                                        TaxAmount = InvoTaxTotal.ChildNodes[0]?.ChildNodes?.GetValue("cbc:TaxAmount"),
                                        TaxSubtotal = new TaxSubtotal
                                        {
                                            TaxableAmount = InvoTaxSubtotal.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxableAmount"),
                                            TaxAmount = InvoTaxSubtotal.ChildNodes[0]?.ChildNodes
                                                ?.GetValue("cbc:TaxAmount"),
                                            Percent =
                                                InvoTaxSubtotal.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Percent"),
                                            TaxCategory = new TaxCategory
                                            {
                                                TaxScheme = new TaxScheme
                                                {
                                                    TaxTypeCode = InvoTaxScheme.ChildNodes[0]?.ChildNodes
                                                        ?.GetValue("cbc:TaxTypeCode")
                                                }
                                            }
                                        }
                                    },
                                    Item = new Item
                                    {
                                        Description = Item.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Description"),
                                        Name = Item.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Name"),
                                    },
                                    Price = new Price
                                    {
                                        PriceAmount = Price.ChildNodes[0]?.ChildNodes?.GetValue("cbc:PriceAmount")
                                    }
                                }
                            }
                        },
                        RequestReturn = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = RequestReturn.ChildNodes[0]?.ChildNodes.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = RequestReturn.ChildNodes[0]?.ChildNodes.GetValue("RETURN_CODE"),
                        },
                    };

                    readFromArchiveResponse.Invoice.CONTENT.AdditionalDocumentReferences =
                        new List<AdditionalDocumentReference>();

                    for (int i = 0; i < AdditionalDocumentReferences.Count; i++)
                    {
                        var AdditionalDocument = XmlStringToXmlNode2(AdditionalDocumentReferences[i]);
                        var AttachmentStr = AdditionalDocument.ChildNodes[0]?.GetClass("cac:Attachment");
                        var Attachment = XmlStringToXmlNode2(AttachmentStr);

                        var AdditionalDocumentReference = new AdditionalDocumentReference
                        {
                            ID = AdditionalDocument?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:ID"),
                            IssueDate = AdditionalDocument?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:IssueDate"),
                            DocumentType = AdditionalDocument?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:DocumentType"),
                            Attachment = new Attachment
                            {
                                EmbeddedDocumentBinaryObject = Attachment?.ChildNodes[0]?.ChildNodes
                                    ?.GetValue("cbc:EmbeddedDocumentBinaryObject")
                            },
                        };
                        readFromArchiveResponse.Invoice.CONTENT.AdditionalDocumentReferences.Add(
                            AdditionalDocumentReference);
                    }

                    readFromArchiveResponse.Invoice.CONTENT.AccountingSupplierParty.Party.PartyIdentifications =
                        new List<PartyIdentification>();

                    for (int i = 0; i < SupplierPartyIdentificationLst.Count; i++)
                    {
                        var SupplierPartyIdentification = XmlStringToXmlNode2(SupplierPartyIdentificationLst[i]);
                        var PartyIdentification = new PartyIdentification
                        {
                            ID = SupplierPartyIdentification?.ChildNodes[0].ChildNodes.GetValue("cbc:ID")
                        };
                        readFromArchiveResponse.Invoice.CONTENT.AccountingSupplierParty.Party.PartyIdentifications.Add(
                            PartyIdentification);
                    }


                    readFromArchiveResponse.Invoice.CONTENT.AccountingCustomerParty.Party.PartyIdentifications =
                        new List<PartyIdentification>();
                    for (int i = 0; i < CustomerPartyIdentificationLst.Count; i++)
                    {
                        var CustomerPartyIdentification = XmlStringToXmlNode2(CustomerPartyIdentificationLst[i]);
                        var PartyIdentification = new PartyIdentification
                        {
                            ID = CustomerPartyIdentification?.ChildNodes[0].ChildNodes.GetValue("cbc:ID")
                        };
                        readFromArchiveResponse.Invoice.CONTENT.AccountingCustomerParty.Party.PartyIdentifications.Add(
                            PartyIdentification);
                    }

                    readFromArchiveResponse.Invoice.CONTENT.AllowanceCharges = new List<AllowanceCharge>();

                    for (int i = 0; i < AllowanceChargeLst.Count; i++)
                    {
                        var AllowanceChargeNode = XmlStringToXmlNode2(AllowanceChargeLst[i]);
                        var AllowancaCharge = new AllowanceCharge
                        {
                            ChargeIndicator = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes
                                ?.GetValue("cbc:ChargeIndicator"),
                            Amount = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Amount"),
                            AllowanceChargeReason = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes
                                ?.GetValue("cbc:AllowanceChargeReason"),
                            BaseAmount = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes
                                ?.GetValue("cbc:AllowanceChargeReason"),
                            MultiplierFactorNumeric = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes
                                ?.GetValue("cbc:BaseAmount"),
                            SequenceNumeric = AllowanceChargeNode?.ChildNodes[0]?.ChildNodes
                                ?.GetValue("cbc:SequenceNumeric"),
                        };
                        readFromArchiveResponse.Invoice.CONTENT.AllowanceCharges.Add(AllowancaCharge);
                    }

                    readFromArchiveResponse.Invoice.CONTENT.InvoiceLine.Notes = new List<string>();
                    for (int i = 0; i < noteLst.Count; i++)
                    {
                        var note = InvoiceLine.ChildNodes[0]?.ChildNodes?.GetValue("cbc:Note");
                        readFromArchiveResponse.Invoice.CONTENT.InvoiceLine.Notes.Add(note);
                    }


                    return new SuccessDataResult<ReadFromArchiveResponse>(readFromArchiveResponse);
                }

                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<ReadFromArchiveResponse>(null, ex.Message);
            }
        }

        public IDataResult<CancelEArchiveInvoiceResponse> CancelEArchiveInvoice(string sessionId, string uuid)
        {
            try
            {
                var res = _iInvoiceDal.GetCancelEArchiveInvoice(sessionId, uuid);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RCancelEArchiveInvoiceRequest> template = new CancelEArchiveInvoiceXML();

                var xmlXElement = template.Run(res.Data);

                var xml = xmlXElement.ObjectToSoapXml();

                CancelEArchiveInvoiceResponse cancelEArchiveInvoiceResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_.Data == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];

                    var ErrorTypeStr = node.GetClass("ERROR_TYPE");
                    var ErrorType = XmlStringToXmlNode2(ErrorTypeStr);

                    var RequestReturnStr = node.GetClass("REQUEST_RETURN");
                    var RequestReturn = XmlStringToXmlNode2(RequestReturnStr);

                    cancelEArchiveInvoiceResponse = new()
                    {
                        ERROR_TYPE = new ERROR_TYPE
                        {
                            INTL_TXN_ID = ErrorType?.ChildNodes[0]?.ChildNodes?.GetValue("INTL_TXN_ID"),
                            ERROR_CODE = ErrorType?.ChildNodes[0]?.ChildNodes?.GetValue("ERROR_CODE"),
                            ERROR_SHORT_DES = ErrorType?.ChildNodes[0]?.ChildNodes?.GetValue("ERROR_SHORT_DES"),
                        },
                        Request_Return = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = RequestReturn?.ChildNodes[0]?.ChildNodes?.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = RequestReturn?.ChildNodes[0]?.ChildNodes?.GetValue("RETURN_CODE"),
                        }
                    };

                    return new SuccessDataResult<CancelEArchiveInvoiceResponse>(cancelEArchiveInvoiceResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new SuccessDataResult<CancelEArchiveInvoiceResponse>(null, ex.Message);
            }
        }

        public IDataResult<GetEArchiveInvoiceStatusResponse> EArchiveInvoiceStatus(string sessionId, string uuid)
        {
            try
            {
                var res = _iInvoiceDal.GetEArchiveInvoiceStatus(sessionId, uuid);

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                if (res.Data == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                ITemplate<RGetEArchiveInvoiceStatusRequest> template = new GetEArchiveInvoiceStatusXML();
                var xmlElement = template.Run(res.Data);
                var xml = xmlElement.ObjectToSoapXml();

                GetEArchiveInvoiceStatusResponse getEArchiveInvoiceStatusResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");
                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_.Data == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res_.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0]?.ChildNodes[0]?.ChildNodes[0];
                    var HeaderStr = node.GetClass("HEADER");
                    var Header = XmlStringToXmlNode2(HeaderStr);

                    getEArchiveInvoiceStatusResponse = new()
                    {
                        INVOICE_ID = Header?.ChildNodes[0]?.ChildNodes?.GetValue("INVOICE_ID"),
                        PROFILE = Header?.ChildNodes[0]?.ChildNodes?.GetValue("PROFILE"),
                        UUID = Header?.ChildNodes[0]?.ChildNodes?.GetValue("UUID"),
                        INVOICE_DATE = Header?.ChildNodes[0]?.ChildNodes?.GetValue("INVOICE_DATE"),
                        STATUS = Header?.ChildNodes[0]?.ChildNodes?.GetValue("STATUS"),
                        STATUS_DESC = Header?.ChildNodes[0]?.ChildNodes?.GetValue("STATUS_DESC"),
                        EMAIL_STATUS = Header?.ChildNodes[0]?.ChildNodes?.GetValue("EMAIL_STATUS"),
                        EMAIL_STATUS_DESC = Header?.ChildNodes[0]?.ChildNodes?.GetValue("EMAIL_STATUS_DESC"),
                        REPORT_ID = Header?.ChildNodes[0]?.ChildNodes?.GetValue("REPORT_ID"),
                        WEB_KEY = Header?.ChildNodes[0]?.ChildNodes?.GetValue("WEB_KEY"),
                    };

                    return new SuccessDataResult<GetEArchiveInvoiceStatusResponse>(getEArchiveInvoiceStatusResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetEArchiveInvoiceStatusResponse>(null, ex.Message);
            }
        }

        public IDataResult<GetEArchiveReportResponse> EArchiveReport(string sessionId, string reportPeriod,
            string reportStatus = "Y")
        {
            try
            {
                var res = _iInvoiceDal.GetEArchiveReportRequest(sessionId, reportPeriod, reportStatus);

                if (res == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                ITemplate<RGetEArchiveReportRequest> template = new EArchiveReportXML();
                var xmlElement = template.Run(res.Data);
                var xml = xmlElement.ObjectToSoapXml();

                GetEArchiveReportResponse getEArchiveReportResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (!res_.Success)
                    {
                        throw new Exception(res_.Message);
                    }

                    if (res_.Data == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];

                    var reportStr = node.GetClass("REPORT");
                    var report = XmlStringToXmlNode2(reportStr);

                    var REQUEST_RETURNStr = node.GetClass("REQUEST_RETURN");
                    var REQUEST_RETURN = XmlStringToXmlNode2(REQUEST_RETURNStr);

                    getEArchiveReportResponse = new()
                    {
                        REPORT = new REPORT
                        {
                            REPORT_NO = report.ChildNodes[0].ChildNodes.GetValue("REPORT_NO"),
                            REPORT_PERIOD = report.ChildNodes[0].ChildNodes.GetValue("REPORT_PERIOD"),
                            REPORT_SUB_STATUS = report.ChildNodes[0].ChildNodes.GetValue("REPORT_SUB_STATUS"),
                        },
                        REQUEST_RETURN = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = REQUEST_RETURN.ChildNodes[0].ChildNodes.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = REQUEST_RETURN.ChildNodes[0].ChildNodes.GetValue("RETURN_CODE"),
                        }
                    };

                    return new SuccessDataResult<GetEArchiveReportResponse>(getEArchiveReportResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetEArchiveReportResponse>(null, ex.Message);
            }
        }

        public IDataResult<ReadEArchiveReportResponse> ReadEArchiveReport(string sessionId, string raporNo)
        {
            try
            {
                var res = _iInvoiceDal.GetReadEArchiveReportRequest(sessionId, raporNo);

                if (res == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                ITemplate<RReadEArchiveReportRequest> template = new ReadEArchiveReportXML();
                var xmlElement = template.Run(res.Data);
                var xml = xmlElement.ObjectToSoapXml();

                ReadEArchiveReportResponse readEArchiveReportResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res_.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];

                    var RequestReturnStr = node.GetClass("REQUEST_RETURN");
                    var RequestReturn = XmlStringToXmlNode2(RequestReturnStr);

                    //var zipByteArray = Convert.FromBase64String(readEArchiveReportResponse.EARCHIVEREPORT);
                    var zipByteArray = Convert.FromBase64String(node.ChildNodes.GetValue("EARCHIVEREPORT"));
                    var stream = new MemoryStream(zipByteArray);
                    var zipArchive = new ZipArchive(stream);
                    var document = new XmlDocument();
                    var x = zipArchive.Entries[0].Open();
                    var zipArchive2 = new ZipArchive(x);
                    var x2 = zipArchive2.Entries[0].Open();

                    var reader = new StreamReader(x2, true);
                    string nodeStr = reader.ReadToEnd();
                    //document.LoadXml(eArchiveStr);
                    var nodeZip = XmlStringToXmlNode2(nodeStr);

                    var baslikStr = nodeZip.ChildNodes[0].ChildNodes[0].GetClass("baslik");
                    var baslik = XmlStringToXmlNode2(baslikStr);

                    var mukellefStr = baslik.ChildNodes[0].GetClass("mukellef");
                    var mukellef = XmlStringToXmlNode2(mukellefStr);

                    var hazirlayanStr = baslik.ChildNodes[0].GetClass("hazirlayan");
                    var hazirlayan = XmlStringToXmlNode2(hazirlayanStr);

                    var serbestMeslekMakbuzIptalStr =
                        nodeZip.ChildNodes[0].ChildNodes[0].GetClass("serbestMeslekMakbuzIptal");
                    var serbestMeslekMakbuzIptal = XmlStringToXmlNode2(serbestMeslekMakbuzIptalStr);


                    readEArchiveReportResponse = new ReadEArchiveReportResponse
                    {
                        EARCHIVEREPORT = node.ChildNodes.GetValue("EARCHIVEREPORT"),
                        RequestReturn = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = RequestReturn.ChildNodes[0]?.ChildNodes?.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = RequestReturn.ChildNodes[0]?.ChildNodes?.GetValue("RETURN_CODE"),
                        },
                        eArsivRaporu = new eArsivRaporu
                        {
                            baslik = new baslik
                            {
                                versiyon = baslik.ChildNodes[0]?.ChildNodes.GetValue("versiyon"),
                                mukellef = new mukellef
                                {
                                    vkn = mukellef.ChildNodes[0]?.ChildNodes.GetValue("vkn"),
                                },
                                hazirlayan = new hazirlayan
                                {
                                    vkn = hazirlayan.ChildNodes[0]?.ChildNodes.GetValue("vkn"),
                                },
                                raporNo = baslik.ChildNodes[0]?.ChildNodes.GetValue("raporNo"),
                                donemBaslangicTarihi =
                                    baslik.ChildNodes[0]?.ChildNodes.GetValue("donemBaslangicTarihi"),
                                donemBitisTarihi = baslik.ChildNodes[0]?.ChildNodes.GetValue("donemBitisTarihi"),
                                bolumBaslangicTarihi =
                                    baslik.ChildNodes[0]?.ChildNodes.GetValue("bolumBaslangicTarihi"),
                                bolumBitisTarihi = baslik.ChildNodes[0]?.ChildNodes.GetValue("bolumBitisTarihi"),
                                bolumNo = baslik.ChildNodes[0]?.ChildNodes.GetValue("bolumNo")
                            },
                            serbestMeslekMakbuzIptal = new serbestMeslekMakbuzIptal
                            {
                                makbuzNo = serbestMeslekMakbuzIptal.ChildNodes[0]?.ChildNodes.GetValue("makbuzNo"),
                                iptalTarihi =
                                    serbestMeslekMakbuzIptal.ChildNodes[0]?.ChildNodes.GetValue("iptalTarihi"),
                                toplamTutar = serbestMeslekMakbuzIptal.ChildNodes[0]?.ChildNodes.GetValue("toplamTutar")
                            }
                        }
                    };

                    return new SuccessDataResult<ReadEArchiveReportResponse>(readEArchiveReportResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReadEArchiveReportResponse>(null, ex.Message);
            }
        }

        public IDataResult<EmailEarchiveInvoiceResponse> EmailEarchiveInvoice(string sessionId, string uuId,
            string eMail)
        {
            try
            {
                var res = _iInvoiceDal.GetEmailEarchiveInvoiceRequest(sessionId, uuId, eMail);
                if (res == null)
                {
                    throw new Exception(Messages.NotFoundDataByTableRowID());
                }

                if (!res.Success)
                {
                    throw new Exception(res.Message);
                }

                ITemplate<RGetEmailEarchiveInvoiceRequest> template = new GetEmailEarchiveInvoiceXML();
                var xmlElement = template.Run(res.Data);
                var xml = xmlElement.ObjectToSoapXml();

                EmailEarchiveInvoiceResponse emailEarchiveInvoiceResponse;
                IDataResult<XmlDocument> res_ = null;

                try
                {
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("Content-Type", "text/xml; charset='UTF - 8'");

                    res_ = CallWebService.Execute("https://efaturatest.izibiz.com.tr:443/EIArchiveWS/EFaturaArchive",
                        xml, "POST", header);

                    if (res_ == null)
                    {
                        throw new Exception(Messages.NotNull("Response"));
                    }

                    if (!res_.Success)
                    {
                        throw new Exception(res_.Message);
                    }

                    var node = res_.Data.ChildNodes[1].ChildNodes[0].ChildNodes[0];

                    var RequestReturnStr = node.GetClass("REQUEST_RETURN");
                    var RequestReturn = XmlStringToXmlNode2(RequestReturnStr);

                    emailEarchiveInvoiceResponse = new EmailEarchiveInvoiceResponse
                    {
                        RequestReturn = new REQUEST_RETURN
                        {
                            INTL_TXN_ID = RequestReturn.ChildNodes[0].ChildNodes.GetValue("INTL_TXN_ID"),
                            RETURN_CODE = RequestReturn.ChildNodes[0].ChildNodes.GetValue("RETURN_CODE")
                        }
                    };

                    return new SuccessDataResult<EmailEarchiveInvoiceResponse>(emailEarchiveInvoiceResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception(Messages.AnErrorOccurred + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<EmailEarchiveInvoiceResponse>(null, ex.Message);
            }
        }

        #endregion
    }
}