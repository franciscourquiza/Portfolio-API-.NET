using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Admin> Admins { get; set; }    
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Proyect> Proyects { get; set; }
        public DbSet<TokenVerify> TokenVerifies { get; set; }

        private readonly bool isTestingEnvironment;

        public ApplicationContext(DbContextOptions<ApplicationContext> options,  bool isTestingEnvironment = false) : base(options)
        {
            this.isTestingEnvironment = isTestingEnvironment;
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<User>().HasDiscriminator(u => u.UserRole);

            base.OnModelCreating(modelBuilder);
            //convierte los enteros asociados al enum a strings, en la columna de UserRole de la base de datos en lugar de guardar 0 1 2 3, va a guardar los strings asociados
        }
    }
}
