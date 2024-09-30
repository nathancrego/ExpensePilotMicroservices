using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.ExpenseAPI.Models.DTO
{
    public class ExpenseDto
    {
        public int ExpenseID { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDescription { get; set; }
        public int ExpenseCategoryID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceVendorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public int InvoiceReceiptID { get; set; }
        public string UserID { get; set; }
        public int ExpenseStatusID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
    }
}
