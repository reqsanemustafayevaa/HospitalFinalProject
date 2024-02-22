using Hospital.Core.Models;

namespace Hospital.MVC.ViewModels
{
    public class AboutViewModel
    {
        public List<About> abouts {  get; set; }
        public List<Doctor> doctors { get; set; }
        public List<Profession>professions { get; set; }
        public List<WorkSchedule> workschedules { get; set; }
        public List<AboutResponse> aboutResponses { get; set; }
    }
}
