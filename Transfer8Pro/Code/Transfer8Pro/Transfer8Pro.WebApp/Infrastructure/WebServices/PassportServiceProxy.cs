using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Transfer8Pro.WebApp.OBWebService;

namespace Transfer8Pro.WebApp.Infrastructure.WebServices
{
    public class PassportServiceProxy : Service
    {
        private static string SYSTEMFLAG = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SystemFlag"]) ? "OL" : ConfigurationManager.AppSettings["SystemFlag"];
        private static string SYSTEMCODE = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SystemCode"]) ? "OL" : ConfigurationManager.AppSettings["SystemCode"];
        public PassportServiceProxy()
        {
            OBSoapHeader headerinfo = new OBSoapHeader();
            headerinfo.Name = SYSTEMFLAG;
            headerinfo.Pass = SYSTEMCODE;
            this.OBSoapHeaderValue = headerinfo;
        }
    }
}