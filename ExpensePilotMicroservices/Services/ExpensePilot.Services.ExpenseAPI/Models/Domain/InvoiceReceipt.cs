using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.ExpenseAPI.Models.Domain
{
    public class InvoiceReceipt
    {
        [Key]
        public int ReceiptID { get; set; }
        public string? ReceiptName { get; set; }
        public string? FileExtension { get; set; }
        public string? FilePath { get; set; }
        public string? UrlPath { get; set; }
        public DateTime LastUpdated { get; set; }

        //Navigationproperty
        public Expense Expense { get; set; }
    }
}
