using ExpensePilot.API.Repositories.Implementation.Policies;
using ExpensePilot.API.Repositories.Interface.Policies;
using ExpensePilot.Services.PoliciesAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Injecting the dbcontext class using dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //Linking to ExpensePilotConnectionString
    options.UseSqlServer(builder.Configuration.GetConnectionString("PoliciesConnectionString"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();

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

app.Run();
