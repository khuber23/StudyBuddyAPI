namespace ApiStudyBuddy.Models
{
	public class FlashCard
	{
		public int FlashCardId { get; set; }

		public string? FlashCardQuestion { get; set; }
		public string? FlashCardQuestionImage { get; set; }


		public string? FlashCardAnswer { get; set; }
		public string? FlashCardAnswerImage { get; set; }

        public bool IsCorrect { get; set; }


        public List<DeckFlashCard>? DeckFlashCard { get; set; }
	}
}
