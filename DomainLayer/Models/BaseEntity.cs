using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public abstract class BaseEntity<K>
        where K : IEquatable<K>
    {

        public virtual K Id { get; protected set; } = default!;

        //[NotMapped]
        private readonly List<INotification> _domainEvents = new();
        [NotMapped]
        public List<INotification> DomainEvents = new List<INotification>();

        public bool IsTransient()
        {
            return Object.Equals(Id, default(K));

        }

        public void AddDomainEvent(INotification evt)
        {
            _domainEvents.Add(evt);
        }
        public void RemoveDomainEvent(INotification evt)
        {
            _domainEvents?.Remove(evt);
        }
        
        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
        public override bool Equals(object? obj)
        {
            return obj != null && obj is BaseEntity<K> entity &&
              Equals(entity);
        }

        public bool Equals(BaseEntity<K> other)
        {
            if (other.IsTransient() || this.IsTransient())
                return false;
            return Object.Equals(Id, other.Id);
        }

        int? _requestedHashCode;
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = HashCode.Combine(Id);
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(BaseEntity<K> left, BaseEntity<K> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(BaseEntity<K> left, BaseEntity<K> right)
        {
            return !(left == right);
        }
    }
}
