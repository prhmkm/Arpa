using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ArpaBackend.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("ArpaBackend", new OpenApiInfo { Title = "ArpaBackend", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
});
var xz = SecurityHelpers.DecryptString(builder.Configuration["ConnectionString:DB"], "@Rp@2022!");
//var xz = builder.Configuration["ConnectionString:DB"];
builder.Services.AddDbContext<ArpaWebsite_DBContext>(options => options.UseSqlServer(xz), ServiceLifetime.Transient);
ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

builder.Services.AddControllers();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.TokenSecret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => httpReq.Scheme = httpReq.Host.Value);
});

app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ArpaBackend/swagger.json", "ArpaBackend v1"));
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ArpaBackend/swagger.json", "ArpaBackend v1"));
//}
//else
//{
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/swagger/ArpaBackend/swagger.json", "ArpaBackend v1"));
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

