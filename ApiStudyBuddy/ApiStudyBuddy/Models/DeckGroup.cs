using Microsoft.OpenApi.Attributes;

namespace ApiStudyBuddy.Models
{
	public class DeckGroup
	{
		public int DeckGroupId { get; set; }

		[Display("Deck Group Name")]
		public string? DeckGroupName { get; set; }

		[Display("Deck Group Description")]
		public string? DeckGroupDescription { get; set; }





		public List<StudySession>? StudySessions { get; set; }

		public List<UserDeckGroup>? UserDeckGroups { get; set; }

		public List<DeckGroupDeck>? DeckGroupDecks { get; set; }

		public DeckGroupDeck? DeckGroupDeck { get; set; }
	}
}
