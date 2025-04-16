using Licencia___PF;
using Licencias___PF.DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Licencias___PF.Services
{
    public class LicenciaValidacion
    {
        private readonly LicenciaContext _context;

        public LicenciaValidacion(LicenciaContext context)
        {
            _context = context;
        }

        public async Task<bool> LicenciaExists(int id)
        {
            return await _context.Licencias.AnyAsync(l => l.Id == id);
        }
        
    }
}

