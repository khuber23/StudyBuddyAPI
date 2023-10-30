using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class DeckGroupDeckEndpoints
{
    public static void MapDeckGroupDeckEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/DeckGroupDeck").WithTags(nameof(DeckGroupDeck));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroupDecks
            .Include(x => x.Deck)
            .Include(x => x.DeckGroup)
            .ToListAsync();
        })
        .WithName("GetAllDeckGroupDecks")
        .WithOpenApi();

        group.MapGet("/maui/{deckgroupid}", async (int deckgroupid, ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroupDecks.AsNoTracking()
            .Include(model => model.DeckGroup)
            .Include(model => model.Deck)
            .ThenInclude(model => model.DeckFlashCards)
            .ThenInclude(model => model.FlashCard)
            .Where(model => model.DeckGroupId == deckgroupid)
            .ToListAsync();
        })
        .WithName("GetmauiDeckGroupDeckByDeckGroupId")
        .WithOpenApi();

        //returns my specific deckGroupDeck by deckgroupDeck Id
        group.MapGet("/maui/specificdeckgroupdeck/{deckgroupid}/{deckid}", async Task<Results<Ok<DeckGroupDeck>, NotFound>>(int deckId, int deckGroupId, ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroupDecks.AsNoTracking()
            .Include(model => model.DeckGroup)
            .Include(model => model.Deck)
            .ThenInclude(model => model.DeckFlashCards)
            .ThenInclude(model => model.FlashCard)
            .FirstOrDefaultAsync(model => model.DeckId == deckId && model.DeckGroupId == deckGroupId)
            is DeckGroupDeck model 
            ? TypedResults.Ok(model)
            : TypedResults.NotFound();
        })
      .WithName("GetmauiDeckGroupDeckByDeckGroupIdDeckId")
      .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<DeckGroupDeck>, NotFound>> (int deckgroupid, ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroupDecks.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DeckGroupId == deckgroupid)
                is DeckGroupDeck model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeckGroupDeckById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int deckgroupid, DeckGroupDeck deckGroupDeck, ApiStudyBuddyContext db) =>
        {
            var affected = await db.DeckGroupDecks
                .Where(model => model.DeckGroupId == deckgroupid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DeckGroupId, deckGroupDeck.DeckGroupId)
                    .SetProperty(m => m.DeckId, deckGroupDeck.DeckId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeckGroupDeck")
        .WithOpenApi();

        group.MapPost("/", async (DeckGroupDeck deckGroupDeck, ApiStudyBuddyContext db) =>
        {
            db.DeckGroupDecks.Add(deckGroupDeck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/DeckGroupDeck/{deckGroupDeck.DeckGroupId}", deckGroupDeck);
        })
        .WithName("CreateDeckGroupDeck")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int deckgroupid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.DeckGroupDecks
                .Where(model => model.DeckGroupId == deckgroupid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeckGroupDeck")
        .WithOpenApi();
    }
}
