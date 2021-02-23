using FluentValidation.AspNetCore;
using ISUCorp.Test.Api.Configurations;
using ISUCorp.Test.Api.Data;
using ISUCorp.Test.Api.Domain;
using ISUCorp.Test.Api.Domain.ContactModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ISUCorp.Test.Api
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
            //Setting Automapper
            services.AddAutoMapper(typeof(Startup));

            //Setting Json format
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            //SQL DataBase Connection
            services.AddDbContext<DataContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            //Using Dependecy Inversion for Services and DataAccess
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddLocalization(options => options.ResourcesPath = "");
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            //Executing migration for first start APP
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Setting languages that App supports
            var supportedCultures = Configuration.GetSection("SupportedCultures").Get<SupportedCultures>();
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures.Cultures[0])
                .AddSupportedCultures(supportedCultures.Cultures)
                .AddSupportedUICultures(supportedCultures.Cultures);
            app.UseRequestLocalization(localizationOptions);

            //Allowing any request server
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            //Setting default endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello ISUCorp!");
                });
            });
        }
    }
}
