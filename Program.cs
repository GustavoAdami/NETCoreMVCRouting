using Microsoft.AspNetCore.Builder;

namespace Routing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add MVC services (controllers + views)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error"); // For conventional routing, this would be /Home/Error
                //app.UseExceptionHandler("/attr/error-page"); // For attribute routing, this would be /attribute/error
            }

            // Handles HTTP error status codes (like 404, 500, etc.) by re-executing the request pipeline for a specified path—in this case, /error.
            app.UseStatusCodePagesWithReExecute("/Error"); // For conventional routing.
            //app.UseStatusCodePagesWithReExecute("/attr/error-page"); // For attribute routing

            // 2. (Optional) Use static files (e.g., CSS/JS under wwwroot)
            app.UseStaticFiles();

            // 3. Enable routing & endpoint mapping
            app.UseRouting();

            // 4. (Optional) If you need auth, add app.UseAuthentication(); app.UseAuthorization();
            app.UseAuthorization();

            // 5. Map default controller routes - Conventional routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // OR
            // The line below is the equivalent to the above MapControllerRoute call - hover over it to see the official documentation
            //app.MapDefaultControllerRoute();

            // AND/OR - Can use both conventional and attribute routing in the same application.
            // Enable Attribute routing - if you want only attribute routing, comment out the MapControllerRoute
            //app.MapControllers();
            /*
             * Must use attribute routing in controllers and actions.
             * This means you need to decorate your controller classes and/or action methods with [Route], [HttpGet], [HttpPost],
             * or similar attributes to define how URLs map to your actions.
             * It is a good idea to redirect root URL ("/" to a custom controller/action (e.g. /Index)
             */

            // Redirect root URL to a specific action
            //app.MapGet("/", context =>
            //{
            //    context.Response.Redirect("/index");
            //    return Task.CompletedTask;
            //});

            // AND/OR - Use Endpoint Routing
            // Endpoints are the final destinations for HTTP requests. UseEndpoints is mostly for legacy or advanced scenarios.
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Maps a custom GET endpoint for health checks
                endpoints.MapGet("/health", async context =>
                {
                    await context.Response.WriteAsync("Healthy!");
                });
            });

            // Middleware branching method, if it matches the path, it runs the delegate you provide (HandleMapTest1).
            app.Map("/map1", HandleMapTest1);

            // 6. Run the application
            app.Run();
        }

        // 
        private static void HandleMapTest1(IApplicationBuilder app)
        {
            /*
             * app.Run(...) defines a terminal middleware
             * it prevents further middleware from processing the request
             * It's called terminal middleware because it terminates the execution of the request pipeline
             */
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }
    }
}
