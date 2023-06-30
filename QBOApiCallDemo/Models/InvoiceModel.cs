using System.Web.Mvc;

namespace QBOApiCallDemo.Models
{
    public class InvoiceModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class BillAddr
        {
            public string Id { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            public string Line4 { get; set; }
            public string Lat { get; set; }
            public string Long { get; set; }
        }

        public class BillEmail
        {
            public string Address { get; set; }
        }

        public class CurrencyRef
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class CustomerMemo
        {
            public string value { get; set; }
        }

        public class CustomerRef
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class CustomField
        {
            public string DefinitionId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string StringValue { get; set; }
        }

        public class Invoice
        {
            public bool AllowIPNPayment { get; set; }
            public bool AllowOnlinePayment { get; set; }
            public bool AllowOnlineCreditCardPayment { get; set; }
            public bool AllowOnlineACHPayment { get; set; }
            public string domain { get; set; }
            public bool sparse { get; set; }
            public string Id { get; set; }
            public string SyncToken { get; set; }
            public MetaData MetaData { get; set; }
            public List<CustomField> CustomField { get; set; }
            public string DocNumber { get; set; }
            public string TxnDate { get; set; }
            public CurrencyRef CurrencyRef { get; set; }
            public List<LinkedTxn> LinkedTxn { get; set; }
            public List<Line> Line { get; set; }
            public TxnTaxDetail TxnTaxDetail { get; set; }
            public CustomerRef CustomerRef { get; set; }
            public CustomerMemo CustomerMemo { get; set; }
            public BillAddr BillAddr { get; set; }
            public ShipAddr ShipAddr { get; set; }
            public bool FreeFormAddress { get; set; }
            public SalesTermRef SalesTermRef { get; set; }
            public string DueDate { get; set; }
            public double TotalAmt { get; set; }
            public bool ApplyTaxAfterDiscount { get; set; }
            public string PrintStatus { get; set; }
            public string EmailStatus { get; set; }
            public BillEmail BillEmail { get; set; }
            public double Balance { get; set; }
        }

        public class ItemAccountRef
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class ItemRef
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class LastModifiedByRef
        {
            public string value { get; set; }
        }

        public class Line
        {
            public string Id { get; set; }
            public int LineNum { get; set; }
            public string Description { get; set; }
            public double Amount { get; set; }
            public List<LinkedTxn> LinkedTxn { get; set; }
            public string DetailType { get; set; }
            public SalesItemLineDetail SalesItemLineDetail { get; set; }
            public SubTotalLineDetail SubTotalLineDetail { get; set; }
        }

        public class LinkedTxn
        {
            public string TxnId { get; set; }
            public string TxnType { get; set; }
        }

        public class MetaData
        {
            public DateTime CreateTime { get; set; }
            public LastModifiedByRef LastModifiedByRef { get; set; }
            public DateTime LastUpdatedTime { get; set; }
        }

        public class QueryResponse
        {
            public List<Invoice> Invoice { get; set; }
            public int startPosition { get; set; }
            public int maxResults { get; set; }
            public int totalCount { get; set; }
        }

        public class Root
        {
            public QueryResponse QueryResponse { get; set; }
            public DateTime time { get; set; }
            public int? rowcount { get; set; }
        }

        public class SalesItemLineDetail
        {
            public ItemRef ItemRef { get; set; }
            public double UnitPrice { get; set; }
            public double Qty { get; set; }
            public ItemAccountRef ItemAccountRef { get; set; }
            public TaxCodeRef TaxCodeRef { get; set; }
        }

        public class SalesTermRef
        {
            public string value { get; set; }
            public string name { get; set; }
        }

        public class ShipAddr
        {
            public string Id { get; set; }
            public string Line1 { get; set; }
            public string City { get; set; }
            public string CountrySubDivisionCode { get; set; }
            public string PostalCode { get; set; }
            public string Lat { get; set; }
            public string Long { get; set; }
        }

        public class SubTotalLineDetail
        {
        }

        public class TaxCodeRef
        {
            public string value { get; set; }
        }

        public class TaxLine
        {
            public double Amount { get; set; }
            public string DetailType { get; set; }
            public TaxLineDetail TaxLineDetail { get; set; }
        }

        public class TaxLineDetail
        {
            public TaxRateRef TaxRateRef { get; set; }
            public bool PercentBased { get; set; }
            public int TaxPercent { get; set; }
            public double NetAmountTaxable { get; set; }
        }

        public class TaxRateRef
        {
            public string value { get; set; }
        }

        public class TxnTaxCodeRef
        {
            public string value { get; set; }
        }

        public class TxnTaxDetail
        {
            public TxnTaxCodeRef TxnTaxCodeRef { get; set; }
            public double TotalTax { get; set; }
            public List<TaxLine> TaxLine { get; set; }
        }

        public class HtmlModel
        {
            [AllowHtml]
            public string GridHtml { get; set; }
        }
    }
}
