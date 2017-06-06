namespace Lab.Models {
	public class Demand {
		public int Id { get; set; }
		public Test Test { get; set; }
		public string DemandName { get; set; }
		public int LowerBorder { get; set; }
		public int UpperBorder { get; set; }
	}
}
