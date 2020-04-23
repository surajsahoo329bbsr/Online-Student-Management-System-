using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC_College.models;

namespace WebAppMVC_College.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult StartPage()
        {
            return View();
        }

        public ActionResult Login()
        {
            Admin admin = new Admin();
            if (admin.AdminEmail != null)
                return View("Login", admin);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {

            CollegeContext collegeContext = new CollegeContext();
            IList<Admin> adminList = (from a in collegeContext.Admins where a.AdminEmail == a.AdminEmail select a).ToList();
            long adminId = 0;
            foreach (Admin adm in adminList) adminId = adm.AdminId;
            Admin adminFound = collegeContext.Admins.Find(adminId);

            if (adminFound != null && adminFound.AdminPassword != admin.AdminPassword)
            {
                admin.AdminEmail = "BadCredentials";
                return View(admin);
            }
            else if (adminFound != null)
            {
                return RedirectToAction("Create", "Student", adminFound);
            }
            else
            {
                admin.AdminEmail = "NotRegistered";
                return View(admin);
            }
        }

        public ActionResult Registration()
        {
            Admin admin = new Admin();
            if (admin.AdminEmail != null)
                return View("Registration", admin);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Registration(Admin admin)
        {
            CollegeContext collegeContext = new CollegeContext();
            IList<Admin> CustList = (from a in collegeContext.Admins where a.AdminEmail == admin.AdminEmail select a).ToList();
            long adminId = 0;
            foreach (Admin adm in CustList) adminId = adm.AdminId;
            Admin adminFound = collegeContext.Admins.Find(adminId);

            if (admin.AdminEmail == null || admin.AdminFirstName == null || admin.AdminLastName == null || admin.AdminPhoneNo.ToString().Count() != 10 || admin.AdminPassword == null ||  admin.AdminPassword != admin.AdminConfirmPassword) 
            {
                admin.AdminEmail = "AdminInvalid";
                return View("Registration", admin);
            }
            else if (adminFound == null)
            {
                collegeContext.Admins.Add(admin);
                if (collegeContext.SaveChanges() > 0)
                {
                    admin.AdminEmail = "AdminRegistered";
                    return View("Registration", admin);
                }
            }
            else
            {
                admin.AdminEmail = "AdminExists";
                return View("Registration", admin);
            }

            return View(admin);
        }
    }
}