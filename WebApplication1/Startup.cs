using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入
            services.AddControllersWithViews();//注册mvc服务
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //配置中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ////自定义中间件
            //app.Use(async (context,next) => {
            //    await context.Response.WriteAsync("lqz ");
            //    await next();//执行下一个中间件
            //});
            //开启路由中间件
            app.UseRouting();
            //app.UseMvc();//开启mvc中间件
            //开启终结点中间件，主要定义路由
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name: "defalut",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
