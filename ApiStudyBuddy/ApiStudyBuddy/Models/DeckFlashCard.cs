using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStudyBuddy.Models
{
	public class DeckFlashCard
	{
		public int DeckId { get; set; }

        public Deck? Deck { get; set; }

        public int FlashCardId { get; set; }

		public FlashCard? FlashCard { get; set; }
	}
}
