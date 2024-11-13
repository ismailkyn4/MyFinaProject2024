using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Ancryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            //AOP
            //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject
            //AOP
            services.AddControllers();
            //services.AddSingleton<IProductDal, EfProductDal>();
            //services.AddSingleton<IProductService,ProductManager>();

            services.AddCors(); //frontend tarafýndan projemize eriþebilmesi adýna. aþaðýda app.useCors demek zorundayýz.

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            //Bu yaptýðýmýz iþlem biz yarýn öbür gün farklý modüllerde oluþturursak injectionlar için onlarý da istediðimiz kadar oluþturup buraya ekle
            services.AddDependencyResolvers(new ICoreModule[]{
                new CoreModule()
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader()); //frontend tarafý kullansýn diye. Bana þu localhosttan get,post,put tarzýnda istek gelirse kullansýn diye.
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication(); //bunu da sonradan ekledik. Authentication yapmak için.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
