using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Models;

namespace ApiStudyBuddy.Data
{
    public class ApiStudyBuddyContext : DbContext
    {
        public ApiStudyBuddyContext (DbContextOptions<ApiStudyBuddyContext> options)
            : base(options)
        {
        }
		public DbSet<User> Users { get; set; }
		public DbSet<StudySession> StudySessions { get; set; }
		public DbSet<Deck> Decks { get; set; }
		public DbSet<DeckGroup> DeckGroups { get; set; }
		public DbSet<FlashCard> FlashCards { get; set; }

		// Transitional Tables 
		public DbSet<UserDeck> UserDecks { get; set; }
		public DbSet<UserDeckGroup> UserDeckGroups { get; set; }
		public DbSet<DeckGroupDeck> DeckGroupDecks { get; set; }
		public DbSet<DeckFlashCard> DeckFlashCards { get; set; }
		public DbSet<StudySessionFlashCard> StudySessionsFlashCards { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);



			//// Create data for database
			//modelBuilder.Entity<User>().HasData(
			//	new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "JohnDoe@gmail.com", Username = "JDoe1", Password = "1234" },
			//	new User { UserId = 2, FirstName = "Mary", LastName = "Jane", Email = "MaryJane@gmail.com", Username = "MJane1", Password = "4321" },
   //             new User { UserId = 3, FirstName = "Kayla", LastName = "Huber", Email = "kayla.huber23@gmail.com", Username = "Khuber", Password = "Kitkat23!" }

   //             );

			//modelBuilder.Entity<StudySession>().HasData(
			//	new StudySession
			//	{
			//		StudySessionId = 1,
			//		StartTime = DateTime.Parse("09/11/2023 03:05:15 PM"),
			//		EndTime = DateTime.Parse("09/11/2023 03:35:15 PM"),
			//		DeckId = 1,
			//		UserId = 1,
			//		DeckGroupId = 1
			//	}
			//	);

			//modelBuilder.Entity<Deck>().HasData(
			//	new Deck { DeckId = 1, DeckName = "Creational Design Patterns", DeckDescription = "Design patterns all about class instantiation" },
			//	new Deck { DeckId = 2, DeckName = "Structural Design Patterns", DeckDescription = "Design patterns all about class and Object composition" },
			//	new Deck { DeckId = 3, DeckName = "Behavorial Design Patterns", DeckDescription = "Design patterns all about Class's objects communication" }
			//	);

			//modelBuilder.Entity<DeckGroup>().HasData(
			//	new DeckGroup { DeckGroupId = 1, DeckGroupName = "Design Patterns", DeckGroupDescription = "Solutions to commonly occurring problems in software design." }
			//	);

			//modelBuilder.Entity<FlashCard>().HasData(
			//	new FlashCard { FlashCardId = 1, FlashCardQuestion = "What is abstract factory", FlashCardAnswer = "Creates an instance of several families of classes" },
			//	new FlashCard { FlashCardId = 2, FlashCardQuestion = "What is Singleton?", FlashCardAnswer = "A class of which only a single instance can exist" },
			//	new FlashCard { FlashCardId = 3, FlashCardQuestion = "What is decorator?", FlashCardAnswer = "Add responsibilites to objects dynamically" },
			//	new FlashCard { FlashCardId = 4, FlashCardQuestion = "What is facade?", FlashCardAnswer = "A single class that represents an entire subsystem" },
			//	new FlashCard { FlashCardId = 5, FlashCardQuestion = "What is iterator?", FlashCardAnswer = "Sequentially access the elements of a collection" }
			//	);

			//modelBuilder.Entity<UserDeck>().HasData(
			//	new UserDeck { UserDeckId = 1, UserId = 1, DeckId = 1 },
   //             new UserDeck { UserDeckId = 2, UserId = 1, DeckId = 2 },
   //             new UserDeck { UserDeckId = 3, UserId = 1, DeckId = 3 }


   //             );

			//modelBuilder.Entity<UserDeckGroup>().HasData(
			//	new UserDeckGroup { UserDeckGroupId = 1, UserId = 1, DeckGroupId = 1 }
			//	);

			//modelBuilder.Entity<DeckGroupDeck>().HasData(
			//	new DeckGroupDeck { DeckGroupDeckId = 1, DeckGroupId = 1, DeckId = 1 },
   //             new DeckGroupDeck { DeckGroupDeckId = 2, DeckGroupId = 1, DeckId = 2 },
   //             new DeckGroupDeck { DeckGroupDeckId = 3, DeckGroupId = 1, DeckId = 3 }

   //             );

			//modelBuilder.Entity<DeckFlashCard>().HasData(
			//	new DeckFlashCard { DeckFlashCardId = 1, DeckId = 1, FlashCardId = 1, },
   //             new DeckFlashCard { DeckFlashCardId = 2, DeckId = 1, FlashCardId = 2, },
   //             new DeckFlashCard { DeckFlashCardId = 3, DeckId = 2, FlashCardId = 3, },
   //             new DeckFlashCard { DeckFlashCardId = 4, DeckId = 2, FlashCardId = 4, },
   //             new DeckFlashCard { DeckFlashCardId = 5, DeckId = 3, FlashCardId = 5, }

   //             );

			//modelBuilder.Entity<StudySessionFlashCard>().HasData(
			//	new StudySessionFlashCard { StudySessionFlashCardId = 1, StudySessionId = 1, FlashCardId = 1, IsCorrect = true },
   //             new StudySessionFlashCard { StudySessionFlashCardId = 2, StudySessionId = 1, FlashCardId = 2, IsCorrect = false }
   //             );

		}
	}
}
