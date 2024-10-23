namespace ContractMonthlyClaimSystem1.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; } // Ensure you have this property
        public decimal HoursWorked { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string AdditionalNotes { get; set; }
        public string DocumentPath { get; set; } // Path for the uploaded document
    }


}
