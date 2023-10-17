using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStudyBuddy.Models
{
	public class DeckFlashCard
	{
		public int DeckFlashCardId { get; set; }

		public int DeckId { get; set; }

		public int FlashCardId { get; set; }
	}
}
