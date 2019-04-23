using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.WebApp.Models;

namespace Transfer8Pro.WebApp.Controllers
{
    public class ComponentController : BaseController
    {        
        public ComponentController()
        {

        }
        public ActionResult LeftMenu()
        {
            List<SystemFunctionEntity> funList = base.CurrSystemUser.SystemFunList;
            return View(funList);
        }     

        public ActionResult TopMessageNav()
        {
            TopMessageNavEntity topMessageNav = new TopMessageNavEntity();
            topMessageNav.SystemUserEntity = base.CurrSystemUser.SystemUser;
            return View(topMessageNav);
        }
    }
}