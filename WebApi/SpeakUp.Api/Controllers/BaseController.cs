using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SpeakUp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected Guid? UserId
    {
        get
        {
            var val = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            return val is null ? null : new Guid(val);
        }
    }
}