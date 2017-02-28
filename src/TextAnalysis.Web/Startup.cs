using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TextAnalysis.Web.Domain.Data;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Repositories;
using TextAnalysis.Web.Domain.Providers;
using MongoDB.Driver;
using TextAnalysis.Web.Domain.Models;
using TextAnalysis.Web.Models;

namespace TextAnalysis.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();                
                
            Configuration = builder.Build();      

            this.RegisterMappings();      
            this.RegisterModelMappings();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            // Add framework services.
            services.AddMvc();

            // Enable CORS
            services.AddCors(options => {
                options
                    .AddPolicy("AllowAllOrigins", 
                            builder => builder.AllowAnyOrigin().AllowAnyMethod()
                        );
            });            
            
            string connectionString = this.Configuration.GetConnectionString("TextAnalysis");
            services.AddDbContext<ResourcesContext>(options => options.UseNpgsql(connectionString));
            
            services.AddSingleton(typeof(MongoClient), new MongoClient(connectionString));            
            services.AddScoped(typeof(IResourcesRepository<>), typeof(MongoRepository<>));            
                    
            
            // services.AddScoped<IUniqueIdentifierProvider, HashIdentityProvider>();
            services.AddScoped<IUniqueIdentifierProvider, Md5IdentifierProvider>();  

            services.AddScoped<IResourceContentProvider, HtmlAgilityPackContentProvider>();                      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });                                  
        }

        
        private void RegisterMappings()
        {
            MongoMappingsProvider.Register();
        }

        private void RegisterModelMappings()
        {
            AutoMapper.Mapper.Initialize(config=>{
                config.CreateMap<ResourceUrl, ArticleModel>();
                config.CreateMap<ResourceContent, ArticleContentModel>()
                    .ForMember(x => x.Content, opt => opt.MapFrom(d => d.Text));
            });
        }
    }
}
