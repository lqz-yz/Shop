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
            //����ע��
            services.AddControllersWithViews();//ע��mvc����


            //ע��һ��ICarService����,��ICarService,CarServiceImplӳ���ϵע�ᵽIOC������
            //services.AddSingleton<ICarService,CarServiceImpl>();//Singleton(��Ӧ�ó������������������ָ�Ӵ���һ��)

            services.AddTransient<ICarService, CarServiceImpl>();//Transient(ÿ��ע�붼�ᴴ���µ�ʵ��)

            //services.AddScoped<ICarService, CarServiceImpl>();//Scoped(һ��������ֻ����һ��ʵ��)

            //ע��ShopContext����
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
            //�����м��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ////�Զ����м��
            //app.Use(async (context,next) => {
            //    await context.Response.WriteAsync("lqz ");
            //    await next();//ִ����һ���м��
            //});
            //����·���м��
            app.UseRouting();
            //app.UseMvc();//����mvc�м��
            //�����ս���м������Ҫ����·��
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
