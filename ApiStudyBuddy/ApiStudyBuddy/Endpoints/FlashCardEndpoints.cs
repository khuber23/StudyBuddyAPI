using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class FlashCardEndpoints
{
    public static void MapFlashCardEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/FlashCard").WithTags(nameof(FlashCard));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.FlashCards.ToListAsync();
        })
        .WithName("GetAllFlashCards")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<FlashCard>, NotFound>> (int flashcardid, ApiStudyBuddyContext db) =>
        {
            return await db.FlashCards.AsNoTracking()
                .FirstOrDefaultAsync(model => model.FlashCardId == flashcardid)
                is FlashCard model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFlashCardById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int flashcardid, FlashCard flashCard, ApiStudyBuddyContext db) =>
        {
            var affected = await db.FlashCards
                .Where(model => model.FlashCardId == flashcardid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.FlashCardId, flashCard.FlashCardId)
                    .SetProperty(m => m.FlashCardQuestion, flashCard.FlashCardQuestion)
                    .SetProperty(m => m.FlashCardQuestionImage, flashCard.FlashCardQuestionImage)
                    .SetProperty(m => m.FlashCardAnswer, flashCard.FlashCardAnswer)
                    .SetProperty(m => m.FlashCardAnswerImage, flashCard.FlashCardAnswerImage)
                    .SetProperty(m => m.IsCorrect, flashCard.IsCorrect)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFlashCard")
        .WithOpenApi();

        group.MapPost("/", async (FlashCard flashCard, ApiStudyBuddyContext db) =>
        {
            db.FlashCards.Add(flashCard);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/FlashCard/{flashCard.FlashCardId}", flashCard);
        })
        .WithName("CreateFlashCard")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int flashcardid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.FlashCards
                .Where(model => model.FlashCardId == flashcardid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFlashCard")
        .WithOpenApi();
    }
}
