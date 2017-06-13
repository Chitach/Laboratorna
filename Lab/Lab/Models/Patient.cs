using System.Collections.Generic;

namespace Lab.Models {
	public class Patient: User {
		public int Height { get; set; }
		public int Weight { get; set; }
		public int Antigen { get; set; }
		public int BloodGroup { get; set; }
		public virtual ICollection<DoctorsPatiens> Doctors { get; set; } 
	}
}
