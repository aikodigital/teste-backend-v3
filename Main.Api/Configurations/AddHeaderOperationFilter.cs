using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Main.Api.Configurations
{
    public class AddHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-ResponseFormat",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Sets the response format from the method",
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }

}
