﻿// <auto-generated />
using System;
using ApiStudyBuddy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiStudyBuddy.Migrations
{
    [DbContext(typeof(ApiStudyBuddyContext))]
    partial class ApiStudyBuddyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiStudyBuddy.Models.Deck", b =>
                {
                    b.Property<int>("DeckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeckId"));

                    b.Property<string>("DeckDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeckName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeckId");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckFlashCard", b =>
                {
                    b.Property<int>("DeckFlashCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeckFlashCardId"));

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<int>("FlashCardId")
                        .HasColumnType("int");

                    b.HasKey("DeckFlashCardId");

                    b.HasIndex("DeckId");

                    b.HasIndex("FlashCardId");

                    b.ToTable("DeckFlashCards");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckGroup", b =>
                {
                    b.Property<int>("DeckGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeckGroupId"));

                    b.Property<string>("DeckGroupDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeckGroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeckGroupId");

                    b.ToTable("DeckGroups");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckGroupDeck", b =>
                {
                    b.Property<int>("DeckGroupDeckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeckGroupDeckId"));

                    b.Property<int>("DeckGroupId")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.HasKey("DeckGroupDeckId");

                    b.HasIndex("DeckGroupId");

                    b.HasIndex("DeckId");

                    b.ToTable("DeckGroupDecks");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.FlashCard", b =>
                {
                    b.Property<int>("FlashCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlashCardId"));

                    b.Property<string>("FlashCardAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlashCardAnswerImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlashCardQuestion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlashCardQuestionImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("StudySessionFlashCardId")
                        .HasColumnType("int");

                    b.HasKey("FlashCardId");

                    b.HasIndex("StudySessionFlashCardId");

                    b.ToTable("FlashCards");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySession", b =>
                {
                    b.Property<int>("StudySessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudySessionId"));

                    b.Property<int?>("DeckGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("DeckId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StudySessionId");

                    b.HasIndex("DeckGroupId");

                    b.HasIndex("DeckId");

                    b.HasIndex("UserId");

                    b.ToTable("StudySessions");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySessionFlashCard", b =>
                {
                    b.Property<int>("StudySessionFlashCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudySessionFlashCardId"));

                    b.Property<int>("StudySessionId")
                        .HasColumnType("int");

                    b.HasKey("StudySessionFlashCardId");

                    b.HasIndex("StudySessionId");

                    b.ToTable("StudySessionsFlashCards");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.UserDeck", b =>
                {
                    b.Property<int>("UserDeckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserDeckId"));

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserDeckId");

                    b.HasIndex("DeckId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDecks");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.UserDeckGroup", b =>
                {
                    b.Property<int>("UserDeckGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserDeckGroupId"));

                    b.Property<int>("DeckGroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserDeckGroupId");

                    b.HasIndex("DeckGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDeckGroups");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckFlashCard", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.Deck", null)
                        .WithMany("DeckFlashCards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiStudyBuddy.Models.FlashCard", null)
                        .WithMany("DeckFlashCard")
                        .HasForeignKey("FlashCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckGroupDeck", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.DeckGroup", null)
                        .WithMany("DeckGroupDecks")
                        .HasForeignKey("DeckGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiStudyBuddy.Models.Deck", null)
                        .WithMany("DeckGroupDecks")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.FlashCard", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.StudySessionFlashCard", null)
                        .WithMany("FlashCards")
                        .HasForeignKey("StudySessionFlashCardId");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySession", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.DeckGroup", null)
                        .WithMany("StudySessions")
                        .HasForeignKey("DeckGroupId");

                    b.HasOne("ApiStudyBuddy.Models.Deck", null)
                        .WithMany("StudySessions")
                        .HasForeignKey("DeckId");

                    b.HasOne("ApiStudyBuddy.Models.User", null)
                        .WithMany("StudySessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySessionFlashCard", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.StudySession", null)
                        .WithMany("StudySessionFlashCards")
                        .HasForeignKey("StudySessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.UserDeck", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.Deck", null)
                        .WithMany("UserDecks")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiStudyBuddy.Models.User", null)
                        .WithMany("UserDecks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.UserDeckGroup", b =>
                {
                    b.HasOne("ApiStudyBuddy.Models.DeckGroup", null)
                        .WithMany("UserDeckGroups")
                        .HasForeignKey("DeckGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiStudyBuddy.Models.User", null)
                        .WithMany("UserDeckGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.Deck", b =>
                {
                    b.Navigation("DeckFlashCards");

                    b.Navigation("DeckGroupDecks");

                    b.Navigation("StudySessions");

                    b.Navigation("UserDecks");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.DeckGroup", b =>
                {
                    b.Navigation("DeckGroupDecks");

                    b.Navigation("StudySessions");

                    b.Navigation("UserDeckGroups");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.FlashCard", b =>
                {
                    b.Navigation("DeckFlashCard");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySession", b =>
                {
                    b.Navigation("StudySessionFlashCards");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.StudySessionFlashCard", b =>
                {
                    b.Navigation("FlashCards");
                });

            modelBuilder.Entity("ApiStudyBuddy.Models.User", b =>
                {
                    b.Navigation("StudySessions");

                    b.Navigation("UserDeckGroups");

                    b.Navigation("UserDecks");
                });
#pragma warning restore 612, 618
        }
    }
}
