using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC_College.models;

namespace WebAppMVC_College.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Create(Admin admin)
        {
            Student student = new Student();
            Session["AdminId"] = admin.AdminId;
            if (student.StudentName != null)
                return View("Create", student);
            return View(student);
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            CollegeContext collegeContext = new CollegeContext();
            student.AdminId = int.Parse(Session["AdminId"].ToString());
            collegeContext.Students.Add(student);
            if (collegeContext.SaveChanges() > 0)
            {
                ViewBag.Status = "added";
                return View(student);
            }
            return View(student);
        }

        public ActionResult Read()
        {
            CollegeContext collegeContext = new CollegeContext();
            List<Student> studentList = collegeContext.Students.ToList();
            return View(studentList);
        }

        public ActionResult UpdateStudentById()
        {
            return View();
        }

        public ActionResult DeleteStudentById()
        {
            return View();
        }

    }
}