// Copyright (c) 2023 Mohammed Hamada, GitHub: M7amd7amada.
// Licensed under MIT license. See LICENSE file in the project root for license information.
namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    static List<Game> games = new() {
        new Game
        {
            GameId = 0,
            Name = "The Witcher 2: Wild Hunt",
            Genre = "Action RPG",
            ImageUri = "https://placehold.co/99",
            Price = 28.99M,
            ReleaseDate = new DateTime(2014, 5, 19)
        },
        new Game
        {
            GameId = 1,
            Name = "Red Dead Redemption 1",
            Genre = "Action-Adventure",
            ImageUri = "https://placehold.co/99",
            Price = 38.99M,
            ReleaseDate = new DateTime(2017, 10, 26)
        },
        new Game
        {
            GameId = 2,
            Name = "The Legend of Zelda: Breath of the Wild",
            Genre = "Action-Adventure",
            ImageUri = "https://placehold.co/99",
            Price = 48.99M,
            ReleaseDate = new DateTime(2016, 3, 3)
        }
    };

    const string GetGameEndpointRouteName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games")
                        .WithParameterValidation();

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

        return group;
    }
}