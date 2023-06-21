namespace FINALEXAM.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int  Count { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
