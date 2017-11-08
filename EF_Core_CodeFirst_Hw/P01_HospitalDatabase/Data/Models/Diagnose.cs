namespace P01_HospitalDatabase.Data.Models
{
    
    public class Diagnose
    {
        public Diagnose()
        {
            
        }

        public Diagnose(string name, string comments, int patientId)
        {
            this.Name = name;
            this.Comments = comments;
            this.PatientId = patientId;
        }

        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
