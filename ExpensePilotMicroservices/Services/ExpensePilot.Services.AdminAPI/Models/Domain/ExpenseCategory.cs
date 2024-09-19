using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AdminAPI.Models.Domain
{
    public class ExpenseCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
