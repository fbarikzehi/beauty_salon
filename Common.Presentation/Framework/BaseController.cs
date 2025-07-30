using Beauty.Resource;
using Common.Presentation.Extensions;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Common.Presentation.Framework
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "POST" && !ModelState.IsValid)
            {
                if (context.HttpContext.Request.Headers.Values.Any(x => x == "XMLHttpRequest"))
                    context.Result = new JsonResult(new ModelStateResult
                    {
                        Message = ModelState.GetModelErrors().ToHtmlString(),
                        AlertType = ResponseAlertResource_en.Error,
                        IsSuccess = false
                    });
                else
                    context.Result = new BadRequestObjectResult(new ModelStateResult
                    {
                        Message = ModelState.GetModelErrors().ToHtmlString(),
                        AlertType = ResponseAlertResource_en.Error,
                        IsSuccess = false
                    });
            }

            //ViewBag.FullName = HttpContext.Session.GetString("fullname");

            ViewBag.FullName = ClaimManager.GetByType(ClaimOptions.UserFullNameClaimType, HttpContext.User.Claims);
            ViewBag.RoleName = ClaimManager.GetByType(ClaimOptions.RolePersianTitleClaimType, HttpContext.User.Claims);
            base.OnActionExecuting(context);
        }
    }
}
