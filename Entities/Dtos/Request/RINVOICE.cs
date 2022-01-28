using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RINVOICE
    {
        public HEADERINV HEADER { get; set; }
        public string ID { get; set; }
        public string UUID { get; set; }
        public CONTENT CONTENT { get; set; }
    }
    public class HEADERINV
    {
        public string DIRECTION { get; set; }
    }
    public class CONTENT
    {
        public List<UBLExtension> UBLExtensions { get; set; }
        public string UBLVersionID { get; set; }
        public string CustomizationID { get; set; }
        public string ProfileID { get; set; }
        public string ID { get; set; }
        public string CopyIndicator { get; set; }
        public string UUID { get; set; }
        public string IssueDate { get; set; }
        public string IssueTime { get; set; }
        public string InvoiceTypeCode { get; set; }
        public string Note { get; set; }
        public string DocumentCurrencyCode { get; set; }
        public string LineCountNumeric { get; set; }
        public DespatchDocumentReference DespatchDocumentReference { get; set; }
        public AdditionalDocumentReference AdditionalDocumentReference { get; set; }
        public List<AdditionalDocumentReference> AdditionalDocumentReferences { get; set; }
        public Signature Signature { get; set; }
        public AccountingSupplierParty AccountingSupplierParty { get; set; }
        public AccountingCustomerParty AccountingCustomerParty { get; set; }
        public List<AllowanceCharge> AllowanceCharges { get; set; }
        public PricingExchangeRate PricingExchangeRate { get; set; }
        public TaxTotal TaxTotal { get; set; }
        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }
    public class PricingExchangeRate
    {
        public string SourceCurrencyCode { get; set; }
        public string TargetCurrencyCode { get; set; }
        public string CalculationRate { get; set; }
        public string Date { get; set; }
    }
    public class DespatchDocumentReference
    {
        public string ID { get; set; }
        public string IssueDate { get; set; }
    }
    public class AllowanceCharge
    {
        public string ChargeIndicator { get; set; }
        public string AllowanceChargeReason { get; set; }
        public string MultiplierFactorNumeric { get; set; }
        public string SequenceNumeric { get; set; }
        public string Amount { get; set; }
        public string BaseAmount { get; set; }
    }
    public class UBLExtension
    {
        public ExtensionContent ExtensionContent { get; set; }
    }
    public class ExtensionContent
    {
        public SignatureContent SignatureContent { get; set; }
    }
    public class SignatureContent // signature
    {
        public SignedInfo SignedInfo { get; set; }
        public string SignatureValue { get; set; }
        public KeyInfo KeyInfo { get; set; }
        public ObjectThis ObjectThis { get; set; }
    }
    public class ObjectThis
    {
        public QualifyingProperties QualifyingProperties { get; set; }
    }
    public class QualifyingProperties
    {
        public SignedProperties SignedProperties { get; set; }
    }
    public class SignedProperties
    {
        public SignedSignatureProperties SignedSignatureProperties { get; set; }
        public SignedDataObjectProperties SignedDataObjectProperties { get; set; }
    }
    public class SignedDataObjectProperties
    {
        public DataObjectFormat DataObjectFormat { get; set; }
    }
    public class DataObjectFormat
    {
        public string MimeType { get; set; }
    }
    public class SignedSignatureProperties
    {
        public string SigningTime { get; set; }
        public SigningCertificate SigningCertificate { get; set; }
        public SignerRole SignerRole { get; set; }
    }
    public class SignerRole
    {
        public List<string> ClaimedRoles { get; set; }
    }
   
    public class SigningCertificate
    {
        public Cert Cert { get; set; }
    }
    public class Cert
    {
        public CertDigest CertDigest { get; set; }
        public IssuerSerial IssuerSerial { get; set; }
    }
    public class IssuerSerial
    {
        public string X509IssuerName { get; set; }
        public string X509SerialNumber { get; set; }
    }
    public class CertDigest
    {
        public string DigestMethod { get; set; }
        public string DigestValue { get; set; }
    }
    public class KeyInfo
    {
        public X509Data X509Data { get; set; }
        public KeyValue KeyValue { get; set; }
    }
    public class KeyValue
    {
        public RSAKeyValue RSAKeyValue { get; set; }
    }
    public class RSAKeyValue
    {
        public string Modulus { get; set; }
    }
    public class X509Data
    {
        public int X509Certificate { get; set; }
    }
    public class SignedInfo
    {
        public string CanonicalizationMethod { get; set; }
        public string SignatureMethod { get; set; }
        public List<Reference> References { get; set; }
    }
    public class Reference
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string URI { get; set; }
        public string DigestMethod { get; set; }
        public string DigestValue { get; set; }
        public List<Transform> Transforms { get; set; }
    }
    public class Transform
    {

    }
    public class AdditionalDocumentReference
    {
        public string ID { get; set; }
        public string IssueDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeCode { get; set; }
        public Attachment Attachment { get; set; }
    }
    public class Attachment
    {
        public string EmbeddedDocumentBinaryObject { get; set; }
    }
    public class Signature
    {
        public string ID { get; set; }
        public SignatoryParty SignatoryParty { get; set; }
        public DigitalSignatureAttachment DigitalSignatureAttachment { get; set; }
    }
    public class SignatoryParty
    {
        public string WebSiteURI { get; set; }
        public PartyIdentification PartyIdentification { get; set; }
        public PostalAddress PostalAddress { get; set; }
        public PartyTaxScheme PartyTaxScheme { get; set; }
        public PartyName PartyName { get; set; }
    }
    public class PostalAddress
    {
        public string ID { get; set; }
        public string Room { get; set; }
        public string StreetName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNumber { get; set; }
        public string CitySubdivisionName { get; set; }
        public string CityName { get; set; }
        public string PostalZone { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public Country Country { get; set; }
    }
    public class Country
    {
        public string Name { get; set; }
    }
    public class DigitalSignatureAttachment
    {
        public ExternalReference ExternalReference { get; set; }
    }
    public class ExternalReference
    {
        public string URI { get; set; }
    }
    public class AccountingSupplierParty
    {
        public Party Party { get; set; }
    }
    public class Party
    {
        public string WebsiteURI { get; set; }
        public List<PartyIdentification> PartyIdentifications { get; set; }
        public PartyName PartyName { get; set; }
        public PostalAddress PostalAddress { get; set; }
        public PartyTaxScheme PartyTaxSheme { get; set; }
        public ContactInv Contact { get; set; }
        public PersonInv Person { get; set; }
    }
    public class PersonInv
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
    }
    public class PartyTaxScheme
    {
        public TaxScheme TaxScheme { get; set; }
    }
    public class PartyIdentification
    {
        public string ID { get; set; }
    }
    public class PartyName
    {
        public string Name { get; set; }
    }
    public class ContactInv
    {
        public string Telephone { get; set; }
        public string Telefax { get; set; }
        public string ElectronicMail { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
    }
    public class AccountingCustomerParty
    {
        public Party Party { get; set; }
    }
    public class TaxTotal
    {
        public string TaxAmount { get; set; }
        public TaxSubtotal TaxSubtotal { get; set; }
    }
    public class TaxSubtotal
    {
        public string TaxableAmount { get; set; }
        public string TaxAmount { get; set; }
        public string CalculationSequenceNumeric { get; set; }
        public string TransactionCurrencyTaxAmount { get; set; }
        public string Percent { get; set; }
        public TaxCategory TaxCategory { get; set; }
    }
    public class TaxCategory
    {
        public string TaxExemptionReasonCode { get; set; }
        public string TaxExemptionReason { get; set; }
        public TaxScheme TaxScheme { get; set; }
    }
    public class TaxScheme
    {
        public string Name { get; set; }
        public string TaxTypeCode { get; set; }
    }
    public class LegalMonetaryTotal
    {
        public string LineExtensionAmount { get; set; }
        public string TaxExclusiveAmount { get; set; }
        public string TaxInclusiveAmount { get; set; }
        public string AllowanceTotalAmount { get; set; }
        public string ChargeTotalAmount { get; set; }
        public string PayableAmount { get; set; }
    }
    public class InvoiceLine
    {
        public string ID { get; set; }
        public List<string> Notes { get; set; }
        public string InvoicedQuantity { get; set; }
        public string LineExtensionAmount { get; set; }
        public OrderLineReference OrderLineReference { get; set; }
        public TaxTotal TaxTotal { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }
        public List<AllowanceCharge> AllowanceCharges { get; set; }
    }
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SellersItemIdentification SellersItemIdentification { get; set; }
        public OriginCountry OriginCountry { get; set; }
        public List<CommodityClassification> CommodityClassifications { get; set; }
    }
    public class CommodityClassification
    {
        public string ItemClassificationCode { get; set; }
    }
    public class OriginCountry
    {
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
    }
    public class SellersItemIdentification
    {
        public string ID { get; set; }
    }
    public class OrderLineReference
    {
        public string LineID { get; set; }
    }
    public class Price
    {
        public string PriceAmount { get; set; }
    }
}
