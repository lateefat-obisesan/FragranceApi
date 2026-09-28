namespace FragranceApi.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = "";

        public DateTime Date { get; set; }

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        public decimal Total { get; set; }
    }
}
