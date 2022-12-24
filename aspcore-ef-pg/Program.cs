using aspcore_ef_pg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PennyContext>(
    opt => 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/json", () => new BenchmarkJSON());

app.MapGet("/profile/{id}", async ([FromRoute] int id, [FromServices] PennyContext _appContext) =>
{
    var user = await _appContext.Users.FindAsync(id);
    return user != null ? Results.Ok(user) : Results.NotFound();
});

app.MapGet("/users/{page}", async ([FromRoute] int page, [FromServices] PennyContext _appContext) =>
{
    var start = (page - 1) * 10;
    var res = await _appContext.Users.OrderBy(u => u.Id).Skip(start).Take(10).ToArrayAsync();

    return Results.Ok(res);
});

app.MapPost("/users", async ([FromBody] PostUserDto data, [FromServices] PennyContext _appContext) =>
{
    var input = new User() { Email = data.Email, Name = data.Name };
    var user = await _appContext.Users.AddAsync(input);
    await _appContext.SaveChangesAsync();
    return Results.Ok(user.Entity);
});

app.Run();