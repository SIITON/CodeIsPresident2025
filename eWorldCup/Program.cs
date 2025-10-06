using eWorldCup.Application;
using eWorldCup.Application.Interfaces;
using eWorldCup.Application.Repositories;
using eWorldCup.Application.Services;
using eWorldCup.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// New service registrations
builder.Services.AddScoped<IRoundPairGenerator, RoundRobinPairGenerator>();
builder.Services.AddScoped<IRoundMetricsService, RoundMetricsService>();
builder.Services.AddScoped<IDirectMatchResolver, DirectMatchResolver>();

builder.Services.AddScoped<IParticipantRepository, FileParticipantRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run("http://localhost:5235");