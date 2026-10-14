namespace DomainLayer.Models.Customers
{
    public class Customer : BaseEntity<Guid>
    {
        public string? Name { get; init; }
        public Customer()
        {
                
        }

        public Customer(int customerId, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
