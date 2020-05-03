using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC_College.models;

namespace WebAppMVC_College.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Create(Admin admin)
        {
            Student student = new Student();
            Session["AdminModel"] = admin;
            Session["AdminId"] = admin.AdminId;
            if (student.StudentName != null)
                return View("Create", student);
            return View(student);
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                CollegeContext collegeContext = new CollegeContext();
                //student.Photo = ImageToByteArray(Image.FromFile(student.ImageFile.FileName));

                student.AdminId = int.Parse(Session["AdminId"].ToString());
                collegeContext.Students.Add(student);
                if (collegeContext.SaveChanges() > 0)
                {
                    ViewBag.Status = "added";
                    return View(student);
                }

                //ModelState.Clear();
                return View(student);
            }

            return View();
            
        }

        public ActionResult Read()
        {
            CollegeContext collegeContext = new CollegeContext();
            List<Student> studentList = collegeContext.Students.ToList();
            return View(studentList);
        }

        public ActionResult Update(int studentId, int adminId)
        {
            Session["AdminId"] = adminId;
            Student student = new CollegeContext().Students.Find(studentId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(Student student, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                CollegeContext collegeContext = new CollegeContext();
                Student updateStudent = collegeContext.Students.Find(int.Parse(form["StudentId"]));
                updateStudent.StudentName = student.StudentName;
                updateStudent.StudentPhoneNo = student.StudentPhoneNo;
                updateStudent.StudentGender = student.StudentGender;
                if(collegeContext.SaveChanges() > 0)
                    ViewBag.Status = "updated";
                return View(student);
            }

            return View();
            
        }

        public ActionResult Delete(int studentId, int adminId)
        {
            Session["AdminId"] = adminId;
            Session["StudentId"] = studentId;
            Student student = new CollegeContext().Students.Find(studentId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            CollegeContext collegeContext = new CollegeContext();
            int studentId = int.Parse(Session["StudentId"].ToString());
            Student studentFound = collegeContext.Students.Find(studentId);
            collegeContext.Students.Remove(studentFound);
            collegeContext.SaveChanges();
            ViewBag.Status = "deleted";
            return View();
        }

        public byte[] ImageToByteArray(Image imageInput)
        {
            MemoryStream memoryStream = new MemoryStream();
            imageInput.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArrayInput)
        {
            MemoryStream memoryStream = new MemoryStream(byteArrayInput);
            Image returnImage = Image.FromStream(memoryStream);
            return returnImage;
        }


    }
}