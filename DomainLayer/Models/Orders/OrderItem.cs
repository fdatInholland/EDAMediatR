using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Order
{
    public class OrderItem : BaseEntity<Guid>
    {
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        public OrderItem()
        {
                Id = Guid.NewGuid();
        }

        public OrderItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }

}