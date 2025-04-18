using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure.Services
{
    public interface IImportService<TEntity>
        where TEntity : Entity
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}
