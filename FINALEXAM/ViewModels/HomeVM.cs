using FINALEXAM.Models;

namespace FINALEXAM.ViewModels
{
    public class HomeVM
    {
        public List<HomeIcon> Icons { get;set; }
        public List<HomeOurAminity> OurAminity { get;set;}
        public List<HomeOurService> OurServices { get;set; }
        public List<HomeProperti> Properti { get;set; }
        public List<HomeSlider> Slider { get;set; }
        public HomeProperti Lastproperty { get;set; }
    }
}
