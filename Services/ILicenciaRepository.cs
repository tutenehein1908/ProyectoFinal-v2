using Licencia___PF.Model;

namespace Licencias___PF.Services
{
    public interface ILicenciaRepository
    {
        Task<IEnumerable<Licencia>> GetAllAsync();
        Task<Licencia?> GetByIdAsync(int id);
        Task AddAsync(Licencia licencia);
        void Update(Licencia licencia);
        void Remove(Licencia licencia);
    }
}
