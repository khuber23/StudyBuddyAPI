namespace ApiStudyBuddy.Models
{
	public class DeckGroupDeck
	{
		public int DeckGroupId { get; set; }

        public DeckGroup? DeckGroup { get; set; }

        public int DeckId { get; set; }

		public Deck? Deck { get; set; }
	}
}
