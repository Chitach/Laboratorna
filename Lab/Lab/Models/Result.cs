namespace Lab.Models {
	public class Result {
		public int Id { get; set; }
		public Conducting Conducting { get; set; }
		public Demand Demand { get; set; }
		public string Comment { get; set; }
        public float Value { get; set; }
	}
}
