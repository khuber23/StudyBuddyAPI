namespace ApiStudyBuddy.Models
{
	public class StudySession
	{
		public int StudySessionId { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public bool IsCompleted { get; set; }
		


		public int? DeckGroupId { get; set; }

		public int? DeckId { get; set; }

		public int UserId { get; set; }

		public List<StudySessionFlashCard>? StudySessionFlashCards { get; set; }
	}
}
