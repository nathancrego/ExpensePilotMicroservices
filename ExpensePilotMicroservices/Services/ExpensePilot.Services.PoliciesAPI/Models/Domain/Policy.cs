using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.PoliciesAPI.Models.Domain
{
    public class Policy
    {
        [Key]
        public int PolicyID { get; set; }
        public string PolicyName { get; set; }
        public string PolicyPurpose { get; set; }
        public string PolicyDescription { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
