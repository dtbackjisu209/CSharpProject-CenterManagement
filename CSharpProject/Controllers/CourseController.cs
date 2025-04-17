using Microsoft.AspNetCore.Mvc;
using CSharpProject.Models;
using ZstdSharp.Unsafe;
using Microsoft.AspNetCore.Authorization;

namespace CSharpProject.Controllers
{

    public class CourseController : Controller
    {
        private readonly MyDbContext _context;
        public CourseController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Main()
        {
            if (HttpContext.Session.GetString("QuanlyID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            var course = _context.Course.ToList();
            var courseMainView = new CourseMainView();
            courseMainView.courses = course;
            courseMainView.FindingName = "";
            return View(courseMainView);

        }
        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("QuanlyID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            var coursechange = _context.Course.FirstOrDefault(s => s.CourseID == course.CourseID);
            if (coursechange != null)
            {
                coursechange.CourseName = course.CourseName;
                coursechange.CourseDate = course.CourseDate;
                coursechange.CourseTeacher = course.CourseTeacher;
                coursechange.CourseTuition = course.CourseTuition;
                coursechange.CourseNumber = course.CourseNumber;
                _context.SaveChanges();
            }
            return RedirectToAction("Main");



        }
        [HttpGet]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("QuanlyID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            CourseMainView coursemainview = new CourseMainView();
            var course = _context.Course.ToList();
            coursemainview.courses = course;

            coursemainview.FindingName = " ";
            return View(coursemainview);



        }
        [HttpPost]
        public IActionResult Search(string FindingName, string FromView)
        {
            var courses = _context.Course
                     .Where(c => c.CourseName.Contains(FindingName))
                     .ToList();
            var courseMainView = new CourseMainView();
            courseMainView.courses = courses;
            courseMainView.FindingName = FindingName;
            if (FromView == "Edit")
            {
                return View("Edit", courseMainView);
            }
            else
            if (FromView == "Delete")
            {
                return View("Delete", courseMainView);
            }
            else
            if(FromView=="Statistic")
            {
                return View("Statistic", courseMainView);
            }
            else
            { return View("Main", courseMainView); }



        }
        [HttpGet]
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetString("QuanlyID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            CourseMainView coursemainview = new CourseMainView();
            var courses = _context.Course.ToList();
            coursemainview.courses = courses;
            coursemainview.FindingName = " ";
            return View(coursemainview);
        }
        [HttpPost]
        public IActionResult Delete(int CourseID)
        {
            var course = _context.Course.FirstOrDefault(s => s.CourseID == CourseID);
            _context.Course.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Main");

        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            _context.Course.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpPost]
        public IActionResult StatisticDetail(int StatisticID)
        {
            var coursedates = _context.ĐKIKH.Where(s => s.CourseID == StatisticID)
                .Select(s => s.DateSignCourse).ToList();
            var x = _context.Course.FirstOrDefault(s => s.CourseID == StatisticID);
            int count = _context.ĐKIKH.Count(s => s.CourseID == StatisticID);



            StatisticData statisticData = new StatisticData();
            statisticData.coursedates = coursedates;
            statisticData.course = x;
            statisticData.count = count;
            return View("StatisticDetail", statisticData);


        }
        [HttpGet]
        public IActionResult Statistic()
        {
            if (HttpContext.Session.GetString("QuanlyID") == null)
            {
                return RedirectToAction("Login", "Authority");
            }
            CourseMainView courseMainView = new CourseMainView();   
            courseMainView.courses= _context.Course.ToList();
            courseMainView.FindingName = " ";


            return View(courseMainView);
        }
    }
}

