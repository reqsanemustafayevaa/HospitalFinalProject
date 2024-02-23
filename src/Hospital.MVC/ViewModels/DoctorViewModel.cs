using Hospital.Core.Models;

namespace Hospital.MVC.ViewModels
{
    public class DoctorViewModel
    {
     
       
        public List<Doctor> Doctors { get;set; }
        public List<Profession>Professions { get; set; }
        public List<WorkSchedule> WorkSchedules { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<About>abouts { get; set; }
        public List<Feature> Features { get; set; }
        public List<Service> Services { get; set; }
        public List<Testimonial> Testimonials { get; set;}
        public List<Gallery> Galleries { get; set; }
        public List<Banner> Banners { get; set; }
    }
   
}
