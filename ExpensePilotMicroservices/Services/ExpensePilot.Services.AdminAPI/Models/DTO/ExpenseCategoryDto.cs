namespace ExpensePilot.Services.AdminAPI.Models.DTO
{
    public class ExpenseCategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
