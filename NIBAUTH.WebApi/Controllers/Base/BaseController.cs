
using Abp.EntityFrameworkCore;
using Asp.Versioning;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace NIBAUTH.WebApi.Controllers.Base
{
    [Asp.Versioning.ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public const string LocalHeaderKey = "local";

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated ? Guid.Empty : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //protected async Task<string> GetLocal(INIBAUTHDbContext _context)
        //{
        //    string local = string.Empty;
        //    var defaultLang = await _context.Languages.FirstOrDefaultAsync(x => x.IsDefault);

        //    if (Request.Headers.TryGetValue(BaseController.LocalHeaderKey, out StringValues langCode))
        //    {
        //        var lang = await _context.Languages.FirstOrDefaultAsync(x => x.Code == langCode.ToString().ToUpper());
        //        if (lang == null)
        //        {
        //            local = defaultLang.Code;
        //        }
        //        else
        //        {
        //            local = lang.Code;
        //        }

        //    }
        //    else
        //    {
        //        local = defaultLang.Code;
        //    }
        //    return local;
        //}


    }
}


