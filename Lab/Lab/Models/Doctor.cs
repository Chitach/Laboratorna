using System.Collections.Generic;

namespace Lab.Models {
	public class Doctor : User{
		public Specialization Specialization { get; set; }
		public virtual ICollection<DoctorsPatiens> Patients { get; set; }
	}
}
