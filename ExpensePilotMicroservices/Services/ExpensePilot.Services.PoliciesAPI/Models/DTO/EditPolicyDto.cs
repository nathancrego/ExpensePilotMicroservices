namespace ExpensePilot.API.Models.DTO.Policies
{
    public class EditPolicyDto
    {
        public string PolicyName { get; set; }
        public string PolicyPurpose { get; set; }
        public string PolicyDescription { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
