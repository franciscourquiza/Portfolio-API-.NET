using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using FluentValidation;
using Application.Dtos.AdminDtos;
using Application.Validators.Admin;
using Application.Dtos.AuthDtos;
using Application.Validators.Auth;
using Application.Dtos.EducationDtos;
using Application.Validators.Education;
using Application.Dtos.ProyectDtos;
using Application.Validators.Proyect;
using Application.Dtos.WorkExperienceDtos;
using Application.Validators.WorkExperience;
using Application.Dtos.UserDtos;
using Application.Validators.User;

namespace CleanArchitectureAPI.Dependencies
{
    public static class Dependencies
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkExperienceService, WorkExperienceService>();
            services.AddScoped<IProyectService, ProyectService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();

            // Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddScoped<IProyectRepository, ProyectRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITokenVerifyRepository, TokenVerifyRepository>();

            //Validators

            services.AddScoped<IValidator<AdminForAddDto>, AdminForAddValidation>();
            services.AddScoped<IValidator<AdminForEditDto>, AdminForEditValidation>();
            services.AddScoped<IValidator<AuthenticationBodyRequest>, AuthValidator>();
            services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordRequestValidator>();
            services.AddScoped<IValidator<EducationForAddDto>, EducationForAddValidator>();
            services.AddScoped<IValidator<EducationForEditDto>, EducationForEditValidator>();
            services.AddScoped<IValidator<ProyectForAddDto>, ProyectForAddValidator>();
            services.AddScoped<IValidator<ProyectForEditDto>, ProyectForEditValidator>();
            services.AddScoped<IValidator<WorkExperienceForAdd>, WorkExperienceForAddValidator>();
            services.AddScoped<IValidator<WorkExperienceForEditDto>, WorkExperienceForEditValidator>();
            services.AddScoped<IValidator<UserForAddRequest>, UserForAddValidator>();
            services.AddScoped<IValidator<UserForEditDto>, UserForEditValidator>();

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