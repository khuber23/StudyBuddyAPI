using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class StudySessionFlashCardEndpoints
{
    public static void MapStudySessionFlashCardEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/StudySessionFlashCard").WithTags(nameof(StudySessionFlashCard));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.StudySessionsFlashCards
            .Include(x => x.StudySession)
            .Include(x => x.FlashCard)
            .ToListAsync();
        })
        .WithName("GetAllStudySessionFlashCards")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<StudySessionFlashCard>, NotFound>> (int studysessionid, ApiStudyBuddyContext db) =>
        {
            return await db.StudySessionsFlashCards.AsNoTracking()
                .FirstOrDefaultAsync(model => model.StudySessionId == studysessionid)
                is StudySessionFlashCard model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudySessionFlashCardById")
        .WithOpenApi();


        group.MapGet("maui/full/{UserId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.StudySessionsFlashCards.AsNoTracking()
                .Include(x => x.StudySession)
                .ThenInclude(x => x.User)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.DeckGroup)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.Deck)
                .Include(x => x.FlashCard)
                .Where(x => x.StudySession.UserId == userid).ToListAsync();
        })
        .WithName("GetMauiFullStudySessionFlashCardsByUserId")
        .WithOpenApi();


        group.MapGet("maui/incorrect/{UserId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.StudySessionsFlashCards.AsNoTracking()
                .Include(x => x.StudySession)
                .ThenInclude(x => x.User)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.DeckGroup)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.Deck)
                .Include(x => x.FlashCard)
                .Where(x => x.StudySession.UserId == userid && x.IsCorrect == false)
                .ToListAsync();
        })
        .WithName("GetMauiIncorrectStudySessionFlashCardsByUserId")
        .WithOpenApi();

        group.MapGet("maui/correct/{UserId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.StudySessionsFlashCards.AsNoTracking()
                .Include(x => x.StudySession)
                .ThenInclude(x => x.User)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.DeckGroup)
                .Include(x => x.StudySession)
                .ThenInclude(x => x.Deck)
                .Include(x => x.FlashCard)
                .Where(x => x.StudySession.UserId == userid && x.IsCorrect == true)
                .ToListAsync();
        })
        .WithName("GetMauiCorrectStudySessionFlashCardsByUserId")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int studysessionid, int flashcardid, StudySessionFlashCard studySessionFlashCard, ApiStudyBuddyContext db) =>
        {
            var affected = await db.StudySessionsFlashCards
                .Where(model => model.StudySessionId == studysessionid && model.FlashCardId == flashcardid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IsCorrect, studySessionFlashCard.IsCorrect)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudySessionFlashCard")
        .WithOpenApi();

        group.MapPost("/", async (StudySessionFlashCard studySessionFlashCard, ApiStudyBuddyContext db) =>
        {
            db.StudySessionsFlashCards.Add(studySessionFlashCard);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/StudySessionFlashCard/{studySessionFlashCard.StudySessionId}", studySessionFlashCard);
        })
        .WithName("CreateStudySessionFlashCard")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int studysessionid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.StudySessionsFlashCards
                .Where(model => model.StudySessionId == studysessionid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudySessionFlashCard")
        .WithOpenApi();
    }
}
