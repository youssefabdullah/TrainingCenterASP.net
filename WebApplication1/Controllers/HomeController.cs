using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;



namespace WebApplication1.Controllers
{
    
    public class HomeController : Controller
    {
        private Training_CenterEntities4 db = new Training_CenterEntities4();
        private Training_CenterEntities5 db2 = new Training_CenterEntities5();
        private Training_CenterEntities6 db3 = new Training_CenterEntities6();
        public string Email ="";
        public int Id ;
        public ActionResult Index()
        {
            return View(db.Course.ToList());
        }

        public ActionResult About(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        public ActionResult Profile()
        {
            return View(db3.UserCourse.ToList());
        }
        int i = 1;
        public ActionResult Book( int? courseId, string CourseDes, string CourseImg, int? CoursePrice,string CourseName)
        {
            UserCourse table = new UserCourse();
            
            table.UserId = Id;
            table.CourseID = (int)courseId ;
            table.CourseDes = CourseDes;
            table.CourseImg = CourseImg;
            table.CoursePrice= CoursePrice;
            table.CourseName= CourseName;
            if (ModelState.IsValid)
            {
                db3.UserCourse.Add(table);
                db3.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ActionName("Contact")]
        public ActionResult Post_Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        [ActionName("Contact")]
        public ActionResult Get_Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LogOut()
        {
            Email = "";
            Session["Email"] = Email;
            Session["togle"] = "f";
            return RedirectToAction("Index");
        }

        [ActionName("SignIn")]
        [HttpGet]
        public ActionResult SignIn_get()
        {
            ViewBag.n1 = "";
            ViewBag.n2 = "";
            ViewBag.r = "";
            return View();
        }

        [ActionName("SignIn")]
        [HttpPost]
        public ActionResult SignIn_post(string n1 , string n2)
        {
            string sum = n1 + n2;
            ViewBag.r = sum;
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Id,UserEmail,UserPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserEmail != "" && user.UserPassword !="")
                {
                    Email = user.UserEmail;
                    Id = user.Id;
                    if(Email == "youssef@gmail.com")
                    {
                        Session["admin"] = "t";
                    }
                    else
                    {
                        Session["admin"] = "f";
                    }
                    Session["Email"] = Email;
                    Session["togle"] = "t";
                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include ="UserEmail,UserPassword")]User user)
        {
            var rec = db.User.Where(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword).ToList().FirstOrDefault();
            
            if (rec != null)
            {
                Email = user.UserEmail;
                Id = user.Id;
                if (Email == "youssef@gmail.com")
                {
                    Session["admin"] = "t";
                }
                else
                {
                    Session["admin"] = "f";
                }

                ViewBag.error = "";
                
                Session["Email"] = Email;
                Session["togle"] = "t";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User";
                return View(user);
            }
            
        }
        
    }
}