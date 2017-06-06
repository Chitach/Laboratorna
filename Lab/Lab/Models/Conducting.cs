using System;

namespace Lab.Models {
	public class Conducting {
		public int Id { get; set; }
		public Test Test { get; set; }
		public Patient Patient { get; set; }
		public DateTime TestDate { get; set; }
		public string Comments { get; set; }
	}
}
