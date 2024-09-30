namespace ExpensePilot.Services.ExpenseAPI.Models.DTO
{
    public class InvoiceReceiptUploadDto
    {
        public IFormFile File { get; set; }
        public string? ReceiptName { get; set; }
        public string? FileExtension { get; set; }
        public string? FilePath { get; set; }
        public string? UrlPath { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
