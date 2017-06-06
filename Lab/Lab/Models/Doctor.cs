using System.Collections.Generic;

namespace Lab.Models {
	public class Doctor {
		public int Id { get; set; }
		public User User { get; set; }
		public Specialization Specialization { get; set; }
		public virtual ICollection<DoctorsPatiens> Patients { get; set; }
	}
}
