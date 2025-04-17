using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CSharpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace CSharpProject.Controllers
{
    
    public class StudentController : Controller
    {
        private readonly MyDbContext _context;
        public StudentController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Main()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            var course = _context.Course.ToList();
            var studentMainView = new StudentMainView();
            studentMainView.courses = course;
            studentMainView.FindingName = "";
            studentMainView.StudentName = _context.Student.FirstOrDefault(s => s.StudentID == int.Parse(HttpContext.Session.GetString("UserID"))).StudentName;
            return View(studentMainView);

        }
        [HttpPost]
        public IActionResult Choose(int CourseID)
        {
            var course = _context.Course.FirstOrDefault(s => s.CourseID == CourseID);
            var StudentID = HttpContext.Session.GetString("UserID");
            var enrollment = new Enrollment();
            enrollment.CourseID = CourseID;
            enrollment.StudentID = int.Parse(StudentID);
            enrollment.DateSignCourse = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            int totalstudent = _context.ĐKIKH.Count(s => s.CourseID == CourseID);
            if (enrollment.DateSignCourse.Date > course.CourseDate.Date ||totalstudent>=course.CourseNumber)
            {
                ModelState.AddModelError("DateSignCourse", "Khóa học này đã quá hạn đăng ký hoặc quá số lượng người đăng kí học");
                StudentMainView ds = new StudentMainView();
                ds.courses = _context.Course.ToList();
                ds.FindingName = "";
                ds.StudentName = _context.Student.FirstOrDefault(s => s.StudentID == int.Parse(HttpContext.Session.GetString("UserID"))).StudentName;
                return View("Main",ds);
            }

            _context.ĐKIKH.Add(enrollment);
            _context.SaveChanges();
            return RedirectToAction("Main", "Student");
        }
        [HttpPost]
        public IActionResult Search(string FindingName, string FromView)
        {
            var courses = _context.Course
                     .Where(c => c.CourseName.Contains(FindingName))
                     .ToList();
            var studentMainView = new StudentMainView();
            studentMainView.courses = courses;
            studentMainView.FindingName = FindingName;
            studentMainView.StudentName = _context.Student.FirstOrDefault(s => s.StudentID == int.Parse(HttpContext.Session.GetString("UserID"))).StudentName;
          
                return View("Main", studentMainView);
          

            


        }
        [HttpGet]
        public IActionResult SignedCourse()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            int StudentID = int.Parse(HttpContext.Session.GetString("UserID"));
            var query = from dki in _context.ĐKIKH
                        join courses in _context.Course on dki.CourseID equals courses.CourseID 
                        join student in _context.Student on dki.StudentID equals student.StudentID
                        where dki.StudentID == StudentID
                        select new {
                            CourseID = dki.CourseID,
                            EnrollmentID = dki.EnrollmentId,
                            CourseName = courses.CourseName,
                            DateOpen=courses.CourseDate,
                            DateSignCourse = dki.DateSignCourse,




                        };
            var list = query.ToList();
            List<SignedCourseData> ds = new List<SignedCourseData>();
            foreach( var item in list)
            {
                SignedCourseData x = new SignedCourseData();
                x.CourseID = item.CourseID;
                   x.CourseName = item.CourseName;
                    x.DateSignCourse = item.DateSignCourse;
                x.DateOpen = item.DateOpen;
                x.EnrollmentID = item.EnrollmentID;
                ds.Add(x);
                
            }
            
            return View(ds);





        }
        [HttpPost]
        public IActionResult Delete(int EnrollmentID)
        {
            var DeleteSignedCourse = _context.ĐKIKH.FirstOrDefault(s => s.EnrollmentId == EnrollmentID);
            int IDDeleteCourse = DeleteSignedCourse.CourseID;
            var Course = _context.Course.FirstOrDefault(s => s.CourseID == IDDeleteCourse);
            var x = Course.CourseDate.Date;
            var currenttime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
    TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).Date;

            if ( currenttime<=x)
            {
                _context.ĐKIKH.Remove(DeleteSignedCourse);
                _context.SaveChanges();
                return RedirectToAction("Main", "Student");
            }
            TempData["Error"] = "Bạn chỉ có thể hủy đăng kí trước ngày khai giảng";
            return RedirectToAction("Main", "Student");

        }
    }

}
