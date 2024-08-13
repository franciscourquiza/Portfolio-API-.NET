using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Profiles;
using Microsoft.OpenApi.Models;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureAPI.Dependencies
{
    public static class Dependencies
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Services

            services.AddScoped<UserService>();
            services.AddScoped<WorkExperienceService>();
            services.AddScoped<ProyectService>();
            services.AddScoped<EducationService>();
            services.AddScoped<AdminService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<EmailService>();

            // Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddScoped<IProyectRepository, ProyectRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITokenVerifyRepository, TokenVerifyRepository>();

            // AutoMapper

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // PasswordHasher
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Swagger + JWT Bearer

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.AddSecurityDefinition("PortfolioAPIBearerAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Pegar Token Generado al loguearse."
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "PortfolioAPIBearerAuth" }
                        }, new List<string>() }
                });
            });

            // CORS

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                });
            });
        }
    }
}