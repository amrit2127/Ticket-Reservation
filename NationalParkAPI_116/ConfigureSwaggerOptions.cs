using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NationalParkAPI_116
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authentication header using the Bearer Scheme\r\n\r\n." +
                "Enter 'Bearer' [space] and then your token in the text input below\r\n\r\n" +
                "Example: Bearer 12345abcder\r\n" +
                "Name: Authentication \r\n" +
                "In: header",
                Name = "Authentication",
                In = ParameterLocation.Header,
            });
                //****

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                   {
                       new OpenApiSecurityScheme {
                           Reference=new OpenApiReference
                           {
                               Type=ReferenceType.SecurityScheme,
                               Id="Bearer"
                           },
                           Scheme="oauth2",
                           Name="Bearer",
                           In=ParameterLocation.Header
                       },
                       new List<String>()
                   }
            });
        }
    }
}
