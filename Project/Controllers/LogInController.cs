using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User u)
        {
            if(ModelState.IsValid)
            {
                using (TeslaDocumentManagerEntities dc = new TeslaDocumentManagerEntities())
                {
                    try
                    {
                        var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                        if (v != null)
                        {
                            Session["LoggedUserFullName"] = v.Fullname.ToString();
                            Session["LoggedUserID"] = v.Username.ToString();
                            return RedirectToAction("AfterLogIn");
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogIn()
        {
            if (Session["LoggedUserID"] != null)
                return View();
            else return RedirectToAction("LogIn");
        }
    }


}