using Licencia___PF;

namespace Licencias___PF.Services
{

    public interface IUnitOfWork : IDisposable
    {
        ILicenciaRepository Licencias { get; }
        Task<int> CommitAsync();
    }


}
