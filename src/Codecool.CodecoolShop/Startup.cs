using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
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
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            Supplier Andi = new Supplier{Name = "Andi", Description = "Codecoolers who can crochet."};
            supplierDataStore.Add(Andi);
            Supplier NotAndi = new Supplier{Name = "Not Andi", Description = "IDK"};
            supplierDataStore.Add(NotAndi);
            ProductCategory Dolls = new ProductCategory {Name = "Dolls", Department = "Crocheted", Description = "Handmade crocheted dolls for children." };
            productCategoryDataStore.Add(Dolls);
            ProductCategory Clothes = new ProductCategory {Name = "Clothes", Department = "Crocheted", Description = "Handmade crocheted clothes for dolls." };
            productCategoryDataStore.Add(Clothes);
            ProductCategory Food = new ProductCategory { Name = "Food", Department = "Crocheted", Description = "Handmade crocheted food items for your dolls." };
            productCategoryDataStore.Add(Food);


            productDataStore.Add(new Product { Name = "Otter", DefaultPrice = 5000m, Currency = "HUF", Description = "Cute little otter, brown fur with white details. Approximately 15 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Koala", DefaultPrice = 5000m, Currency = "HUF", Description = "Gray koala with big feet. Approximately 12 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Seahorse", DefaultPrice = 5000m, Currency = "HUF", Description = "Sand colored seahorse. Approximately 14 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Mermaid", DefaultPrice = 10000m, Currency = "HUF", Description = "Colored-skinned mermaid, with curly hair and a little bow. Just in time for the new movie. Approximately 25 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Lamb", DefaultPrice = 5000m, Currency = "HUF", Description = "Adorable white lamb with a polaroid camera. Approximately 14 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Fox", DefaultPrice = 5000m, Currency = "HUF", Description = "Ornange fox with a light blue collar. Approximately 14 cm.", ProductCategory = Dolls, Supplier = Andi });
            productDataStore.Add(new Product { Name = "Stag", DefaultPrice = 5000m, Currency = "HUF", Description = "Oh my dear, what a stag! Handsome stag with little boots. Approximately 16 cm.", ProductCategory = Dolls, Supplier = Andi });
        }
    }
}
