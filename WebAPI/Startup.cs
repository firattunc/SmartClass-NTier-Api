using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

namespace WebAPI
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
            IdentityModelEventSource.ShowPII = true;

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    configurePolicy: builder => builder.WithOrigins("http://localhost:31541"));
            });


            //var tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidIssuer = tokenOptions.Issuer,
            //        ValidAudience = tokenOptions.Audience,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

            //    };
            //});

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});

            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins("http://localhost:31541").AllowAnyHeader().AllowAnyMethod());


            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();           




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
