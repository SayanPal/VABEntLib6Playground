using System.Web;
using System.Web.Mvc;

namespace VABEntLib6Playground
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}