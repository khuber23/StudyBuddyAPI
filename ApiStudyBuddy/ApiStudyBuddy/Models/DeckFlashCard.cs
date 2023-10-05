namespace ApiStudyBuddy.Models
{
	public class DeckFlashCard
	{
		public int DeckFlashCardId { get; set; }

		public int DeckId { get; set; }

		public int FlashCardId { get; set; }

		public Deck? Deck { get; set; }

		public List<FlashCard>? FlashCards { get; set; }
	}
}
