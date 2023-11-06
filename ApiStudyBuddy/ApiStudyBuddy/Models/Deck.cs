using Microsoft.OpenApi.Attributes;

namespace ApiStudyBuddy.Models
{
	public class Deck
	{
		public int DeckId { get; set; }

		[Display("Deck Name")]
		public string? DeckName { get; set; }

		[Display("Deck Description")]
		public string? DeckDescription { get; set; }

        public bool IsPublic { get; set; }

        public bool ReadOnly { get; set; }


        public List<UserDeck>? UserDecks { get; set; }

		public List<StudySession>? StudySessions { get; set; }

		public List<DeckFlashCard>? DeckFlashCards { get; set; }

		public List<DeckGroupDeck>? DeckGroupDecks { get; set; }
	}
}
