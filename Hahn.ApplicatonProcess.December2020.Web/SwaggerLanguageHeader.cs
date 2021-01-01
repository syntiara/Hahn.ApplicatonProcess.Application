using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hahn.ApplicatonProcess.December2020.Web
{
   public class SwaggerLanguageHeader : IOperationFilter
   {
       readonly IServiceProvider serviceProvider;
       public SwaggerLanguageHeader(IServiceProvider serviceProvider)
       {
           this.serviceProvider = serviceProvider;
       }

       public void Apply(OpenApiOperation operation, OperationFilterContext context)
       {
           if (operation.Parameters == null)
               operation.Parameters = new List<OpenApiParameter>();

           operation.Parameters.Add(new OpenApiParameter
           {
               Name = "Accept-Language",
               In = ParameterLocation.Header,
               Schema = new OpenApiSchema() { 
                   Type = "String",
                    Enum = (IList<IOpenApiAny>)(serviceProvider.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                       .Value?.SupportedCultures?.Select(c => (IOpenApiAny)new OpenApiString(c.TwoLetterISOLanguageName)).ToList()                
                   },
               Description = "Supported languages",
               Required = false
           });
       }
   }
}