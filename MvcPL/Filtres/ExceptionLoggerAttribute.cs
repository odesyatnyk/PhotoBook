using System;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Filtres
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public IExceptionDetailsService ExceptionDetailService
        {
            get { return (IExceptionDetailsService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IExceptionDetailsService)); }
        }
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetailsEntity exceptionDetail = new ExceptionDetailsEntity()
            {
                ExceptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            ExceptionDetailService.Create(exceptionDetail);
            filterContext.Result = filterContext.HttpContext.Response.StatusCode == 404 ? 
                new ViewResult {ViewName = "~/Views/Error/NotFound.cshtml"} : new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
            
            filterContext.ExceptionHandled = true;
        }
    }
}