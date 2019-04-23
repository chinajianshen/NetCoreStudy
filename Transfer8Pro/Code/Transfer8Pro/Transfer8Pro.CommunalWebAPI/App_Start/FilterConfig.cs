using System.Web;
using System.Web.Mvc;

namespace Transfer8Pro.CommunalWebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
