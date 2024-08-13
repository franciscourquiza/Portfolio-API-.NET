using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Domain.Exceptions;

namespace Infrastructure.Repositories
{
    public class ProyectRepository : BaseRepository<Proyect>, IProyectRepository
    {
        private readonly ApplicationContext _context;
        public ProyectRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public Proyect? GetByTitle(string title)
        {
            return _context.Proyects.FirstOrDefault(t => t.Title == title);
        }
        public void Delete(string title)
        {
            Proyect? proyectToDelete = GetByTitle(title);
            if (proyectToDelete == null)
            {
                throw new ArgumentNullException(nameof(proyectToDelete));
            }
            _context.Proyects.Remove(proyectToDelete);
            _context.SaveChanges();
        }
    }
}
