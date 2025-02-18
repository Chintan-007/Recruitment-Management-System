using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Service;
using RecruitmentManagement.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            builder => builder.WithOrigins("http://localhost:5173") // Frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader());
    });
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddEndpointsApiExplorer();

//Preventing object cycles(loops in the results)
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// builder.Services.AddScoped<IUserTypeRepository,UserTypeService>();
builder.Services.AddScoped<IDocumentTypeRepository,DocumentTypeService>();
builder.Services.AddScoped<IInterviewTypeRepository,InterviewTypeService>();
builder.Services.AddScoped<IPositionRepository,PositionService>();
builder.Services.AddScoped<IOrganisationTypeRepository,OrganisationTypeService>();
builder.Services.AddScoped<IOrganisationRepository,OrganisationService>();
builder.Services.AddScoped<ICandidateSkillRepository,CandidateSkillService>();
builder.Services.AddScoped<IJobOpeningRepository,JobOpeningService>();
builder.Services.AddScoped<IJobCandidateRepository,JobCandidateService>();
builder.Services.AddScoped<IScheduleInterviewRepository,ScheduleInterviewService>();
builder.Services.AddScoped<ICandidateDocsRepository,CandidateDocsService>();
builder.Services.AddScoped<IInterviewRoundRepository,InterviewRoundService>();
builder.Services.AddScoped<ITokenService,TokenService>();

//-------------------Database Connection--------------------------------------------------
builder.Services.AddDbContext<ApplicationContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//----------------------------------------------------------------------------------------

//------------------Configuring Authentication--------------------------------------------
builder.Services.AddIdentity<Users,IdentityRole>(opt=>{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt=>{
    opt.DefaultAuthenticateScheme = 
    opt.DefaultChallengeScheme =
    opt.DefaultForbidScheme = 
    opt.DefaultScheme = 
    opt.DefaultSignInScheme = 
    opt.DefaultSignOutScheme = 
    JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt=>{
    opt.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey( System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Signingkey"])),
        // RoleClaimType = ClaimTypes.Role,  // Ensure roles are validated as "role" claims
        // NameClaimType = ClaimTypes.Name 
    };
});

//----------------------------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


 app.UseCors("AllowFrontend"); // Apply the policy

app.UseRouting();

//Authentication Part
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


