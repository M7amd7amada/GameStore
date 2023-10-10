// Copyright (c) 2023 Mohammed Hamada, GitHub: M7amd7amada.
// Licensed under MIT license. See LICENSE file in the project root for license information.

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

const string GetGameEndpointRouteName = "GetGame";

List<Game> games = new() {
    new Game
    {
        GameId = 1,
        Name = "The Witcher 3: Wild Hunt",
        Genre = "Action RPG",
        ImageUri = "https://placehold.co/100",
        Price = 29.99M,
        ReleaseDate = new DateTime(2015, 5, 19)
    },
    new Game
    {
        GameId = 2,
        Name = "Red Dead Redemption 2",
        Genre = "Action-Adventure",
        ImageUri = "https://placehold.co/100",
        Price = 39.99M,
        ReleaseDate = new DateTime(2018, 10, 26)
    },
    new Game
    {
        GameId = 3,
        Name = "The Legend of Zelda: Breath of the Wild",
        Genre = "Action-Adventure",
        ImageUri = "https://placehold.co/100",
        Price = 49.99M,
        ReleaseDate = new DateTime(2017, 3, 3)
    }
};


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/games");
group.MapGet("/", () => games);
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(g => g.GameId == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName(GetGameEndpointRouteName);

group.MapPost("/", (Game game) =>
{
    game.GameId = games.Max(g => g.GameId) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointRouteName, new { id = game.GameId }, game);
});

group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? game = games.Find(g => g.GameId == id);

    if (game is null)
    {
        return Results.NotFound();
    }

    game.Name = updatedGame.Name;
    game.Genre = updatedGame.Genre;
    game.ImageUri = updatedGame.ImageUri;
    game.Price = updatedGame.Price;
    game.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(g => g.GameId == id);
    if (game is not null)
    {
        games.Remove(game);
    }
    return Results.NoContent();
});

app.Run();
