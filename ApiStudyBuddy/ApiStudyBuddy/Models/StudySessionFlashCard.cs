namespace ApiStudyBuddy.Models
{
	public class StudySessionFlashCard
	{
		public int StudySessionId { get; set; }
        public StudySession? StudySession { get; set; }

        public int FlashCardId { get; set; }

		public FlashCard? FlashCard { get; set; }

        public bool IsCorrect { get; set; }
    }
}
