using Licencia___PF.Model;
using Licencia___PF;
using Microsoft.EntityFrameworkCore;

namespace Licencias___PF.Services
{
    public class LicenciaRepository : ILicenciaRepository
    {
        private readonly LicenciaContext _context;

        public LicenciaRepository(LicenciaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Licencia>> GetAllAsync()
        {
            return await _context.Licencias.ToListAsync();
        }

        public async Task<Licencia?> GetByIdAsync(int id)
        {
            return await _context.Licencias.FindAsync(id);
        }

        public async Task AddAsync(Licencia licencia)
        {
            await _context.Licencias.AddAsync(licencia);
        }

        public void Update(Licencia licencia)
        {
            _context.Licencias.Update(licencia);
        }

        public void Remove(Licencia licencia)
        {
            _context.Licencias.Remove(licencia);
        }
    }
}
