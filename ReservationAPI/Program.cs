using ReservationAPI.Repository.Interfaces;
using ReservationAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CORSPolicy");

var repo = app.Services.GetRequiredService<IReservationRepository>();


if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.MapControllers();
app.Run();
