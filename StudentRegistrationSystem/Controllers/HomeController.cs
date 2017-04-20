using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.ViewModel;

namespace StudentRegistrationSystem.Controllers
{
    public class HomeController : Controller
    {
        StudentContext eb = new StudentContext();

        public List<Menu> menus = new List<Menu>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(T_Student model)
        {
            StudentContext eb = new StudentContext();

            if (eb.T_Student.Any(x => x.UserName == model.UserName))
            {
                TempData["Success"] = "UserName Already Available!";
                return View();
            }
            model.RoleId = 2;//bydefault selected as student
            model.IsAdmin = false;
            model.IsTeacher = false;
            eb.T_Student.Add(model);
            eb.SaveChanges();
            TempData["Success"] = "Added Successfully!";
            return View();
        }



        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(T_Student student)
        {
            using (StudentContext eb = new StudentContext())
            {
                var stu = eb.T_Student.Where(x => x.UserName == student.UserName && x.Password == student.Password).FirstOrDefault();
                if (stu != null)
                {
                    Session["UserName"] = student.UserName.ToString();
                    Session["Password"] = student.Password.ToString();
                    menus = GetMenusBasedOnUser();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    TempData["Success"] = "Login Failed";
                    return RedirectToAction("Login");
                }

            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserName"] != null)
            {
                //find menus from userid which is stored in session
                // if student then his menus will be returned
                //session has username
                List<Menu> menus = GetMenusBasedOnUser();
                ViewBag.Menus = menus;
                return View();
            }

            return RedirectToAction("Login");
        }


        public ActionResult Profile()
        {
            if (Session["UserName"] != null)
            {
                var userName = Session["UserName"].ToString();
                var studentProfile = eb.T_Student.Where(x => x.UserName
                  == userName).FirstOrDefault();
                ViewBag.Menus = GetMenusBasedOnUser();
                return View(studentProfile);
            }

            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            if (Session["UserName"] != null)
            {
                Session["UserName"] = null;
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult AddCource(Cource c, FormCollection formCollection)
        {
            var selectedRoleIdComaSeprated = formCollection["selectedRole"];
            var rolesId = selectedRoleIdComaSeprated.Split(',');
            var list = new List<Role>();
            foreach (var x1 in rolesId)
            {
                InsertCource(c.CourceName, c.CourceDescription, Convert.ToInt32(x1));
            }

            eb.SaveChanges();
            ViewBag.Roles = eb.Roles.ToList();
            ViewBag.Menus = GetMenusBasedOnUser();
            TempData["Success"] = "Added Successfullt";
            return View();
        }



        //GBP :: 041417 Display Cources based on role Type : teacher,student
        // this will be called when load the Form
        public ActionResult RegisterCource()
        {
            if (Session["UserName"] != null)
            {
                var userName = Session["UserName"].ToString();
                var RoleId = eb.T_Student.Where(x => x.UserName == userName).FirstOrDefault().RoleId;
                var cources = eb.Cources.ToList();
                if (RoleId != null && cources != null)
                {
                    //if teacher then display cource of teacher 
                    //if student then display cource of student
                    cources = cources.Where(x => x.RoleId == RoleId).ToList();
                }
                ViewBag.Menus = GetMenusBasedOnUser();
                return View(cources);
            }
            else
                return RedirectToAction("Login");
        }
        [HttpPost]
        //this will be called when submission of form
        public ActionResult RegisterCource(Cource c, FormCollection form)
        {
            var userName = Session["UserName"];
            if (userName == null)
                return RedirectToAction("LogIn");
            var StudentId = eb.T_Student.Where(x => x.UserName == userName.ToString()).FirstOrDefault().Id;

            var cources = eb.Cources.ToList();
            var roleId = eb.T_Student.Where(x => x.UserName == userName.ToString()).FirstOrDefault().RoleId;
            cources = cources.Where(x => x.RoleId == roleId).ToList();

            var selectedCourcesComaSeprated = form["chkCource"];
            if (!string.IsNullOrEmpty(selectedCourcesComaSeprated))
            {
                var courceId = selectedCourcesComaSeprated.Split(',');
                foreach (var x1 in courceId)
                {
                    SaveRegisteredCource(Convert.ToInt16(x1), Convert.ToInt16(StudentId));
                }
                TempData["Success"] = "Registered Successfullt";
                ViewBag.Menus = GetMenusBasedOnUser();
            }
            return View(cources);
        }


        #region admin Leval
        public ActionResult AddCource()
        {
            var roles = eb.Roles.ToList();
            ViewBag.Menus = GetMenusBasedOnUser();
            ViewBag.Roles = roles;
            return View();
        }
        public ActionResult ViewCource()
        {
            if (Session["UserName"] != null)
            {
                var userName = Session["UserName"].ToString();
                var RoleId = eb.T_Student.Where(x => x.UserName == userName).FirstOrDefault().RoleId;

                ViewBag.Menus = GetMenusBasedOnUser();
                var cources = eb.Cources.ToList();
                if (RoleId == 1)
                {
                    return View(cources);
                }
                else
                {
                    cources = cources.Where(x => x.RoleId == RoleId).ToList();
                    return View(cources);
                }
            }
            return RedirectToAction("Login");


        }
        //public ActionResult EditCource() {
        //    return RedirectToAction("ViewCource");
        //}
        
        public ActionResult EditCource(int id) {
            if (Session["UserName"] != null)
            {
                var userName = Session["UserName"].ToString();
                var RoleId = eb.T_Student.Where(x => x.UserName == userName).FirstOrDefault().RoleId;

                ViewBag.Menus = GetMenusBasedOnUser();
                var cources = eb.Cources.ToList();
                if (RoleId == 1)
                {
                    eb.Cources.Where(x => x.Id == id).FirstOrDefault();
                    eb.SaveChanges();
                    return RedirectToAction("ViewCource");
                }
                else
                {
                    return RedirectToAction("ViewCource");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        [ActionName("RegisteredCource")]
        public ActionResult ViewRegisteredCourcesAndStudent()
        {
            var adminUserName = Session["UserName"];
            if (adminUserName != null)
            {
                bool isAdmin = eb.T_Student.Where(x => x.UserName == adminUserName.ToString()).FirstOrDefault().IsAdmin;
                if (isAdmin)//TO DISPLAY REGISTERED STUDENT TO ADMIN ONLY
                {
                    adminUserName = adminUserName.ToString();
                    var CourceRegistration = GetCourceRegistrationData();
                    ViewBag.Menus = GetMenusBasedOnUser();
                    return View(CourceRegistration);
                }
            }
            return RedirectToAction("Login");
        }
        
        public ActionResult AddTeacher()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Menus = GetMenusBasedOnUser();
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(T_Student s)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            if (eb.T_Student.Any(x => x.UserName ==s.UserName ))
            {
                TempData["Success"] = "User Already Exists!";
            }
            else
            {
                s.IsTeacher = true;
                s.RoleId = 3;
                eb.T_Student.Add(s);
                eb.SaveChanges();
                TempData["Success"] = "Saved Successfully!";
                ViewBag.Menus = GetMenusBasedOnUser();
            }
            return View();
        }


        #endregion

            #region databaseMethods

        private List<Menu> GetMenusBasedOnUser()
        {
            if (menus == null || menus.Count<=0 && Session["UserName"]!=null)
            {
                var studentUserName = Session["UserName"].ToString();
                var roleId = eb.T_Student.Where(x => x.UserName == studentUserName).FirstOrDefault().RoleId;
                menus = eb.Menus.Where( (x => x.RoleId == roleId || x.RoleId == 4  && !x.Hidden) ).ToList();

            }
            return menus;
        }

        public int InsertCource(string CourceName, string CourceDescription, int RoleId)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudentContext"].ToString();
            var conn = new SqlConnection(connectionString);
            conn.Open();
            var query = "insert into T_Cource values('" + CourceName + "','" + CourceDescription + "','" + RoleId + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            var x = cmd.ExecuteNonQuery();
            return x;

        }
        public int SaveRegisteredCource(int CourceId, int StudentId)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudentContext"].ToString();
            var conn = new SqlConnection(connectionString);
            conn.Open();
            var query = "insert into T_CourceRegistration values('" + CourceId + "','" + StudentId + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            var x = cmd.ExecuteNonQuery();
            return x;
        }

        public List<CourceRegistrationViewModel> GetCourceRegistrationData()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudentContext"].ToString();
            var conn = new SqlConnection(connectionString);
            conn.Open();
            var query = "select s.UserName,c.CourceName from T_Student s ,  t_Cource c , T_CourceRegistration cs where s.Id=cs.StudentId AND c.Id=cs.CourceId";
            SqlCommand cmd = new SqlCommand(query, conn);
            var x = cmd.ExecuteReader();
            var listData = new List<CourceRegistrationViewModel>();
            while (x.Read())
            {
                var obj = new CourceRegistrationViewModel();
                obj.CourceName = x.GetString(0);
                obj.StudentName = x.GetString(1);
                listData.Add(obj);
            }
            return listData;

        }
        #endregion


    }
}
    
