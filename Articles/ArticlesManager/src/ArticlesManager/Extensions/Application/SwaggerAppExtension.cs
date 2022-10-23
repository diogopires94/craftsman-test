namespace ArticlesManager.Extensions.Application;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using ArticlesManager.Middleware;
using Microsoft.Extensions.Hosting;

public static class SwaggerAppExtension
{
    public static void UseSwaggerExtension(this IApplicationBuilder app, WebApplicationBuilder builder)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            if (builder.Environment.IsDevelopment())
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            else
                config.SwaggerEndpoint("/articles/swagger/v1/swagger.json", ""); 
            config.DocExpansion(DocExpansion.None);
            config.OAuthClientId(Environment.GetEnvironmentVariable("AUTH_CLIENT_ID"));
            config.OAuthClientSecret(Environment.GetEnvironmentVariable("AUTH_CLIENT_SECRET"));
            config.OAuthUsePkce();
        });
    }
}