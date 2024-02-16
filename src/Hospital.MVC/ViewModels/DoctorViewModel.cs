using Hospital.Core.Models;

namespace Hospital.MVC.ViewModels
{
    public class DoctorViewModel
    {
     
       
        public List<Doctor> Doctors { get;set; }
        public List<Profession>Professions { get; set; }
        public List<WorkSchedule> WorkSchedules { get; set; }
    }
   
}
