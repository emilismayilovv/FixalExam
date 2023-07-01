namespace FINALEXAM.Models
{
    public class HomeType
    {
        public int Id { get; set; }
        public string HouseType { get; set; }
        
        public List<HomeProperti>? homePropertis { get; set; }
    }
}
