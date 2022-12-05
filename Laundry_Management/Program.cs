using Laundry_Management.Data;
using Laundry_Management.Procedures;
using Laundry_Management.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IMachine, MachineServices>();
builder.Services.AddScoped<IUser, UserServices>();

builder.Services.AddDbContext<LaundryContext>(
    option =>
    {
        option.UseMySql(builder.Configuration.GetConnectionString("LaundryDB"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    });

builder.Services.AddDbContext<LaundryContextProcedures>(
    option =>
    {
        option.UseMySql(builder.Configuration.GetConnectionString("LaundryDB"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

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
