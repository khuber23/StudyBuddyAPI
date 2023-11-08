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

            // Define the composite key for UserDeckGroups
            modelBuilder.Entity<UserDeckGroup>()
                 .HasKey(u => new { u.UserId, u.DeckGroupId });

            // Define relationship between UserDeckGroups and the Users
            modelBuilder.Entity<UserDeckGroup>()
                .HasOne(udg => udg.User)
                .WithMany(u => u.UserDeckGroups)
                .HasForeignKey(udg => udg.UserId);

            // Define relationship between UserDeckGroups and the DeckGroups
            modelBuilder.Entity<UserDeckGroup>()
                .HasOne(udg => udg.DeckGroup)
                .WithMany(u => u.UserDeckGroups)
                .HasForeignKey(udg => udg.DeckGroupId);

            // =============================================================================

            // Define the composite key for UserDeck
            modelBuilder.Entity<UserDeck>()
                .HasKey(u => new { u.UserId, u.DeckId });

            // Define relationship between UserDecks and the Users
            modelBuilder.Entity<UserDeck>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDecks)
                .HasForeignKey(ud => ud.UserId);

            // Define relationship between UserDecks and the Decks 
            modelBuilder.Entity<UserDeck>()
                .HasOne(ud => ud.Deck)
                .WithMany(u => u.UserDecks)
                .HasForeignKey(ud => ud.DeckId);
            // =============================================================================

            // Define the composite key for DeckGroupDeck
            modelBuilder.Entity<DeckGroupDeck>()
                .HasKey(d => new { d.DeckGroupId, d.DeckId });

            // Define relationship between DeckGroupDeck and DeckGroups
            modelBuilder.Entity<DeckGroupDeck>()
                .HasOne(dgd => dgd.DeckGroup)
                .WithMany(d => d.DeckGroupDecks)
                .HasForeignKey(dgd => dgd.DeckGroupId);

            // Define relationship between DeckGroupDeck and Decks
            modelBuilder.Entity<DeckGroupDeck>()
                .HasOne(dgd => dgd.Deck)
               .WithMany(d => d.DeckGroupDecks)
               .HasForeignKey(dgd => dgd.DeckId);
            // ===============================================================================

            // Define the composite key for DeckFlashCards
            modelBuilder.Entity<DeckFlashCard>()
                .HasKey(d => new { d.DeckId, d.FlashCardId });

            // Define the relationship between DeckFlashCards and Decks
            modelBuilder.Entity<DeckFlashCard>()
                .HasOne(dfc => dfc.Deck)
                .WithMany(d => d.DeckFlashCards)
                .HasForeignKey(dfc => dfc.DeckId);

            // Define the relationship between DeckFlashCards and FlashCards
            modelBuilder.Entity<DeckFlashCard>()
                .HasOne(dfc => dfc.FlashCard)
                .WithMany(d => d.DeckFlashCards)
                .HasForeignKey(dfc => dfc.FlashCardId);
            // ================================================================================

            // Define the composite key for StudySessionFlashCards
            modelBuilder.Entity<StudySessionFlashCard>()
                .HasKey(s => new { s.StudySessionId, s.FlashCardId });

            // Define the relationship between StudySessionFlashCards and StudySession
            modelBuilder.Entity<StudySessionFlashCard>()
                .HasOne(ssf => ssf.StudySession)
                .WithMany(s => s.StudySessionFlashCards)
                .HasForeignKey(ssf => ssf.StudySessionId);

            // Define the relationship between StudySessionFlashCards and FlashCards
            modelBuilder.Entity<StudySessionFlashCard>()
                .HasOne(ssf => ssf.FlashCard)
                .WithMany(s => s.StudySessionFlashCards)
                .HasForeignKey(ssf => ssf.FlashCardId);


            // Create data for database
            modelBuilder.Entity<User>().HasData(
                //password 1234
                new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "JohnDoe@gmail.com", Username = "JDoe1", PasswordHash = "AQAAAAIAAYagAAAAEHkNcdcydJ6lCgu6hPLEdV8CezbujT87yOO2nMMXwe71pTX+CdelWp6WHAD+hNGN3w==", IsAdmin = false },
                //password 4321
                new User { UserId = 2, FirstName = "Mary", LastName = "Jane", Email = "MaryJane@gmail.com", Username = "MJane1", PasswordHash = "AQAAAAIAAYagAAAAEFCYkHw0hLhF5AiysQpkKd5Y1DBCL0iJgPA/dQtXBzrbyuCHNqZOh8Db9rAZg1DrsA==", IsAdmin = false }
                );

            modelBuilder.Entity<UserDeckGroup>().HasData(
                new UserDeckGroup { UserId = 1, DeckGroupId = 1 }
                );

            modelBuilder.Entity<UserDeck>().HasData(
                new UserDeck { UserId = 1, DeckId = 1 },
                new UserDeck { UserId = 1, DeckId = 2 },
                new UserDeck { UserId = 1, DeckId = 3 }
                );

            modelBuilder.Entity<DeckGroupDeck>().HasData(
                new DeckGroupDeck { DeckGroupId = 1, DeckId = 1 },
                new DeckGroupDeck { DeckGroupId = 1, DeckId = 2 },
                new DeckGroupDeck { DeckGroupId = 1, DeckId = 3 }
                );

            modelBuilder.Entity<DeckFlashCard>().HasData(
                new DeckFlashCard { DeckId = 1, FlashCardId = 1, },
                new DeckFlashCard { DeckId = 1, FlashCardId = 2, },
                new DeckFlashCard { DeckId = 2, FlashCardId = 3, },
                new DeckFlashCard { DeckId = 2, FlashCardId = 4, },
                new DeckFlashCard { DeckId = 3, FlashCardId = 5, }
                );

            modelBuilder.Entity<StudySessionFlashCard>().HasData(
                new StudySessionFlashCard { StudySessionId = 1, FlashCardId = 1, IsCorrect = true },
                new StudySessionFlashCard { StudySessionId = 1, FlashCardId = 2, IsCorrect = false }
                );

            modelBuilder.Entity<StudySession>().HasData(
                new StudySession
                {
                    StudySessionId = 1,
                    StartTime = DateTime.Parse("09/11/2023 03:05:15 PM"),
                    EndTime = DateTime.Parse("09/11/2023 03:35:15 PM"),
                    DeckId = 1,
                    UserId = 1,
                    DeckGroupId = 1
                });

            modelBuilder.Entity<Deck>().HasData(
                new Deck { DeckId = 1, DeckName = "Creational Design Patterns", DeckDescription = "Design patterns all about class instantiation", IsPublic = false, ReadOnly = false },
                new Deck { DeckId = 2, DeckName = "Structural Design Patterns", DeckDescription = "Design patterns all about class and Object composition", IsPublic = false, ReadOnly = false },
                new Deck { DeckId = 3, DeckName = "Behavorial Design Patterns", DeckDescription = "Design patterns all about Class's objects communication", IsPublic = false, ReadOnly = false }
                );

            modelBuilder.Entity<DeckGroup>().HasData(
                new DeckGroup { DeckGroupId = 1, DeckGroupName = "Design Patterns", DeckGroupDescription = "Solutions to commonly occurring problems in software design.", IsPublic = false, ReadOnly = false }
                );

            modelBuilder.Entity<FlashCard>().HasData(
                new FlashCard { FlashCardId = 1, FlashCardQuestion = "What is abstract factory", FlashCardQuestionImage = null, FlashCardAnswerImage = null, IsPublic = true, ReadOnly = true, FlashCardAnswer = "Creates an instance of several families of classes" },
                new FlashCard { FlashCardId = 2, FlashCardQuestion = "What is Singleton?", FlashCardQuestionImage = null, FlashCardAnswerImage = null, IsPublic = true, ReadOnly = true, FlashCardAnswer = "A class of which only a single instance can exist" },
                new FlashCard { FlashCardId = 3, FlashCardQuestion = "What is decorator?", FlashCardQuestionImage = null, FlashCardAnswerImage = null, IsPublic = true, ReadOnly = true, FlashCardAnswer = "Add responsibilites to objects dynamically" },
                new FlashCard { FlashCardId = 4, FlashCardQuestion = "What is facade?", FlashCardQuestionImage = null, FlashCardAnswerImage = null, IsPublic = true, ReadOnly = true, FlashCardAnswer = "A single class that represents an entire subsystem" },
                new FlashCard { FlashCardId = 5, FlashCardQuestion = "What is iterator?", FlashCardQuestionImage = null, FlashCardAnswerImage = null, IsPublic = true, ReadOnly = true, FlashCardAnswer = "Sequentially access the elements of a collection" }
                );


        }
    }
}
