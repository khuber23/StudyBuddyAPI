namespace ApiStudyBuddy.Models
{
	public class StudySessionFlashCard
	{
		public int StudySessionFlashCardId { get; set; }

		public int StudySessionId { get; set; }

		public List<FlashCard>? FlashCards { get; set; }
	}
}
