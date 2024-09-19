namespace ExpensePilot.Services.AdminAPI.Models.DTO
{
    public class ExpenseStatusDto
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
