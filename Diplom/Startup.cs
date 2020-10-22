using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Data;
using Diplom.Helpers;
using Diplom.Interfaces;
using Diplom.Models.BasketModels;
using Diplom.Models.ContactForm;
using Diplom.Models.DataModel.Blog;
using Diplom.Models.DataModel.Identity;
using Diplom.Models.DataModel.Shop.Penties;
using Diplom.Models.DataModel.ShopCart;
using Diplom.Repositories.Blog;
using Diplom.Repositories.Shop.Penties;
using Diplom.Repositories.ShopCart;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Blog;
using Diplom.Services.Models.Shop.Penties;
using Diplom.Services.Models.ShopCart;
using Diplom.Services.Services.Blog;
using Diplom.Services.Services.Shop.Penties;
using Diplom.Services.Services.ShopCart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diplom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // подключение к БД
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // реализация Identity
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = true;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = true; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = true; // требуются ли цифры
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Shop
            services.AddScoped<IService<PentieDTO>, PentieService>();
            services.AddScoped<IService<PBrandDTO>, PBrandService>();
            services.AddScoped<IService<PCategoryDTO>, PCategoryService>();
            services.AddScoped<IService<PColorDTO>, PColorService>();
            services.AddScoped<IService<PSizeDTO>, PSizeService>();
            services.AddScoped<IService<PentieColorDTO>, PentieColorService>();
            services.AddScoped<IService<PentieSizeDTO>, PentieSizeService>();

            services.AddTransient<IGenericRepository<Pentie>, PentieRepository>();
            services.AddTransient<IGenericRepository<PBrand>, PBrandRepository>();
            services.AddTransient<IGenericRepository<PColor>, PColorRepository>();
            services.AddTransient<IGenericRepository<PCategory>, PCategoryRepository>();
            services.AddTransient<IGenericRepository<PSize>, PSizeRepository>();
            services.AddTransient<IGenericRepository<PentieColor>, PentieColorRepository>();
            services.AddTransient<IGenericRepository<PentieSize>, PentieSizeRepository>();

            //ShopCart
            services.AddScoped<IService<AddressDTO>, AddressService>();
            services.AddScoped<IService<CountryDTO>, CountryService>();
            services.AddScoped<IService<OrderDTO>, OrderService>();

            services.AddTransient<IGenericRepository<Address>, AddressRepository>();
            services.AddTransient<IGenericRepository<Country>, CountryRepository>();
            services.AddTransient<IGenericRepository<Order>, OrderRepository>();

            //Blog
            services.AddScoped<IService<PostDTO>, PostService>();
            services.AddScoped<IService<TagDTO>, TagService>();
            services.AddScoped<IService<PostTagDTO>, PostTagService>();

            services.AddTransient<IGenericRepository<Post>, PostRepository>();
            services.AddTransient<IGenericRepository<Tag>, TagRepository>();
            services.AddTransient<IGenericRepository<PostTag>, PostTagRepository>();

            services.AddTransient<IEmailSender, EmailSender>();

            // Контактная форма
            EmailServerConfiguration config = new EmailServerConfiguration
            {
                SmtpPassword = "Qqazqaz21",
                SmtpServer = "smtp.gmail.com",
                SmtpUsername = "nics.company.email@gmail.com"
            };

            EmailAddress FromEmailAddress = new EmailAddress
            {
                Address = "nics.company.email@gmail.com",
                Name = "Company Email"
            };

            services.AddSingleton<EmailServerConfiguration>(config);
            services.AddTransient<IEmailServicee, MailKitEmailService>();
            services.AddSingleton<EmailAddress>(FromEmailAddress);

            services.AddControllersWithViews();

            // basket
            services.AddScoped(sp => SessionBasket.GetBasket(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMemoryCache();
            services.AddSession();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                // маршрут для области admin
                endpoints.MapAreaControllerRoute(
                    name: "admin_area",
                    areaName: "admin",
                    pattern: "admin/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
