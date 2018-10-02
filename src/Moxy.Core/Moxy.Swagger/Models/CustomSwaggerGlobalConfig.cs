using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Swagger.Models
{
    public static class CustomSwaggerGlobalConfig
    {
        public static List<Tag> CURRENT_SWAGGER_TAGS { get; set; }
    }
}
