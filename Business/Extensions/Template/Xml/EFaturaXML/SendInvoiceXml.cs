using Business.Abstract;
using Entities.Dtos.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Business.Extensions.Template.Xml.EFaturaXML
{
    public class SendInvoiceXml : ITemplate<RSendInvoiceRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace wsdl = "http://schemas.i2i.com/ei/wsdl";
        public static readonly XNamespace xm = "http://www.w3.org/2005/05/xmlmime";
        public static readonly XNamespace contentType = "application/?";

        public static readonly XNamespace xmlns = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
        public static readonly XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public static readonly XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public static readonly XNamespace ds = "http://www.w3.org/2000/09/xmldsig#";
        public static readonly XNamespace ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
        public static readonly XNamespace ns8 = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2";
        public static readonly XNamespace xades = "http://uri.etsi.org/01903/v1.3.2#";
        public static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public static readonly XNamespace schemaLocation = @"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd"; //xsi
        public static readonly XNamespace characterSetCode = "UTF-8";
        public static readonly XNamespace encodingCode = "Base64";
        public static readonly XNamespace filename = "DMY2018625142607.xslt";
        public static readonly XNamespace mimeCode = "application/xml";
        public static readonly XNamespace schemeID = "VKN_TCKN";
        public static readonly XNamespace partyIdentificationSchemeID = "VKN";
        public static readonly XNamespace partyIdentificationSchemeIDMersisNo = "MERSISNO";
        public static readonly XNamespace partyIdentificationSchemeIDTICARETSICILNO = "TICARETSICILNO";
        public static readonly XNamespace currencyID = "TRY";
        public static readonly XNamespace unitCode = "C62";

        public XElement Run(RSendInvoiceRequest model)
        {

            XElement xElement = new XElement(soapenv + "Envelope",
              new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
              new XAttribute(XNamespace.Xmlns + "wsdl", wsdl.NamespaceName),
              new XAttribute(XNamespace.Xmlns + "xm", xm.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement SendInvoiceRequestBody = new XElement(wsdl + "SendInvoiceRequest");

            XElement RequestHeader = new XElement("REQUEST_HEADER");

            RequestHeader.Add(new XElement("SESSION_ID", model.Body?.SendInvoiceRequestBody?.REQUEST_HEADER.SESSION_ID));
            RequestHeader.Add(new XElement("COMPRESSED", model.Body?.SendInvoiceRequestBody?.REQUEST_HEADER.COMPRESSED));

            SendInvoiceRequestBody.Add(RequestHeader);

            for (int i = 0; i < model.Body?.SendInvoiceRequestBody?.Invoices.Count; i++)
            {
                XElement INVOICE = new XElement("INVOICE");

                XElement Invoice = new XElement(xmlns + "Invoice",
                new XAttribute(XNamespace.Xmlns + "cac", cac.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "cbc", cbc.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ds", ds.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ext", ext.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ns8", ns8.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xades", xades.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XAttribute(xsi + "schemaLocation", schemaLocation.NamespaceName));

                XElement HEADERINV = new XElement("HEADER");
                HEADERINV.Add(new XElement("DIRECTION", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.HEADER?.DIRECTION));

                INVOICE.Add(HEADERINV);

                XElement CONTENT = new XElement("CONTENT", new XAttribute(xm + "contentType", contentType.NamespaceName));

                XElement UBLExtensions = new XElement(ext + "UBLExtensions");
                for (int j = 0; j < model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.UBLExtensions?.Count; j++)
                {
                    XElement UBLExtension = new XElement(ext + "UBLExtension"); //, model.UBLExtensions[i]?.ExtensionContent
                    UBLExtension.Add(new XElement(ext + "ExtensionContent", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.UBLExtensions[j]?.ExtensionContent));
                    UBLExtensions.Add(UBLExtension);
                }

                //foreach (var item in model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.UBLExtensions)
                //{
                //    XElement UBLExtension = new XElement(ext + "UBLExtension"); //, model.UBLExtensions[i]?.ExtensionContent
                //    UBLExtension.Add(new XElement(ext + "ExtensionContent", item?.ExtensionContent));
                //    UBLExtensions.Add(UBLExtension);
                //}

                Invoice.Add(UBLExtensions);

                Invoice.Add(new XElement(cbc + "UBLVersionID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.UBLVersionID));
                Invoice.Add(new XElement(cbc + "CustomizationID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.CustomizationID));
                Invoice.Add(new XElement(cbc + "ProfileID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.ProfileID));
                Invoice.Add(new XElement(cbc + "ID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.ID));
                Invoice.Add(new XElement(cbc + "CopyIndicator", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.CopyIndicator));
                Invoice.Add(new XElement(cbc + "UUID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.UUID));
                Invoice.Add(new XElement(cbc + "IssueDate", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.IssueDate));
                Invoice.Add(new XElement(cbc + "IssueTime", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.IssueTime));
                Invoice.Add(new XElement(cbc + "InvoiceTypeCode", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceTypeCode));
                Invoice.Add(new XElement(cbc + "Note", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Note));
                Invoice.Add(new XElement(cbc + "DocumentCurrencyCode", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.DocumentCurrencyCode));
                Invoice.Add(new XElement(cbc + "LineCountNumeric", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LineCountNumeric));

                XElement AdditionalDocumentReference = new XElement(cac + "AdditionalDocumentReference");
                AdditionalDocumentReference.Add(new XElement(cbc + "ID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AdditionalDocumentReference?.ID));
                AdditionalDocumentReference.Add(new XElement(cbc + "IssueDate", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AdditionalDocumentReference?.IssueDate));
                AdditionalDocumentReference.Add(new XElement(cbc + "DocumentType", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AdditionalDocumentReference?.DocumentType));

                XElement Attachment = new XElement(cac + "Attachment");
                Attachment.Add(new XElement(cbc + "EmbeddedDocumentBinaryObject",
                new XAttribute("characterSetCode", characterSetCode.NamespaceName),
                new XAttribute("encodingCode", encodingCode.NamespaceName),
                new XAttribute("filename", filename.NamespaceName),
                new XAttribute("mimeCode", mimeCode.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AdditionalDocumentReference?.Attachment?.EmbeddedDocumentBinaryObject));

                AdditionalDocumentReference.Add(Attachment);

                Invoice.Add(AdditionalDocumentReference);

                XElement Signature = new XElement(cac + "Signature");
                Signature.Add(new XElement(cbc + "ID", new XAttribute("schemeID", schemeID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.ID));

                XElement SignatoryParty = new XElement(cac + "SignatoryParty");

                XElement PartyIdentification = new XElement(cac + "PartyIdentification");
                PartyIdentification.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PartyIdentification.ID));

                XElement PostalAddress = new XElement(cac + "PostalAddress");
                PostalAddress.Add(new XElement(cbc + "StreetName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.StreetName));
                PostalAddress.Add(new XElement(cbc + "BuildingName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.BuildingName));
                PostalAddress.Add(new XElement(cbc + "BuildingNumber", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.BuildingNumber));
                PostalAddress.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.CitySubdivisionName));
                PostalAddress.Add(new XElement(cbc + "CityName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.CityName));
                PostalAddress.Add(new XElement(cbc + "PostalZone", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.PostalZone));
                PostalAddress.Add(new XElement(cbc + "Region", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.Region));

                XElement Country = new XElement(cac + "Country");
                Country.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.Country.Name));
                PostalAddress.Add(Country);

                SignatoryParty.Add(PartyIdentification);
                SignatoryParty.Add(PostalAddress);

                XElement DigitalSignatureAttachment = new XElement(cac + "DigitalSignatureAttachment");
                XElement ExternalReference = new XElement(cac + "ExternalReference");
                ExternalReference.Add(new XElement(cbc + "URI", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.Signature?.DigitalSignatureAttachment?.ExternalReference?.URI));
                DigitalSignatureAttachment.Add(ExternalReference);

                Signature.Add(SignatoryParty);
                Signature.Add(DigitalSignatureAttachment);

                Invoice.Add(Signature);

                XElement AccountingSupplierParty = new XElement(cac + "AccountingSupplierParty");
                XElement Party = new XElement(cac + "Party");
                Party.Add(new XElement(cbc + "WebsiteURI", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.WebsiteURI));

                XElement PartyIdentificationAccoting = new XElement(cac + "PartyIdentification");
                PartyIdentificationAccoting.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PartyIdentifications[0]?.ID));

                Party.Add(PartyIdentificationAccoting);

                XElement PartyIdentificationMersis = new XElement(cac + "PartyIdentification");
                PartyIdentificationMersis.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeIDMersisNo.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PartyIdentifications[1]?.ID));

                Party.Add(PartyIdentificationMersis);

                XElement PartyIdentificationSchemeIDTICARETSICILNO = new XElement(cac + "PartyIdentification");
                PartyIdentificationSchemeIDTICARETSICILNO.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeIDTICARETSICILNO.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PartyIdentifications[2]?.ID));

                Party.Add(PartyIdentificationSchemeIDTICARETSICILNO);

                XElement PartyName = new XElement(cac + "PartyName");
                PartyName.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PartyName?.Name));

                Party.Add(PartyName);

                XElement PostalAddressAccountingSupplierParty = new XElement(cac + "PostalAddress");
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "StreetName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress.StreetName));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "BuildingName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.BuildingName));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "BuildingNumber", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.BuildingNumber));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.CitySubdivisionName));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "CityName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.CityName));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "PostalZone", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.PostalZone));
                PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "Region", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.Region));

                XElement CountryAccountingSupplierParty = new XElement(cac + "Country");
                CountryAccountingSupplierParty.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.Country.Name));
                PostalAddressAccountingSupplierParty.Add(CountryAccountingSupplierParty);

                Party.Add(PostalAddressAccountingSupplierParty);

                XElement PartyTaxScheme = new XElement(cac + "PartyTaxScheme");
                XElement TaxScheme = new XElement(cac + "TaxScheme");
                TaxScheme.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.PartyTaxSheme?.TaxScheme?.Name));
                PartyTaxScheme.Add(TaxScheme);

                Party.Add(PartyTaxScheme);

                XElement Contact = new XElement(cac + "Contact");
                Contact.Add(new XElement(cbc + "Telephone", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.Telephone));
                Contact.Add(new XElement(cbc + "Telefax", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.Telefax));
                Contact.Add(new XElement(cbc + "ElectronicMail", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.ElectronicMail));

                Party.Add(Contact);

                AccountingSupplierParty.Add(Party);

                Invoice.Add(AccountingSupplierParty);


                XElement AccountingCustomerParty = new XElement(cac + "AccountingCustomerParty");
                XElement PartyCustomer = new XElement(cac + "Party");
                PartyCustomer.Add(new XElement(cbc + "WebsiteURI", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.WebsiteURI));

                XElement PartyIdentificationCustomer = new XElement(cac + "PartyIdentification");
                PartyIdentificationCustomer.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PartyIdentifications[0]?.ID));

                PartyCustomer.Add(PartyIdentificationCustomer);

                XElement PartyNameCustomer = new XElement(cac + "PartyName");
                PartyNameCustomer.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PartyName?.Name));

                PartyCustomer.Add(PartyNameCustomer);

                XElement PostalAddressAccountingCustomerParty = new XElement(cac + "PostalAddress");
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "StreetName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress.StreetName));

                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "BuildingName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.BuildingName));
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "BuildingNumber", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.BuildingNumber));
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.CitySubdivisionName));
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "CityName", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.CityName));
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "PostalZone", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.PostalZone));
                PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "Region", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.Region));

                XElement CountryAccountingCustomerParty = new XElement(cac + "Country");
                CountryAccountingCustomerParty.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.Country.Name));
                PostalAddressAccountingCustomerParty.Add(CountryAccountingCustomerParty);

                PartyCustomer.Add(PostalAddressAccountingCustomerParty);

                XElement PartyTaxSchemeCustomer = new XElement(cac + "PartyTaxScheme");
                XElement TaxSchemeCustomer = new XElement(cac + "TaxScheme");
                TaxSchemeCustomer.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.PartyTaxSheme?.TaxScheme?.Name));
                PartyTaxSchemeCustomer.Add(TaxSchemeCustomer);

                PartyCustomer.Add(PartyTaxSchemeCustomer);

                XElement ContactCustomer = new XElement(cac + "Contact");
                ContactCustomer.Add(new XElement(cbc + "Telephone", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.Telephone));
                ContactCustomer.Add(new XElement(cbc + "Telefax", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.Telefax));
                ContactCustomer.Add(new XElement(cbc + "ElectronicMail", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.ElectronicMail));

                PartyCustomer.Add(ContactCustomer);

                AccountingCustomerParty.Add(PartyCustomer);

                Invoice.Add(AccountingCustomerParty);

                XElement TaxTotal = new XElement(cac + "TaxTotal");
                TaxTotal.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxAmount));

                XElement TaxSubtotal = new XElement(cac + "TaxSubtotal");
                TaxSubtotal.Add(new XElement(cbc + "TaxableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal.TaxableAmount));
                TaxSubtotal.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal.TaxAmount));
                TaxSubtotal.Add(new XElement(cbc + "CalculationSequenceNumeric", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric));
                TaxSubtotal.Add(new XElement(cbc + "Percent", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal.Percent));

                XElement TaxCategory = new XElement(cac + "TaxCategory");
                XElement TaxTotalTaxScheme = new XElement(cac + "TaxScheme");
                TaxTotalTaxScheme.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.Name));
                TaxTotalTaxScheme.Add(new XElement(cbc + "TaxTypeCode", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.TaxTypeCode));
                TaxCategory.Add(TaxTotalTaxScheme);

                TaxSubtotal.Add(TaxCategory);

                TaxTotal.Add(TaxSubtotal);

                Invoice.Add(TaxTotal);

                XElement LegalMonetaryTotal = new XElement(cac + "LegalMonetaryTotal");
                LegalMonetaryTotal.Add(new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.LineExtensionAmount));
                LegalMonetaryTotal.Add(new XElement(cbc + "TaxExclusiveAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.TaxExclusiveAmount));
                LegalMonetaryTotal.Add(new XElement(cbc + "TaxInclusiveAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.TaxInclusiveAmount));
                LegalMonetaryTotal.Add(new XElement(cbc + "AllowanceTotalAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.AllowanceTotalAmount));
                LegalMonetaryTotal.Add(new XElement(cbc + "ChargeTotalAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.ChargeTotalAmount));
                LegalMonetaryTotal.Add(new XElement(cbc + "PayableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.LegalMonetaryTotal?.PayableAmount));

                Invoice.Add(LegalMonetaryTotal);


                int invoCount;

                if (model.Body.SendInvoiceRequestBody.Invoices[i].CONTENT.InvoiceLines != null)
                {
                    invoCount = model.Body.SendInvoiceRequestBody.Invoices[i].CONTENT.InvoiceLines.Count;
                }
                else
                {
                    invoCount = 0;
                }

                for (int j = 0; j < invoCount; j++)
                {
                    XElement InvoiceLine = new XElement(cac + "InvoiceLine");

                    InvoiceLine.Add(new XElement(cbc + "ID", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.ID));
                    InvoiceLine.Add(new XElement(cbc + "Note", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.Notes));
                    InvoiceLine.Add(new XElement(cbc + "InvoicedQuantity", new XAttribute("unitCode", unitCode.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.InvoicedQuantity));
                    InvoiceLine.Add(new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.LineExtensionAmount));

                    XElement TaxTotalInvoice = new XElement(cac + "TaxTotal");
                    TaxTotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxAmount));

                    XElement TaxSubtotalInvoice = new XElement(cac + "TaxSubtotal");
                    TaxSubtotalInvoice.Add(new XElement(cbc + "TaxableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.TaxableAmount));
                    TaxSubtotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.TaxAmount));
                    TaxSubtotalInvoice.Add(new XElement(cbc + "CalculationSequenceNumeric", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric));
                    TaxSubtotalInvoice.Add(new XElement(cbc + "Percent", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.Percent));

                    XElement TaxCategoryInvoice = new XElement(cac + "TaxCategory");
                    XElement TaxSchemeInvoice = new XElement(cac + "TaxScheme");
                    TaxSchemeInvoice.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.Name));
                    TaxSchemeInvoice.Add(new XElement(cbc + "TaxTypeCode", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.TaxTypeCode));
                    TaxCategoryInvoice.Add(TaxTotalTaxScheme);

                    TaxSubtotalInvoice.Add(TaxCategoryInvoice);

                    TaxTotalInvoice.Add(TaxSubtotalInvoice);

                    InvoiceLine.Add(TaxTotalInvoice);

                    XElement Item = new XElement(cac + "Item");
                    Item.Add(new XElement(cbc + "Name", model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.Item?.Name));

                    InvoiceLine.Add(Item);

                    XElement Price = new XElement(cac + "Price");
                    Price.Add(new XElement(cbc + "PriceAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.SendInvoiceRequestBody?.Invoices[i]?.CONTENT?.InvoiceLines[j]?.Price?.PriceAmount));

                    InvoiceLine.Add(Price);


                    Invoice.Add(InvoiceLine);
                }

                string invoiceStr = Invoice.ToString();
                byte[] invoiceBytes = Encoding.ASCII.GetBytes(invoiceStr);
                var invoiceBase64 = Convert.ToBase64String(invoiceBytes);

                CONTENT.Add(invoiceBase64);

                INVOICE.Add(CONTENT);

                SendInvoiceRequestBody.Add(INVOICE);
            }

            Body.Add(SendInvoiceRequestBody);

            xElement.Add(Body);

            return xElement;
        }
        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
