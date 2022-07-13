using System.ComponentModel.DataAnnotations;

namespace OrderApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int ProviderId { get; set; }
    }
}
