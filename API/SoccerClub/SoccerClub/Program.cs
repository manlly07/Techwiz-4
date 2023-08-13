using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SoccerClub.Configurations;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

namespace SoccerClub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            SqlConnection connection = new SqlConnection(builder.Configuration.GetConnectionString("SqlServer"));

            connection.Open();

            builder.Services.AddControllers();
            builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureServices();
            builder.Services.AddRouting();
                
            builder.Services.ConfigureServices();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            //IConfiguration configuration = builder.Configuration;
            //builder.Services.AddAuthentication
            //    (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            //    (option =>
            //    {
            //        option.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = false,
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8qUAIZa51UOZH5dEAQEgvA")),
            //            ClockSkew = TimeSpan.Zero
            //        };
            //});

            //builder.Services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "TechWarriors",
            //        Version = "v1"
            //    });
            //    options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT containing userid claim",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey
            //    });
            //    var security =
            //        new OpenApiSecurityRequirement
            //        {
            //            {
            //                new OpenApiSecurityScheme
            //                {
            //                    Reference = new OpenApiReference
            //                    {
            //                        Id = "Authorization",
            //                        Type = ReferenceType.SecurityScheme
            //                    },
            //                    UnresolvedReference = true
            //                },
            //                new List<string>()
            //            }
            //        };

            //    options.AddSecurityRequirement(security);
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(/*option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "TechWarriors v1")*/);
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/Resources"
            });

            app.MapControllers();

            app.Run();
        }
    }
}