namespace Lab.Models {
	public class Demand {
		public int Id { get; set; }
		public Test Test { get; set; }
		public string DemandName { get; set; }
		public float LowerBorder { get; set; }
		public float UpperBorder { get; set; }
	}
}
