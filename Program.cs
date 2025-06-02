using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaTarefa.Data;
using SistemaTarefa.Repositories;
using SistemaTarefa.Repositories.Interfaces;
using SistemaTarefa.Services;

namespace SistemaTarefa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string SecretKey = "7fddc1c5-7654-4501-b5d2-a7dd6df1a5c7";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Configuração do Swagger com JWT
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de Tarefas - API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "JWT Autenticação",
                    Description = "Entre com o JWT Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new string[] {} }
                });
            });

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SystemTaskDBContext>(
                    options => options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DataBase")
                    )
                );

            builder.Services.AddScoped<IUserRepositorie, UserRepositorie>();
            builder.Services.AddScoped<ITaskRepositorie, TaskRepositorie>();
            builder.Services.AddScoped<TokenService>();


            // Configuração do CORS para permitir acesso do React
          



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "sua_empresa",
                    ValidAudience = "sua_aplicacao",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
                };
            });

            var app = builder.Build();

            builder.Services.AddCors();

            app.UseCors(opt =>{

                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Habilitar CORS antes de Authentication/Authorization
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
