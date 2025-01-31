using ReservationAPI.Repository.Interfaces;
using ReservationAPI.Repository;
using ReservationAPI.Model;
using ReservationAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CORSPolicy");

var repo = app.Services.GetRequiredService<IReservationRepository>();

app.MapGet("/reservations", () => Results.Json(repo.GetReservations()));
app.MapGet("/reservations/{id}", (Guid id) =>
    repo.GetReservation(id) is Reservation res ? Results.Json(res) : Results.NotFound());

app.MapPost("/reservations", (ReservationRequest reservation) =>
{
    if (reservation != null)
    {

        if (!DateTime.TryParse(reservation.ArrivalTime, out var arrival) || !DateTime.TryParse(reservation.DepartureTime, out var departure))
        {
            return Results.BadRequest(new
            {
                message = "Invalid date format"
            });
        }

        if (arrival < departure)
        {
            return Results.BadRequest(new
            {
                message = "Arrival time cannot be before departure time"
            });
        }
        var newReservation = new Reservation() {
            Id = Guid.NewGuid(),
            PassengerName = reservation.PassengerName,
            FlightNumber = reservation.FlightNumber,
            ArrivalTime = arrival,
            DepartureTime = departure,
            Class = reservation.Class
        };

        repo.AddReservation(newReservation);
        return Results.Created($"/reservations/{newReservation.Id}", newReservation);
    }
    return Results.BadRequest();
});

app.MapDelete("/reservations/{id}", (Guid id) =>
{
    var existing = repo.GetReservation(id);
    if (existing != null)
    {
        repo.DeleteReservation(id);
        return Results.NoContent();
    }
    return Results.NotFound();
});

if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Run();
