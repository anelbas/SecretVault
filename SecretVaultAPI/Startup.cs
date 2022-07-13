using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretVaultAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace SecretVaultAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecretVaultAPI", Version = "v1" });
            });

            services.AddDbContext<SecretVaultDBContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "PostsPolicy", builder =>
                {
                    //To be changed to deployed URL
                    builder.WithOrigins("http://localhost:3003").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = GetCognitoTokenValidationParams();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecretVaultAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private TokenValidationParameters GetCognitoTokenValidationParams()
        {
            string appClientId = this.Configuration.GetSection("UserPool")["AppClientId"];
            string jwtKeySetUrl = this.Configuration.GetSection("UserPool")["UserPoolAuth"];
            var cognitoIssuer = this.Configuration.GetSection("UserPool")["cognitoIssuer"];

            var cognitoAudience = appClientId;

            return new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    // get JsonWebKeySet from AWS 
                    var json = new WebClient().DownloadString(jwtKeySetUrl);

                    // serialize the result 
                    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;

                    // cast the result to be the type expected by IssuerSigningKeyResolver 
                    return (IEnumerable<SecurityKey>)keys;
                },
                ValidIssuer = cognitoIssuer,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = cognitoAudience
            };
        }
    }
}
