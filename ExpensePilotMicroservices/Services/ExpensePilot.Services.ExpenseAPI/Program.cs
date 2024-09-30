using ExpensePilot.Services.ExpenseAPI.Data;
using ExpensePilot.Services.ExpenseAPI.Repositories.Implementation;
using ExpensePilot.Services.ExpenseAPI.Repositories.Interface;
using ExpensePilot.Services.ExpenseAPI.Services.Implementation;
using ExpensePilot.Services.ExpenseAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ExpenseDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseConnectionString"));
});


builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

builder.Services.AddHttpClient<IExpenseCategoryService, ExpenseCategoryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7002");
});


//Defining Cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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
