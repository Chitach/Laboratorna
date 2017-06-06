using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Lab.Models {
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class User {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string Lastname { get; set; }
		public DateTime BirthDate { get; set; }
		public string Address { get; set; }
		public bool IsMale { get; set; }
	}
}
