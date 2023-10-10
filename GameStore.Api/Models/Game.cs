// Copyright (c) 2023 Mohammed Hamada, GitHub: M7amd7amada.
// Licensed under MIT license. See LICENSE file in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class Game
{
    public int GameId { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required]
    [StringLength(50)]
    public required string Genre { get; set; }

    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}