using FINALEXAM.Models;

namespace FINALEXAM.ViewModels
{
    public class ShopVM
    {
        public int? Order { get; set; }
        public int?[] homeTypeId { get; set; }
        public string Search { get; set; }
        public List<HomeType> homeType { get; set; }

        public List<HomeProperti> homePropertis { get; set; }
        
    }
}
