using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashewCheese.API.Common
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public abstract class ApiControllerBase:ControllerBase
    {
    }
}
