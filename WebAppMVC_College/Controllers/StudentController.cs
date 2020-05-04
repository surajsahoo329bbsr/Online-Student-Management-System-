using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            //if (ModelState.IsValid)
            //{
                string imgPath = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(student.ImageFile.FileName));
                student.ImageFile.SaveAs(imgPath);                
                string query = "insert into Students values ('" + student.StudentName + "', '" + student.StudentPhoneNo + "', " + (int)student.StudentGender + ", ( select * from openrowset(bulk N'" + imgPath + "', single_blob) image ) , " + int.Parse(Session["AdminId"].ToString()) + ");";

                string connString = ConfigurationManager.ConnectionStrings["CollegeContext"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    ViewBag.Status = "added";
                    con.Close();
                }
            //}            

            return View();
            
        }

        public ActionResult Read()
        {
            List<Student> studentList = GetStudentDetails();
            return View(studentList);
        }

        private List<Student> GetStudentDetails()
        {
            string query = "select * from Students";
            List<Student> StudentList = new List<Student>();
            string constr = ConfigurationManager.ConnectionStrings["CollegeContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            StudentList.Add(new Student
                            {
                                StudentId = int.Parse(sdr[0].ToString()),
                                StudentName = sdr[1].ToString(),
                                StudentPhoneNo = sdr[2].ToString(),
                                StudentGender = (GenderType) Enum.Parse(typeof(GenderType), sdr[3].ToString()),
                                StudentPhoto = (byte[])sdr[4],
                                AdminId = int.Parse(sdr[5].ToString())
                            });
                        }
                    }
                    con.Close();
                }

                return StudentList;
            }
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

    }
}