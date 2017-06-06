namespace Lab.Models {
	public class DoctorsPatiens {
		public int DoctorId { get; set; }
		public int PatientId { get; set; }
		public Doctor Doctor { get; set; }
		public Patient Patient { get; set; }
	}
}
