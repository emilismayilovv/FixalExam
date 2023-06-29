using FINALEXAM.ViewModels;

namespace FINALEXAM.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Address { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime PurchasedAt { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public AboutTeam? AboutTeam { get; set; }
        public int? AboutTeamId { get; set; }

    }
}
