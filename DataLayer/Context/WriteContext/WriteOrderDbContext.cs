using DataLayer.Repo.UofW;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.Customers;
using DomainLayer.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace DataLayer.Context.ReadContext
{
    public class WriteOrderDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();

        public WriteOrderDbContext(DbContextOptions<WriteOrderDbContext> options, IMediator mediator)
        : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder
        builder)
        {
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            //Dispatch Domain Events
            var domainEntities = ChangeTracker
                                            .Entries<BaseEntity<Guid>>()
                                            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvent();
            }

            // Publish all events via MediatR
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            //Save changes to DB
            try
            {
                return await SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ConstraintViolationException(ex);
            }
        }
        public async Task CommitAsync()
        {
            await Database.CommitTransactionAsync();
        }
        public Task RollbackAsync()
        {
            Database.RollbackTransaction();
            return Task.CompletedTask;
        }

        public Task StartAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Database.BeginTransactionAsync(isolationLevel, CancellationToken.None);
        }
    }
}
