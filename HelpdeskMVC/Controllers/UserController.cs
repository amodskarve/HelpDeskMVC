using HelpdeskMVC.Component;
using HelpdeskMVC.Models;
using HelpdeskMVC.Models.Home;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Controllers
{
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));           
        readonly UserComponent userComplaintComponent;
        readonly HelpdeskComponent helpdeskComponent;

        public UserController( UserComponent usrComplaintComponent, HelpdeskComponent helpdeskComponent)
        {            
            this.userComplaintComponent = usrComplaintComponent;
            this.helpdeskComponent = helpdeskComponent;
        }
        UserComplaintModel userComplaintModel = new UserComplaintModel();

        [HttpGet]
        public ActionResult AddUserComplaint()
        {
            userComplaintModel.ApplicationName = helpdeskComponent.GetApplicationName();
            return View(userComplaintModel);
        }

        /// <summary>
        /// Save The user Cmplaint
        /// </summary>
        /// <returns></returns>
       [HttpPost]
        public ActionResult AddUserComplaint(UserComplaintModel userComplaint)
        {
            if (ModelState.IsValid)
            {
                userComplaintComponent.SaveUserComplaint(userComplaint);
            }
           return RedirectToAction("GetUserComplaint");
        }

        [HttpGet]
        public ActionResult GetUserComplaint()
        {
            return View();
        }

        [HttpGet]
        public JsonResult abc( string  emailid,string status)
        {
            //emailId = Request.QueryString[""]
            log.Info("Email ID received in GetUserComplaintDetails " + emailid);
            log.Info("Status received in GetUserComplaintDetails" + status);
            return Json(userComplaintComponent.GetUserComplaintDetails(emailid, status), JsonRequestBehavior.AllowGet);
        }
    }
}