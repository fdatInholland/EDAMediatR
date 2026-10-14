using DomainLayer.Events;
using DomainLayer.Models.Customers;

namespace DomainLayer.Models.Order
{
    public class Order : BaseEntity<Guid>
    {
        public DateTime DeliveryTime { get; set; }
        public Customer? Customer { get; set; }
        public DateTime CreationTime { get; init; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Order()
        {

        }

        public Order(DateTime creationTime, DateTime _deliveryTime)
        {
            Id = Guid.NewGuid();
            CreationTime = creationTime;
            DeliveryTime = _deliveryTime;
        }

        public bool DelayDeliveryTime(DateTime newDeliveryTime)
        {
            if (DeliveryTime < newDeliveryTime)
            {
                DeliveryTime = newDeliveryTime;
                return true;
            }
            else return false;
        }

        public void ApplyDiscount(decimal discount)
        {
            foreach (OrderItem item in OrderItems)
            {
                decimal? discounteditem = item.UnitPrice * discount;
                item.UnitPrice = item.UnitPrice - discounteditem.Value;
            }

            //add event 
            DomainEvents.Add(new OrderPlacedDiscountEvent(this ,discount));

        }
    }
}
