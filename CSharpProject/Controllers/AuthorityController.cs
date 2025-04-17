using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using CSharpProject.Models;
namespace CSharpProject.Controllers
{
    public class AuthorityController:Controller
    {
        private readonly MyDbContext _context;
        public AuthorityController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View(new Student());
        }

        [HttpPost]
        public IActionResult Signup(Student student)
        {  if(ModelState.IsValid)
            {
                var existingaccount = _context.Student.FirstOrDefault(s => s.StudentAccount == student.StudentAccount);
               
                if(existingaccount!=null)
                {
                    ModelState.AddModelError("StudentAccount", "Tài khoản đã tồn tại");
                    return View(student);
                }
                else
                {
                    _context.Student.Add(student);
                     _context.SaveChanges();
                    return RedirectToAction("Login", "Authority");


                }

            }
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage); 
                }
            }
            return View(student);
            

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());

        }
        [HttpPost]
        public IActionResult Login(LoginViewModel student)
        {
            if(ModelState.IsValid)
            {
                var existingStudent = _context.Student.FirstOrDefault(s => s.StudentAccount == student.StudentAccount && s.StudentPass == student.StudentPass);
               if (existingStudent!=null)
                {
                    HttpContext.Session.SetString("UserID", existingStudent.StudentID.ToString());
                    return RedirectToAction("Main", "Student");


                }
                else
                if(student.StudentAccount.Equals("Quanly")&&student.StudentPass.Equals("68066666"))
                { int QuanlyID = 68;
                    HttpContext.Session.SetString("QuanlyID",QuanlyID.ToString() );
                    return RedirectToAction("Main","Course");
                }
                else
                {
                    ModelState.AddModelError("StudentAccount", "Tài khoản hoặc mật khẩu không đúng ");
                    return View(student);
                }
            }
            return View(student);
        }
        [HttpGet]
        public IActionResult Logout(string Session)
        {   if(Session=="User")
            {
                HttpContext.Session.Remove("UserID");
            }
            else
            if(Session=="Quanly")
            {
                HttpContext.Session.Remove("QuanlyID");
            }
                return RedirectToAction("Login", "Authority");
        }


    }
}
