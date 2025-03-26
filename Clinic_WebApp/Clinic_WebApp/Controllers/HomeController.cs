using Clinic_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Numerics;

namespace Clinic_WebApp.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;

        public ClinicDbContext c1;
        public IWebHostEnvironment env;
        
        public HomeController(ILogger<HomeController> logger,ClinicDbContext c1, IWebHostEnvironment env)
        {
            _logger = logger;
            this.c1 = c1;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult BookApp()
        {
            var doctors = c1.Doctors.ToList();
            ViewBag.Doctors = doctors;
            
            return View();
        }
        [HttpPost]
        public IActionResult BookApp(BookAppoitment b1)
        {   
                c1.BookAppoitments.Add(b1);
                c1.SaveChanges();
                return RedirectToAction("Index");
        }
        public IActionResult CreateDoc()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDoc(Doctor D1)
        {
            if (ModelState.IsValid)
            {
                c1.Doctors.Add(D1); 
                c1.SaveChanges();   
                return RedirectToAction("Index"); 
            }
            return View(D1); 
        }

        public IActionResult DocLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DocLogin(Doctor D1)
        {
            var doctor = c1.Doctors.Where(model => model.dname.Equals(D1.dname) && model.dphone.Equals(D1.dphone)).SingleOrDefault();
            if (doctor != null)
            {
                HttpContext.Session.SetInt32("DoctorId", doctor.DoctorID); // Save integer
                HttpContext.Session.SetString("DoctorName", doctor.dname); // Save string
                return RedirectToAction("DocDash");
            }
            ViewBag.ErrorMessage = "Invalid name or phone number.";
            return View();
        }

        public IActionResult DocDash(Doctor D1)
        {
            var doctorId = HttpContext.Session.GetInt32("DoctorId"); // Retrieve doctor ID
            
            var doctorName = HttpContext.Session.GetString("DoctorName"); // Retrieve doctor name
            if (string.IsNullOrEmpty(doctorName))
            {
                return RedirectToAction("DocLogin"); // Redirect to login if session is empty
            }
            ViewData["DoctorName"] = doctorName; // Pass name to the view
            return View();
        }

        public IActionResult ViewAppointments()
        {
            var doctorId = HttpContext.Session.GetInt32("DoctorId"); // Retrieve doctor ID
            if (doctorId == null)
            {
                return RedirectToAction("DocLogin"); // Redirect to login if session is empty
            }
            var appointments = c1.BookAppoitments.Where(a => a.DoctorID == doctorId).ToList();
            ViewData["Appointments"] = appointments;

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("DocLogin");
        }
        public IActionResult DocInfo()
        {
            var x = c1.Doctors.Include("BookAppoitments").ToList();
            return View(x);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AccpetAppointments(int id)
        {
            var appointment = c1.BookAppoitments.FirstOrDefault(a => a.patientID == id);

            if (appointment != null)
            {
                var doctorId = HttpContext.Session.GetInt32("DoctorId");
                if (doctorId == null)
                {
                    return RedirectToAction("DocLogin", "Home");
                }
                appointment.IsAccepted = true;
                appointment.DoctorID = doctorId.Value;
                
                c1.SaveChanges();
                return RedirectToAction("PatientRecords");
            }
            return RedirectToAction("ViewAppointments");
        }
        public IActionResult PatientRecords()
        {
            var doctorId = HttpContext.Session.GetInt32("DoctorId");
            if (doctorId == null)
            {
                return RedirectToAction("DocLogin", "Home");
            }
            var acceptedAppointments = c1.BookAppoitments
                .Where(a => a.IsAccepted == true && a.DoctorID == doctorId)
                .ToList();
            return View(acceptedAppointments);
        }
        public IActionResult RemovePR(int id)
        {
            
            return RedirectToAction("PatientRecords");
        }
        public IActionResult ManageProfile(int id)
        {
            var doctorId = HttpContext.Session.GetInt32("DoctorId");
            if (doctorId == null || doctorId != id)
            {
                return RedirectToAction("DocDash"); 
            }
            var doctor = c1.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound(); 
            }
            return View(doctor);
        }
        public IActionResult RejecttAppointments(int id)
        {
            //var x = c1.BookAppoitments.Find(id);
            //c1.BookAppoitments.Remove(x);
            //c1.SaveChanges();
            return RedirectToAction("ViewAppointments");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
