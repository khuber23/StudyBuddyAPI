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



        group.MapGet("/{id}", async Task<Results<Ok<UserDeckGroup>, NotFound>> (int userdeckgroupid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups.AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserDeckGroupId == userdeckgroupid)
                is UserDeckGroup model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserDeckGroupById")
        .WithOpenApi();

        //full userDeckGroup for maui, gets literally all/most of the info needed.
        group.MapGet("maui/user/{UserId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups.AsNoTracking()
            .Include(model => model.User)
            .Include(model => model.DeckGroup)
            .ThenInclude(model => model.DeckGroupDeck)
            .ThenInclude(model => model.Deck)
            .ThenInclude(model => model.DeckFlashCards)
            .ThenInclude(model => model.FlashCard)
            .Where(model => model.UserId == userid)
            .ToListAsync();
        })
        .WithName("GetMauiFullUserDeckGroupByUserId")
        .WithOpenApi();

        //basically just gets deckgroup and not all the stuff with it...mostly used for the MAUI DeckGroup page.
        group.MapGet("maui/deckgroup/{UserId}", async (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.UserDeckGroups.AsNoTracking()
            .Include(model => model.User)
            .Include(model => model.DeckGroup)
            .Where(model => model.UserId == userid)
            .ToListAsync();
        })
       .WithName("GetMauiOnlyUserDeckGroupByUserId")
       .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userdeckgroupid, UserDeckGroup userDeckGroup, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDeckGroups
                .Where(model => model.UserDeckGroupId == userdeckgroupid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.UserDeckGroupId, userDeckGroup.UserDeckGroupId)
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
            return TypedResults.Created($"/api/UserDeckGroup/{userDeckGroup.UserDeckGroupId}", userDeckGroup);
        })
        .WithName("CreateUserDeckGroup")
        .WithOpenApi();



        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userdeckgroupid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.UserDeckGroups
                .Where(model => model.UserDeckGroupId == userdeckgroupid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUserDeckGroup")
        .WithOpenApi();
    }
}
