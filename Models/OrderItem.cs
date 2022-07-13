using System.ComponentModel.DataAnnotations;

namespace OrderApp.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }
    }
}
