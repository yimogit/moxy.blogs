using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api
{

    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class BaseSiteController : ControllerBase
    {

    }
}