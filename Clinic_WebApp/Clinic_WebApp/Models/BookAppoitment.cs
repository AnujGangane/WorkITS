using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_WebApp.Models
{
    public class BookAppoitment
    {
        [Key]
        public int patientID {  get; set; }
        public string pname {  get; set; }
        public string pemail {  get; set; }
        public string pphone {  get; set; }
        [ForeignKey("Doctor")]
        public int DoctorID {  get; set; }
        public string pdate {  get; set; }
        public string pmsg {  get; set; }

        public Doctor Doctor { get; set; }

        public bool IsAccepted { get; set; } = true;
    }
}
