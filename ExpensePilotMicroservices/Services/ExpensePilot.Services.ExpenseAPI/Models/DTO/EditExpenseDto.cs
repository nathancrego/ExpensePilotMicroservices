namespace ExpensePilot.Services.ExpenseAPI.Models.DTO
{
    public class EditExpenseDto
    {
        public string ExpenseName { get; set; }
        public string ExpenseDescription { get; set; }
        public int ExpenseCategoryID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceVendorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public int InvoiceReceiptID { get; set; }
        //public Guid UserID { get; set; }
        //public int ExpenseStatusID { get; set; }
    }
}
