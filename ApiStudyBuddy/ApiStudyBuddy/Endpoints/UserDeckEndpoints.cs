using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class UserDeckEndpoints
{
    public static void MapUserDeckEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UserDeck").WithTags(nameof(UserDeck));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.UserDecks
            .Include(x => x.User)
            .Include(x => x.Deck)
            .ToListAsync();
        })
        .WithName("GetAllUserDecks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<UserDeck>, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDecks
                .Include(x => x.Deck)
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserId == userid)
                is UserDeck model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserDeckById")
        .WithOpenApi();

        group.MapGet("/maui/user/{userId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDecks
            .Include(x => x.User)
            .Include(x => x.Deck)
            .ThenInclude(x => x.DeckFlashCards)
            .ThenInclude(x => x.FlashCard)
            .Where(x => x.UserId == userid)
            .ToListAsync();
        })
        .WithName("GetAllUserDecksbyUserId")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, UserDeck userDeck, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDecks
                .Where(model => model.UserId == userid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.UserId, userDeck.UserId)
                    .SetProperty(m => m.DeckId, userDeck.DeckId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUserDeck")
        .WithOpenApi();

        group.MapPost("/", async (UserDeck userDeck, ApiStudyBuddyContext db) =>
        {
            db.UserDecks.Add(userDeck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/UserDeck/{userDeck.UserId}", userDeck);
        })
        .WithName("CreateUserDeck")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDecks
                .Where(model => model.UserId == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUserDeck")
        .WithOpenApi();
    }
}
