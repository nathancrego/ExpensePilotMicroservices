using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AdminAPI.Models.Domain
{
    public class ExpenseStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
