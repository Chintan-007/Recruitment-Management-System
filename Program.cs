using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserTypeRepository,UserTypeService>();
builder.Services.AddScoped<IDocumentTypeRepository,DocumentTypeService>();
builder.Services.AddScoped<IInterviewTypeRepository,InterviewTypeService>();

//Database Connection
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<ApplicationContext>(opt => 
    opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();


app.Run();


