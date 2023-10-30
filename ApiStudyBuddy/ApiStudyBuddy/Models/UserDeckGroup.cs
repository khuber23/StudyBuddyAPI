namespace ApiStudyBuddy.Models
{
	public class UserDeckGroup
	{
		public int UserId { get; set; }

        public User? User { get; set; }

        public int DeckGroupId { get; set; }

		public DeckGroup? DeckGroup { get; set; }
	}
}
