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
    public class WriteToArchieveExtendedRequestXML : ITemplate<RWriteToArchieveExtendedRequest>
    {
        public static readonly XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public static readonly XNamespace arc = "http://schemas.i2i.com/ei/wsdl/archive";
        public static readonly XNamespace xm = "http://www.w3.org/2005/05/xmlmime";
        public static readonly XNamespace contentType = "application/?";

        public static readonly XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public static readonly XNamespace xades = "http://uri.etsi.org/01903/v1.3.2#";
        public static readonly XNamespace udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
        public static readonly XNamespace ubltr = "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents";
        public static readonly XNamespace qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
        public static readonly XNamespace ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
        public static readonly XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public static readonly XNamespace ccts = "urn:un:unece:uncefact:documentation:2";
        public static readonly XNamespace ds = "http://www.w3.org/2000/09/xmldsig#";
        public static readonly XNamespace xmlns = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
        public static readonly XNamespace schemeID = "VKN_TCKN";
        public static readonly XNamespace partyIdentificationSchemeID = "VKN";
        public static readonly XNamespace customerSchemeID = "TCKN";
        public static readonly XNamespace currencyID = "TRY";
        public static readonly XNamespace unitCode = "C62";

        public static readonly XNamespace mimeCode = "application/xml";
        public static readonly XNamespace encodingCode = "Base64";
        public static readonly XNamespace characterSetCode = "UTF-8";
        public static readonly XNamespace filename = "NEA2021000000002.xslt";

        public XElement Run(RWriteToArchieveExtendedRequest model)
        {
            XElement xElement = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "arc", arc.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xm", xm.NamespaceName));

            XElement Header = new XElement(soapenv + "Header");

            xElement.Add(Header);

            XElement Body = new XElement(soapenv + "Body");

            XElement ArchiveInvoiceExtendedRequest = new XElement(arc + "ArchiveInvoiceExtendedRequest");

            XElement RequestHeader = new XElement("REQUEST_HEADER");

            RequestHeader.Add(new XElement("SESSION_ID", model.Body?.RequestHeader?.SESSION_ID));
            RequestHeader.Add(new XElement("COMPRESSED", model.Body?.RequestHeader?.COMPRESSED));

            ArchiveInvoiceExtendedRequest.Add(RequestHeader);

            XElement ArchiveInvoiceExtendedContent = new XElement("ArchiveInvoiceExtendedContent");

            XElement INVOICE_PROPERTIES = new XElement("INVOICE_PROPERTIES");

            INVOICE_PROPERTIES.Add(new XElement("EARSIV_FLAG", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.EARSIV_FLAG));

            XElement EARSIV_PROPERTIES = new XElement("EARSIV_PROPERTIES");
            EARSIV_PROPERTIES.Add(new XElement("EARSIV_TYPE", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.EARSIV_PROPERTIES?.EARSIV_TYPE));
            EARSIV_PROPERTIES.Add(new XElement("EARSIV_EMAIL_FLAG", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.EARSIV_PROPERTIES?.EARSIV_EMAIL_FLAG));
            EARSIV_PROPERTIES.Add(new XElement("SUB_STATUS", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.EARSIV_PROPERTIES?.SUB_STATUS));

            INVOICE_PROPERTIES.Add(EARSIV_PROPERTIES);

            INVOICE_PROPERTIES.Add(new XElement("PDF_PROPERTIES")); //, model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES.PDF_PROPERTIES

            XElement INVOICE_CONTENT = new XElement("INVOICE_CONTENT", new XAttribute(xm + "contentType", contentType.NamespaceName));

            //INVOICE

            XElement Invoice = new XElement(xmlns + "Invoice",
                new XAttribute(XNamespace.Xmlns + "cac", cac.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xades", xades.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "udt", udt.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ubltr", ubltr.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "qdt", qdt.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ext", ext.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "cbc", cbc.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ccts", ccts.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "ds", ds.NamespaceName));

            Invoice.Add(new XElement(cbc + "UBLVersionID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.UBLVersionID));
            Invoice.Add(new XElement(cbc + "CustomizationID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.CustomizationID));
            Invoice.Add(new XElement(cbc + "ProfileID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.ProfileID));
            Invoice.Add(new XElement(cbc + "ID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.ID));
            Invoice.Add(new XElement(cbc + "CopyIndicator", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.CopyIndicator));
            Invoice.Add(new XElement(cbc + "UUID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.UUID));
            Invoice.Add(new XElement(cbc + "IssueDate", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.IssueDate));
            Invoice.Add(new XElement(cbc + "IssueTime", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.IssueTime));
            Invoice.Add(new XElement(cbc + "InvoiceTypeCode", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceTypeCode));
            Invoice.Add(new XElement(cbc + "Note", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Note));
            Invoice.Add(new XElement(cbc + "DocumentCurrencyCode", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.DocumentCurrencyCode));
            Invoice.Add(new XElement(cbc + "LineCountNumeric", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LineCountNumeric));

            int additionalCount = model.Body.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT.AdditionalDocumentReferences.Count;
            List<AdditionalDocumentReference> additionals = model.Body.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT.AdditionalDocumentReferences;
            //XElement AdditionalDocumentReference = new XElement(cac + "AdditionalDocumentReference");
            for (int i = 0; i < additionalCount; i++)
            {
                XElement AdditionalDocumentReference = new XElement(cac + "AdditionalDocumentReference");
                AdditionalDocumentReference.Add(new XElement(cbc + "ID", additionals[i]?.ID));
                AdditionalDocumentReference.Add(new XElement(cbc + "IssueDate", additionals[i]?.IssueDate));
                if (!string.IsNullOrEmpty(additionals[i]?.DocumentTypeCode))
                {
                    AdditionalDocumentReference.Add(new XElement(cbc + "DocumentTypeCode", additionals[i]?.DocumentTypeCode));
                }

                AdditionalDocumentReference.Add(new XElement(cbc + "DocumentType", additionals[i]?.DocumentType));

                //additionals[i].Attachment = new Attachment();
                if (additionals[i].Attachment != null)
                {
                    XElement Attachment = new XElement(cac + "Attachment");
                    Attachment.Add(new XElement(cbc + "EmbeddedDocumentBinaryObject",
                       new XAttribute("mimeCode", mimeCode.NamespaceName),
                       new XAttribute("encodingCode", encodingCode.NamespaceName),
                       new XAttribute("characterSetCode", characterSetCode.NamespaceName),
                       new XAttribute("filename", filename.NamespaceName),
                       additionals[i]?.Attachment?.EmbeddedDocumentBinaryObject));
                    AdditionalDocumentReference.Add(Attachment);
                }
                Invoice.Add(AdditionalDocumentReference);
            }

            XElement Signature = new XElement(cac + "Signature");
            Signature.Add(new XElement(cbc + "ID", new XAttribute("schemeID", schemeID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.ID));

            XElement SignatoryParty = new XElement(cac + "SignatoryParty");
            SignatoryParty.Add(new XElement(cbc + "WebsiteURI", model.Body.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.WebSiteURI));

            XElement PartyIdentification = new XElement(cac + "PartyIdentification");
            PartyIdentification.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PartyIdentification.ID));


            XElement PostalAddress = new XElement(cac + "PostalAddress");
            PostalAddress.Add(new XElement(cbc + "StreetName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.StreetName));
            PostalAddress.Add(new XElement(cbc + "BuildingName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.BuildingName));
            PostalAddress.Add(new XElement(cbc + "BuildingNumber", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.BuildingNumber));
            PostalAddress.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.CitySubdivisionName));
            PostalAddress.Add(new XElement(cbc + "CityName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.CityName));
            PostalAddress.Add(new XElement(cbc + "PostalZone", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.PostalZone));
            PostalAddress.Add(new XElement(cbc + "Region", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.Region));

            XElement Country = new XElement(cac + "Country");
            Country.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.SignatoryParty?.PostalAddress?.Country.Name));
            PostalAddress.Add(Country);

            SignatoryParty.Add(PartyIdentification);
            SignatoryParty.Add(PostalAddress);

            XElement DigitalSignatureAttachment = new XElement(cac + "DigitalSignatureAttachment");
            XElement ExternalReference = new XElement(cac + "ExternalReference");
            ExternalReference.Add(new XElement(cbc + "URI", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.Signature?.DigitalSignatureAttachment?.ExternalReference?.URI));
            DigitalSignatureAttachment.Add(ExternalReference);

            Signature.Add(SignatoryParty);
            Signature.Add(DigitalSignatureAttachment);

            Invoice.Add(Signature);

            XElement AccountingSupplierParty = new XElement(cac + "AccountingSupplierParty");
            XElement Party = new XElement(cac + "Party");
            Party.Add(new XElement(cbc + "WebsiteURI", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.WebsiteURI));

            XElement PartyIdentificationAccoting = new XElement(cac + "PartyIdentification");
            PartyIdentificationAccoting.Add(new XElement(cbc + "ID", new XAttribute("schemeID", partyIdentificationSchemeID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PartyIdentifications[0]?.ID));

            Party.Add(PartyIdentificationAccoting);



            XElement PartyName = new XElement(cac + "PartyName");
            PartyName.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PartyName?.Name));



            Party.Add(PartyName);

            XElement PostalAddressAccountingSupplierParty = new XElement(cac + "PostalAddress");
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "StreetName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress.StreetName));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "BuildingName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.BuildingName));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "BuildingNumber", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.BuildingNumber));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.CitySubdivisionName));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "CityName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.CityName));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "PostalZone", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.PostalZone));
            PostalAddressAccountingSupplierParty.Add(new XElement(cbc + "Region", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.Region));

            XElement CountryAccountingSupplierParty = new XElement(cac + "Country");
            CountryAccountingSupplierParty.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PostalAddress?.Country.Name));
            PostalAddressAccountingSupplierParty.Add(CountryAccountingSupplierParty);

            Party.Add(PostalAddressAccountingSupplierParty);

            //XElement PartyTaxScheme = new XElement(cac + "PartyTaxScheme");
            //XElement TaxScheme = new XElement(cac + "TaxScheme");
            //TaxScheme.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.PartyTaxSheme?.TaxScheme?.Name));
            //PartyTaxScheme.Add(TaxScheme);

            //Party.Add(PartyTaxScheme);

            XElement Contact = new XElement(cac + "Contact");
            Contact.Add(new XElement(cbc + "Telephone", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.Telephone));
            Contact.Add(new XElement(cbc + "Telefax", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.Telefax));
            Contact.Add(new XElement(cbc + "ElectronicMail", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.Contact?.ElectronicMail));

            Party.Add(Contact);

            XElement Person = new XElement(cac + "Person");
            Person.Add(new XElement(cbc + "FirstName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.Person?.FirstName));
            Person.Add(new XElement(cbc + "FamilyName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingSupplierParty?.Party?.Person?.FamilyName));

            Party.Add(Person);

            AccountingSupplierParty.Add(Party);

            Invoice.Add(AccountingSupplierParty);


            XElement AccountingCustomerParty = new XElement(cac + "AccountingCustomerParty");
            XElement PartyCustomer = new XElement(cac + "Party");
            PartyCustomer.Add(new XElement(cbc + "WebsiteURI", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.WebsiteURI));

            XElement PartyIdentificationCustomer = new XElement(cac + "PartyIdentification");
            PartyIdentificationCustomer.Add(new XElement(cbc + "ID", new XAttribute("schemeID", customerSchemeID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PartyIdentifications[0]?.ID));

            PartyCustomer.Add(PartyIdentificationCustomer);

            XElement PartyNameCustomer = new XElement(cac + "PartyName");
            PartyNameCustomer.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PartyName?.Name));

            PartyCustomer.Add(PartyNameCustomer);

            XElement PostalAddressAccountingCustomerParty = new XElement(cac + "PostalAddress");
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "StreetName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress.StreetName));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "BuildingName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.BuildingName));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "BuildingNumber", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.BuildingNumber));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "CitySubdivisionName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.CitySubdivisionName));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "CityName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.CityName));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "PostalZone", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.PostalZone));
            PostalAddressAccountingCustomerParty.Add(new XElement(cbc + "Region", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.Region));

            XElement CountryAccountingCustomerParty = new XElement(cac + "Country");
            CountryAccountingCustomerParty.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PostalAddress?.Country.Name));
            PostalAddressAccountingCustomerParty.Add(CountryAccountingCustomerParty);

            PartyCustomer.Add(PostalAddressAccountingCustomerParty);

            //XElement PartyTaxSchemeCustomer = new XElement(cac + "PartyTaxScheme");
            //XElement TaxSchemeCustomer = new XElement(cac + "TaxScheme");
            //TaxSchemeCustomer.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.PartyTaxSheme?.TaxScheme?.Name));
            //PartyTaxSchemeCustomer.Add(TaxSchemeCustomer);

            //PartyCustomer.Add(PartyTaxSchemeCustomer);

            XElement ContactCustomer = new XElement(cac + "Contact");
            ContactCustomer.Add(new XElement(cbc + "Telephone", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.Telephone));
            ContactCustomer.Add(new XElement(cbc + "Telefax", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.Telefax));
            ContactCustomer.Add(new XElement(cbc + "ElectronicMail", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.Contact?.ElectronicMail));

            PartyCustomer.Add(ContactCustomer);

            XElement PersonCustomer = new XElement(cac + "Person");
            PersonCustomer.Add(new XElement(cbc + "FirstName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.Person.FirstName));
            PersonCustomer.Add(new XElement(cbc + "FamilyName", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AccountingCustomerParty?.Party?.Person.FamilyName));

            PartyCustomer.Add(PersonCustomer);


            AccountingCustomerParty.Add(PartyCustomer);

            Invoice.Add(AccountingCustomerParty);


            var allowances = model.Body.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT.AllowanceCharges.Count;
            for (int i = 0; i < allowances; i++)
            { //XElement INVOICE_CONTENT = new XElement("INVOICE_CONTENT", new XAttribute(xm + "contentType", contentType.NamespaceName));
                XElement AllowanceCharge = new XElement(cac + "AllowanceCharge");
                AllowanceCharge.Add(new XElement(cbc + "ChargeIndicator", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AllowanceCharges[i].ChargeIndicator));
                AllowanceCharge.Add(new XElement(cbc + "Amount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AllowanceCharges[i].Amount));
                AllowanceCharge.Add(new XElement(cbc + "BaseAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.AllowanceCharges[i].BaseAmount));
                Invoice.Add(AllowanceCharge);
            }

            XElement TaxTotal = new XElement(cac + "TaxTotal");
            TaxTotal.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxAmount));

            XElement TaxSubtotal = new XElement(cac + "TaxSubtotal");
            TaxSubtotal.Add(new XElement(cbc + "TaxableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal.TaxableAmount));
            TaxSubtotal.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal.TaxAmount));

            if (!string.IsNullOrEmpty(model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric))
            {
                TaxSubtotal.Add(new XElement(cbc + "CalculationSequenceNumeric", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric));
            }


            TaxSubtotal.Add(new XElement(cbc + "Percent", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal.Percent));

            XElement TaxCategory = new XElement(cac + "TaxCategory");
            XElement TaxTotalTaxScheme = new XElement(cac + "TaxScheme");
            TaxTotalTaxScheme.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.Name));
            TaxTotalTaxScheme.Add(new XElement(cbc + "TaxTypeCode", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.TaxTypeCode));
            TaxCategory.Add(TaxTotalTaxScheme);

            TaxSubtotal.Add(TaxCategory);

            TaxTotal.Add(TaxSubtotal);

            Invoice.Add(TaxTotal);

            XElement LegalMonetaryTotal = new XElement(cac + "LegalMonetaryTotal");
            LegalMonetaryTotal.Add(new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.LineExtensionAmount));
            LegalMonetaryTotal.Add(new XElement(cbc + "TaxExclusiveAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.TaxExclusiveAmount));
            LegalMonetaryTotal.Add(new XElement(cbc + "TaxInclusiveAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.TaxInclusiveAmount));
            LegalMonetaryTotal.Add(new XElement(cbc + "AllowanceTotalAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.AllowanceTotalAmount));
            if (!string.IsNullOrEmpty(model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.ChargeTotalAmount))
            {
                LegalMonetaryTotal.Add(new XElement(cbc + "ChargeTotalAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.ChargeTotalAmount));
            }

            LegalMonetaryTotal.Add(new XElement(cbc + "PayableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.LegalMonetaryTotal?.PayableAmount));

            Invoice.Add(LegalMonetaryTotal);

            //XElement InvoiceLine = new XElement(cac + "InvoiceLine");
            //InvoiceLine.Add(new XElement(cbc + "ID", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.ID));
            //InvoiceLine.Add(new XElement(cbc + "Note", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.Notes));
            //InvoiceLine.Add(new XElement(cbc + "InvoicedQuantity", new XAttribute("unitCode", unitCode.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.InvoicedQuantity));
            //InvoiceLine.Add(new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.LineExtensionAmount));

            //XElement TaxTotalInvoice = new XElement(cac + "TaxTotal");
            //TaxTotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxAmount));

            //XElement TaxSubtotalInvoice = new XElement(cac + "TaxSubtotal");
            //TaxSubtotalInvoice.Add(new XElement(cbc + "TaxableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal.TaxableAmount));
            //TaxSubtotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal.TaxAmount));
            //TaxSubtotalInvoice.Add(new XElement(cbc + "CalculationSequenceNumeric", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric));
            //TaxSubtotalInvoice.Add(new XElement(cbc + "Percent", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal.Percent));

            //XElement TaxCategoryInvoice = new XElement(cac + "TaxCategory");
            //XElement TaxSchemeInvoice = new XElement(cac + "TaxScheme");
            //TaxSchemeInvoice.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.Name));
            //TaxSchemeInvoice.Add(new XElement(cbc + "TaxTypeCode", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.TaxTypeCode));
            //TaxCategoryInvoice.Add(TaxTotalTaxScheme);

            //TaxSubtotalInvoice.Add(TaxCategoryInvoice);

            //TaxTotalInvoice.Add(TaxSubtotalInvoice);

            //InvoiceLine.Add(TaxTotalInvoice);

            //XElement Item = new XElement(cac + "Item");
            //Item.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.Item?.Name));

            //InvoiceLine.Add(Item);

            //XElement Price = new XElement(cac + "Price");
            //Price.Add(new XElement(cbc + "PriceAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent?.INVOICE_PROPERTIES?.INVOICE_CONTENT?.INVOICE?.CONTENT?.InvoiceLine?.Price?.PriceAmount));

            //InvoiceLine.Add(Price);

            //Invoice.Add(InvoiceLine);

            int invoCount;

            if (model.Body.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT.InvoiceLines != null)
            {
                invoCount = model.Body.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT.InvoiceLines.Count;
            }
            else
            {
                invoCount = 0;
            }

            for (int j = 0; j < invoCount; j++)
            {
                XElement InvoiceLine = new XElement(cac + "InvoiceLine");

                InvoiceLine.Add(new XElement(cbc + "ID", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.ID));
                InvoiceLine.Add(new XElement(cbc + "Note", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.Notes));
                InvoiceLine.Add(new XElement(cbc + "InvoicedQuantity", new XAttribute("unitCode", unitCode.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.InvoicedQuantity));
                InvoiceLine.Add(new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.LineExtensionAmount));

                XElement TaxTotalInvoice = new XElement(cac + "TaxTotal");
                TaxTotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxAmount));

                XElement TaxSubtotalInvoice = new XElement(cac + "TaxSubtotal");
                TaxSubtotalInvoice.Add(new XElement(cbc + "TaxableAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.TaxableAmount));
                TaxSubtotalInvoice.Add(new XElement(cbc + "TaxAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.TaxAmount));
                TaxSubtotalInvoice.Add(new XElement(cbc + "CalculationSequenceNumeric", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.CalculationSequenceNumeric));
                TaxSubtotalInvoice.Add(new XElement(cbc + "Percent", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal.Percent));

                XElement TaxCategoryInvoice = new XElement(cac + "TaxCategory");
                XElement TaxSchemeInvoice = new XElement(cac + "TaxScheme");
                TaxSchemeInvoice.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.Name));
                TaxSchemeInvoice.Add(new XElement(cbc + "TaxTypeCode", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.TaxTotal?.TaxSubtotal?.TaxCategory?.TaxScheme?.TaxTypeCode));
                TaxCategoryInvoice.Add(TaxTotalTaxScheme);

                TaxSubtotalInvoice.Add(TaxCategoryInvoice);

                TaxTotalInvoice.Add(TaxSubtotalInvoice);

                InvoiceLine.Add(TaxTotalInvoice);

                XElement Item = new XElement(cac + "Item");
                Item.Add(new XElement(cbc + "Name", model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.Item?.Name));

                InvoiceLine.Add(Item);

                XElement Price = new XElement(cac + "Price");
                Price.Add(new XElement(cbc + "PriceAmount", new XAttribute("currencyID", currencyID.NamespaceName), model.Body?.ArchiveInvoiceExtendedContent.INVOICE_PROPERTIES.INVOICE_CONTENT.INVOICE.CONTENT?.InvoiceLines[j]?.Price?.PriceAmount));

                InvoiceLine.Add(Price);


                Invoice.Add(InvoiceLine);
            }

            //INVOICE
            string invoiceStr = Invoice.ToString();
            byte[] invoiceBytes = Encoding.ASCII.GetBytes(invoiceStr);
            var invoiceBase64 = Convert.ToBase64String(invoiceBytes);

            INVOICE_CONTENT.Add(invoiceBase64);

            INVOICE_PROPERTIES.Add(INVOICE_CONTENT);

            ArchiveInvoiceExtendedContent.Add(INVOICE_PROPERTIES);

            ArchiveInvoiceExtendedRequest.Add(ArchiveInvoiceExtendedContent);

            Body.Add(ArchiveInvoiceExtendedRequest);

            xElement.Add(Body);

            return xElement;
        }
    }
}
