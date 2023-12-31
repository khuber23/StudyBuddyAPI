﻿using Microsoft.EntityFrameworkCore;
using ApiStudyBuddy.Data;
using ApiStudyBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace ApiStudyBuddy.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        group.MapGet("/", async (ApiStudyBuddyContext db) =>
        {
            return await db.Users
            .Include(x => x.UserDecks)
            .ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/MVC/User", async Task<Results<Ok<User>, NotFound>> (string username, ApiStudyBuddyContext db) =>
        {
            return await db.Users
            .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Username == username)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserByUsername")
        .WithOpenApi();


        group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            return await db.Users
            .Include(x => x.UserDeckGroups)
            .ThenInclude(z => z.DeckGroup)
            .Include(x => x.UserDecks)
            .ThenInclude(y => y.Deck)
            .ThenInclude(f => f.DeckFlashCards)
            .ThenInclude(d => d.FlashCard)
            .AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserId == userid)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, User user, ApiStudyBuddyContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.UserId == userid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.FirstName, user.FirstName)
                    .SetProperty(m => m.LastName, user.LastName)
                    .SetProperty(m => m.Email, user.Email)
                    .SetProperty(m => m.Username, user.Username)
                    .SetProperty(m => m.PasswordHash, user.PasswordHash)
                    .SetProperty(m => m.ProfilePicture, user.ProfilePicture)
                    .SetProperty(m => m.IsAdmin, user.IsAdmin)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        group.MapPost("/", async (User user, ApiStudyBuddyContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.UserId}", user);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, ApiStudyBuddyContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.UserId == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}
