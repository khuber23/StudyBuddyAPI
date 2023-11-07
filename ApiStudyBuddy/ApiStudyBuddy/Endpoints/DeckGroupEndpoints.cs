using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class DeckGroupEndpoints
{
    public static void MapDeckGroupEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/DeckGroup").WithTags(nameof(DeckGroup));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroups
            .Include(x => x.UserDeckGroups)
            .Include(x => x.DeckGroupDecks)
            .Include(x => x.StudySessions)
            .ToListAsync();
        })
        .WithName("GetAllDeckGroups")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<DeckGroup>, NotFound>> (int deckgroupid, ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroups.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DeckGroupId == deckgroupid)
                is DeckGroup model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeckGroupById")
        .WithOpenApi();

        //needed to add a way to retrieve a deckgroup by deckgroupname
        group.MapGet("/deckgroup/{deckgroupname}", async Task<Results<Ok<DeckGroup>, NotFound>> (string deckGroupName, ApiStudyBuddyContext db) =>
        {
            return await db.DeckGroups
            .Include(x => x.DeckGroupDecks)
            .ThenInclude(d => d.Deck)
            .ThenInclude(d => d.DeckFlashCards)
            .ThenInclude(d => d.FlashCard)
            .AsNoTracking()
                .FirstOrDefaultAsync(model => model.DeckGroupName == deckGroupName)
                is DeckGroup model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
       .WithName("GetDeckGroupByDeckGroupName")
       .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int deckgroupid, DeckGroup deckGroup, ApiStudyBuddyContext db) =>
        {
            var affected = await db.DeckGroups
                .Where(model => model.DeckGroupId == deckgroupid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DeckGroupName, deckGroup.DeckGroupName)
                    .SetProperty(m => m.DeckGroupDescription, deckGroup.DeckGroupDescription)
                      .SetProperty(m => m.IsPublic, deckGroup.IsPublic)
                    .SetProperty(m => m.ReadOnly, deckGroup.ReadOnly)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeckGroup")
        .WithOpenApi();

        group.MapPost("/", async (DeckGroup deckGroup, ApiStudyBuddyContext db) =>
        {
            db.DeckGroups.Add(deckGroup);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/DeckGroup/{deckGroup.DeckGroupId}", deckGroup);
        })
        .WithName("CreateDeckGroup")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int deckgroupid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.DeckGroups
                .Where(model => model.DeckGroupId == deckgroupid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeckGroup")
        .WithOpenApi();
    }
}
