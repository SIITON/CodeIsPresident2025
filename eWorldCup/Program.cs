using eWorldCup.Application.Interfaces;
using eWorldCup.Application.Repositories;
using eWorldCup.Application.Services;
using eWorldCup.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // Vite and CRA default ports
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Application Services
builder.Services.AddScoped<IRoundPairGenerator, RoundRobinPairGenerator>();
builder.Services.AddScoped<IRoundMetricsService, RoundMetricsService>();
builder.Services.AddScoped<IDirectMatchResolver, DirectMatchResolver>();

// Infrastructure Services
builder.Services.AddScoped<IParticipantRepository, FileParticipantRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowReactApp");

app.UseStaticFiles(); // For serving images

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5235");