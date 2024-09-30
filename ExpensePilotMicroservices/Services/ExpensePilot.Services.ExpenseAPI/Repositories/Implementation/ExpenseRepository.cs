using ExpensePilot.Services.ExpenseAPI.Data;
using ExpensePilot.Services.ExpenseAPI.Models.Domain;
using ExpensePilot.Services.ExpenseAPI.Models.DTO;
using ExpensePilot.Services.ExpenseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.ExpenseAPI.Repositories.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ExpenseDbContext dbContext;
        private readonly long fileSizeLimit = 20 * 1024 * 1024; // 20 MB
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };

        public ExpenseRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ExpenseDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;

        }

        public async Task<Expense> CreateAsync(Expense expenses, InvoiceReceiptUploadDto invoiceReceiptUpload)
        {
            if (invoiceReceiptUpload.File == null && invoiceReceiptUpload.File.Length == 0)
            {
                throw new Exception("Please upload the Invoice Receipt");
            }

            if (invoiceReceiptUpload.File.Length > fileSizeLimit)
            {
                throw new Exception($"File size exceeds the limit of {fileSizeLimit / (1024 * 1024)} MB.");
            }
            var fileExtension = Path.GetExtension(invoiceReceiptUpload.File.FileName).ToLowerInvariant();
            if (!permittedExtensions.Contains(fileExtension))
            {
                throw new Exception("Invalid file type. Only JPG, PNG, JPEG and PDF files are allowed.");
            }
            // Save the file to the server/local
            if (invoiceReceiptUpload.File != null && invoiceReceiptUpload.File.Length > 0)
            {
                var invoice = new InvoiceReceipt();

                // Save the InvoiceReceipt to get the ReceiptID
                await dbContext.InvoiceReceipts.AddAsync(invoice);
                await dbContext.SaveChangesAsync();

                // Create the specific folder for this invoice receipt
                var folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/Uploads", invoice.ReceiptID.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Create the file path
                var filePath = Path.Combine(folderPath, invoiceReceiptUpload.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await invoiceReceiptUpload.File.CopyToAsync(stream);
                }

                // Update the InvoiceReceipt with file information
                invoice.FilePath = filePath;
                invoice.ReceiptName = Path.GetFileName(invoiceReceiptUpload.File.FileName);
                invoice.FileExtension = Path.GetExtension(invoiceReceiptUpload.File.FileName).ToLower();
                invoice.LastUpdated = DateTime.Now;

                var httpRequest = httpContextAccessor.HttpContext.Request;
                var url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/wwwroot/Uploads/{invoice.ReceiptID}/{invoiceReceiptUpload.File.FileName}";

                invoice.UrlPath = url;

                // Update the InvoiceReceipt entity with the file path
                dbContext.InvoiceReceipts.Update(invoice);
                await dbContext.SaveChangesAsync();

                // Set the foreign key for expenses
                expenses.InvoiceReceiptID = invoice.ReceiptID;

                // Save the expenses entity
                await dbContext.Expenses.AddAsync(expenses);
                await dbContext.SaveChangesAsync();
            }
            return expenses;
        }

        public async Task<Expense?> DeleteAsync(int id)
        {
            var existingInvoice = await dbContext.InvoiceReceipts.FirstOrDefaultAsync(i => i.ReceiptID == id);
            if (existingInvoice is null)
            {
                //Invoice not found
                return null;
            }

            //fetch the associated Expense using invoice receipt ID
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.InvoiceReceiptID == id);
            if (existingExpense is null)
            {
                return null;
            }
            //Delete the file from the server if it exists
            if (!string.IsNullOrEmpty(existingInvoice.FilePath) && File.Exists(existingInvoice.FilePath))
            {
                File.Delete(existingInvoice.FilePath);
            }
            //Remove entities from DB
            dbContext.InvoiceReceipts.Remove(existingInvoice);
            dbContext.Expenses.Remove(existingExpense);

            await dbContext.SaveChangesAsync();
            return existingExpense;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await dbContext.Expenses.ToListAsync();
        }

        public async Task<Expense?> GetByIDAsync(int id)
        {
            return await dbContext.Expenses.FirstOrDefaultAsync(e => e.ExpenseID == id);
        }

        public async Task<Expense?> UpdateAsync(Expense updatedExpenses, InvoiceReceiptUploadDto invoiceReceiptUpload)
        {
            // Fetch the existing Expense using the primary key (ExpenseID)
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.ExpenseID == updatedExpenses.ExpenseID);
            if (existingExpense == null)
            {
                // Expense not found
                return null;
            }

            // Fetch the associated InvoiceReceipt using the InvoiceReceiptID from the existingExpense
            var existingInvoice = await dbContext.InvoiceReceipts.FirstOrDefaultAsync(i => i.ReceiptID == existingExpense.InvoiceReceiptID);
            if (existingInvoice == null)
            {
                // Invoice not found
                return null;
            }

            if (invoiceReceiptUpload.File != null && invoiceReceiptUpload.File.Length > 0)
            {
                // Delete the old file if it exists
                if (!string.IsNullOrEmpty(existingInvoice.FilePath) && File.Exists(existingInvoice.FilePath))
                {
                    File.Delete(existingInvoice.FilePath);
                }

                // Create the specific folder for this invoice receipt
                var folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/Uploads", existingInvoice.ReceiptID.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Create the file path
                var filePath = Path.Combine(folderPath, invoiceReceiptUpload.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await invoiceReceiptUpload.File.CopyToAsync(stream);
                }

                // Update the InvoiceReceipt with file information
                existingInvoice.FilePath = filePath;
                existingInvoice.ReceiptName = Path.GetFileName(invoiceReceiptUpload.File.FileName);
                existingInvoice.FileExtension = Path.GetExtension(invoiceReceiptUpload.File.FileName).ToLower();
                existingInvoice.LastUpdated = DateTime.Now;

                var httpRequest = httpContextAccessor.HttpContext.Request;
                var url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/wwwroot/Uploads/{existingInvoice.ReceiptID}/{invoiceReceiptUpload.File.FileName}";

                existingInvoice.UrlPath = url;
            }

            // Update the existingExpense and existingInvoice entities with new values
            dbContext.Entry(existingInvoice).CurrentValues.SetValues(existingInvoice); // Make sure we use existingInvoice values
            dbContext.Entry(existingExpense).CurrentValues.SetValues(updatedExpenses); // Set the new expense values to the existing expense
            // Ensure InvoiceReceiptID is not changed                                                                           
            existingExpense.InvoiceReceiptID = existingInvoice.ReceiptID;

            await dbContext.SaveChangesAsync();
            return existingExpense;
        }
    }
}
