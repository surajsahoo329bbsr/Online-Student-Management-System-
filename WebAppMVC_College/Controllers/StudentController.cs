using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Session["AdminModel"] = admin;
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

        public ActionResult Update(int studentId, int adminId)
        {
            Session["AdminId"] = adminId;
            Student student = new CollegeContext().Students.Find(studentId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            int studentId = int.Parse(form["StudentId"].ToString());
            string studentName = form["StudentName"].ToString();
            string studentGender = form["StudentGender"].ToString();
            long studentPhoneNo = long.Parse(form["StudentPhoneNo"].ToString());
            int adminId = int.Parse(Session["AdminId"].ToString());

            SqlConnection sqlConn = new SqlConnection(@"Data Source=JOHNDOE-PC\SQLEXPRESS;Initial Catalog=CollegeDB;Integrated Security=True");
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand("update Students set StudentName = '"+ studentName + "', StudentPhoneNo = "+ studentPhoneNo + ", StudentGender = '" + studentGender +"', AdminId = "+ adminId +" where StudentId = " + studentId + ";", sqlConn);
            sqlCmd.ExecuteNonQuery();
            SqlCommand sqlCmdFetch = new SqlCommand("select * from Students where StudentId = " + studentId + ";", sqlConn);
            SqlDataReader sdr = sqlCmdFetch.ExecuteReader();
            Student student = null;

            while (sdr.Read())
            {
                student = new Student
                {
                    StudentId = int.Parse(sdr[0].ToString()),
                    StudentName = sdr[1].ToString(),
                    StudentPhoneNo = long.Parse(sdr[2].ToString()),
                    StudentGender = sdr[3].ToString(),
                    AdminId = int.Parse(sdr[4].ToString())
                };
                ViewBag.Status = "updated";
            }
            
            sqlConn.Close();
            return View(student);
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