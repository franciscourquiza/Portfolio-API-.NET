using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProyectRepository : BaseRepository<Proyect>, IProyectRepository
    {
        private readonly ApplicationContext _context;
        public ProyectRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Proyect?> GetByTitle(string title)
        {
            return await _context.Proyects.FirstOrDefaultAsync(t => t.Title == title);
        }
        public async Task Delete(string title)
        {
            Proyect? proyectToDelete = await GetByTitle(title);
            if (proyectToDelete == null)
            {
                throw new ArgumentNullException(nameof(proyectToDelete));
            }
            _context.Proyects.Remove(proyectToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
