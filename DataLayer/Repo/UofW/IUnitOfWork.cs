using System.Data;

namespace DataLayer.Repo.UofW
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);
        Task StartAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitAsync();
        Task RollbackAsync();
    }
}
