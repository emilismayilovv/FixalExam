namespace FINALEXAM.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int  Count { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public HomeProperti HomeProperti { get; set; }
        public int HomePropertiId { get; set; }
    }
}
