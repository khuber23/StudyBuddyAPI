using System.ComponentModel.DataAnnotations;

namespace ApiStudyBuddy.Models
{
	public class User
	{
		public int UserId { get; set; }

		public string? FirstName { get; set; }

        public string? LastName { get; set; }

		public string? Email { get; set; }

		[Required]
		public string? Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? PasswordHash { get; set; }

		public string? ProfilePicture { get; set; }

		public bool IsAdmin { get; set; }

		public List<UserDeck>? UserDecks { get; set; }

		public List<UserDeckGroup>? UserDeckGroups { get; set; }

		public List<StudySession>? StudySessions { get; set; }
	}
}
