using Microsoft.AspNetCore.Rewrite;
using MiddleManServices.ApiServices.QuickBase;
using MiddleManServices.ApiServices.QuickBase.Interfaces;

namespace MiddleMan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient(typeof(IQuickBaseService), typeof(QuickBaseService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Environment.SetEnvironmentVariable("UserToken", builder.Configuration.GetSection("MyVariables")["UserToken"]);

            RewriteOptions options = new RewriteOptions()
                .AddRedirect("^middleman\\.azurewebsites\\.net$", "https://sinoxpert.eu", 301);

            app.UseRewriter(options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}