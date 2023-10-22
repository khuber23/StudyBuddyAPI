using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class UserDeckGroupEndpoints
{
    public static void MapUserDeckGroupEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UserDeckGroup").WithTags(nameof(UserDeckGroup));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups
            .Include(x => x.User)
            .Include(x => x.DeckGroup)
            .ToListAsync();
        })
        .WithName("GetAllUserDeckGroups")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<UserDeckGroup>, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups.AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserId == userid)
                is UserDeckGroup model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserDeckGroupById")
        .WithOpenApi();

        group.MapGet("/maui/user/{userId}", async ( int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups
            .Include(x => x.User)
            .Include(x => x.DeckGroup)
            .ThenInclude(x => x.DeckGroupDecks)
            .ThenInclude(x => x.Deck)
            .ThenInclude(x => x.DeckFlashCards)
            .ThenInclude(x => x.FlashCard)
            .Where(x => x.UserId == userid)
            .ToListAsync();
        })
        .WithName("GetAllUserDeckGroupsbyUserId")
        .WithOpenApi();

        group.MapGet("/maui/deckgroup/{userId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups
            .Include(x => x.User)
            .Include(x => x.DeckGroup)
            .Where(x => x.UserId == userid)
            .ToListAsync();
        })
       .WithName("GetAllUserDeckGroupsjustdeckgroup")
       .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, UserDeckGroup userDeckGroup, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDeckGroups
                .Where(model => model.UserId == userid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.UserId, userDeckGroup.UserId)
                    .SetProperty(m => m.DeckGroupId, userDeckGroup.DeckGroupId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUserDeckGroup")
        .WithOpenApi();

        group.MapPost("/", async (UserDeckGroup userDeckGroup, ApiStudyBuddyContext db) =>
        {
            db.UserDeckGroups.Add(userDeckGroup);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/UserDeckGroup/{userDeckGroup.UserId}", userDeckGroup);
        })
        .WithName("CreateUserDeckGroup")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDeckGroups
                .Where(model => model.UserId == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUserDeckGroup")
        .WithOpenApi();
    }
}
