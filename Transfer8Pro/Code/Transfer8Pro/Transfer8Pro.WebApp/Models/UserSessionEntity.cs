using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.WebApp.Models
{
    public class UserSessionEntity
    {
        public UserSessionEntity()
        {
            SystemFunList = new List<SystemFunctionEntity>();
            SystemUser = new SystemUserViewEntity();
        }

        public SystemUserViewEntity SystemUser { get; set; }

        public List<SystemFunctionEntity> SystemFunList
        {
            get; set;
        }
    }
}