namespace OrderApp.Models
{
    public class Filter
    {
        public DateTime? FromDate { get; set; } = DateTime.Now.AddMonths(-1);

        public DateTime? ToDate { get; set; } = DateTime.Now;
        
        public int? ProviderId { get; set; }
        
        public string? Number { get; set; }
        
        public string? Name { get; set; }
        
        public decimal? Quantity { get; set; }
        
        public string? Unit { get; set; }

        public IQueryable<Order> Apply(IQueryable<Order> orders, IQueryable<OrderItem> items)
            => from order in orders
               where
               // Date filter
               (!this.FromDate.HasValue || order.Date > this.FromDate.Value) &&
               (!this.ToDate.HasValue || order.Date < this.ToDate.Value) &&

               // Provider filter
               (!this.ProviderId.HasValue || order.ProviderId == this.ProviderId.Value) &&

               // Number filter
               (string.IsNullOrEmpty(this.Number) || order.Number == this.Number) &&

               // Contains item filter
               (string.IsNullOrEmpty(this.Name) && !this.Quantity.HasValue && string.IsNullOrEmpty(this.Unit) ||
               items.Any(item =>
               item.OrderId == order.Id &&
               (string.IsNullOrEmpty(this.Name) || item.Name == this.Name) &&
               (!this.Quantity.HasValue || item.Quantity == this.Quantity) &&
               (string.IsNullOrEmpty(this.Unit) || item.Unit == this.Unit)))

               select order;
    }
}
