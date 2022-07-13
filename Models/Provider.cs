using System.ComponentModel.DataAnnotations;

namespace OrderApp.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
