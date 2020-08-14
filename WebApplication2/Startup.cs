using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.DB;
using WebApplication1.Service;
using WebApplication1.ServiceImpl;

namespace WebApplication2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入
            services.AddControllersWithViews();//注册mvc服务


            //注册一个ICarService服务,将ICarService,CarServiceImpl映射关系注册到IOC容器中
            //services.AddSingleton<ICarService,CarServiceImpl>();//Singleton(在应用程序的整个生命周期内指挥创建一次)

            services.AddTransient<ICarService, CarServiceImpl>();//Transient(每次注入都会创建新的实例)

            //services.AddScoped<ICarService, CarServiceImpl>();//Scoped(一个请求内只创建一个实例)

            //注册ShopContext服务
            services.AddDbContext<ShopContext>(option =>
            {
                var connectionString = Configuration["Connection:ConnectionString"];
                option.UseMySql(connectionString);
            });

        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
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
