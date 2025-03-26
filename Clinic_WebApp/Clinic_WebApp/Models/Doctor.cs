using System.ComponentModel.DataAnnotations;

namespace Clinic_WebApp.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID {  get; set; }
        public string dname { get; set; }
        public string dspeciality {  get; set; }
        public string dphone {  get; set; }

        public ICollection<BookAppoitment> BookAppoitments { get; set; } = new List<BookAppoitment>();
    }
}
