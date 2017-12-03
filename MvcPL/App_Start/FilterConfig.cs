using System.Web.Mvc;
using MvcPL.Filtres;

namespace MvcPL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionLoggerAttribute());
        }
    }
}