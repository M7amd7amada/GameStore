// Copyright (c) 2023 Mohammed Hamada, GitHub: M7amd7amada.
// Licensed under MIT license. See LICENSE file in the project root for license information.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoint();
app.Run();
