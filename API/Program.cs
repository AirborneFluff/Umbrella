using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddIdentityCore();
builder.AddAuthentication();
builder.AddAuthorization();
builder.AddMongoDbContext();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
//app.MapFallbackToController("Index", "Fallback");

app.SeedDatabase();

app.Run();