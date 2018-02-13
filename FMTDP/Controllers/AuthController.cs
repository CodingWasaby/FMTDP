using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FMTDP.Controllers
{
    public class AuthController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values["controller"].ToString().ToUpper() == "HOME"
                    && context.RouteData.Values["action"].ToString().ToUpper() == "ERROR")
            {
                base.OnActionExecuting(context);
            }
            else
            {
                //var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
                //if (authService.IsLoggedIn(context.HttpContext))
                //{
                //    base.OnActionExecuting(context);
                //}
                //else
                //{
                //    context.Result = new RedirectResult("http://" + context.HttpContext.Request.Host.Value);
                //}
            }
        }

        protected List<Tuple<int, int>> GetPages(int dataCount, int pageNum = 20)
        {
            var pageCount = dataCount / pageNum + 1;
            var pages = new List<Tuple<int, int>>();
            for (var i = 0; i < pageCount; i++)
            {
                var tuple = new Tuple<int, int>(1 + i * pageNum, (i + 1) * pageNum);
                pages.Add(tuple);
            }
            return pages;
        }

        
    }
}