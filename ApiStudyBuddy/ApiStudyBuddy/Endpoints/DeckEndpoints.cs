using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Deck").WithTags(nameof(Deck));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.Decks
            .Include(x => x.UserDecks)
            .Include(x => x.DeckFlashCards)
            .ThenInclude(f => f.FlashCard)
            .Include(x => x.DeckGroupDecks)
            .Include(x => x.StudySessions)
            .ToListAsync();
        })
        .WithName("GetAllDecks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Deck>, NotFound>> (int deckid, ApiStudyBuddyContext db) =>
        {
            return await db.Decks
            .Include(x => x.DeckFlashCards)
            .ThenInclude(d => d.FlashCard)
            .AsNoTracking()
                .FirstOrDefaultAsync(model => model.DeckId == deckid)
                is Deck model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeckById")
        .WithOpenApi();

        //needed to add a way to retrieve a deck by the deck name. Also needed to do /deckname/{deckname} since it was too similiar to just do /{deckname}
        group.MapGet("/deckname/{deckname}", async Task<Results<Ok<Deck>, NotFound>> (string deckName, ApiStudyBuddyContext db) =>
        {
            return await db.Decks
            .Include(x => x.DeckFlashCards)
            .ThenInclude(d => d.FlashCard)
            .AsNoTracking()
                .FirstOrDefaultAsync(model => model.DeckName == deckName)
                is Deck model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
       .WithName("GetDeckByDeckName")
       .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int deckid, Deck deck, ApiStudyBuddyContext db) =>
        {
            var affected = await db.Decks
                .Where(model => model.DeckId == deckid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DeckName, deck.DeckName)
                    .SetProperty(m => m.DeckDescription, deck.DeckDescription)
                    .SetProperty(m => m.IsPublic, deck.IsPublic)
                    .SetProperty(m => m.ReadOnly, deck.ReadOnly)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeck")
        .WithOpenApi();

        group.MapPost("/", async (Deck deck, ApiStudyBuddyContext db) =>
        {
            db.Decks.Add(deck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Deck/{deck.DeckId}", deck);
        })
        .WithName("CreateDeck")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int deckid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.Decks
                .Where(model => model.DeckId == deckid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeck")
        .WithOpenApi();
    }
}
