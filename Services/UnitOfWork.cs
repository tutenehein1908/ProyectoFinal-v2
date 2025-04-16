using Licencia___PF;

namespace Licencias___PF.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LicenciaContext _context;

        public ILicenciaRepository Licencias { get; }

        public UnitOfWork(LicenciaContext context)
        {
            _context = context;
            Licencias = new LicenciaRepository(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

   
}
