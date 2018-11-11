using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moxy.Framework.Authentication;
using Moxy.Framework.Permissions;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api.Controllers.V1.Admin
{

    [Area("admin")]
    [ApiVersion("1.0")]
    [Route("[area]/api/v{api-version:apiVersion}/[controller]")]
    [AdminAuth]
    public class BaseAdminController : ControllerBase
    {
    }
}