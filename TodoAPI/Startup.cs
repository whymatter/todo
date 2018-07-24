using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using TodoAPI.Requirements;
using TodoAPI.Stores;

namespace TodoAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"));
            });

            services.AddMvc();


            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });

            services.AddSingleton<IUserStore, UserStore>();
            services.AddSingleton<ITodoStore, TodoStore>();

            services.AddSwaggerGen(o => {
                o.DescribeAllParametersInCamelCase();
                o.AddSecurityDefinition("JWT", new ApiKeyScheme {In = "header", Name = "Authorization", Type = "apiKey"});
                o.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>() {{"JWT", new string[0]}});
                o.SwaggerDoc("v1", new Info {Title = "Todo", Version = "v1", Description = "API of the worlds most advanced Todo app."});
            });

            services.AddAuthorization(o => {
                o.AddPolicy(Constants.ListOwnerPolicy, builder => {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(new TodoListOwnerRequirement());
                });

                o.AddPolicy(Constants.NonePolicy, builder => { builder.RequireAuthenticatedUser(); });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "todo.com",
                        ValidAudience = "todo.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySecureKey"))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(o => { o.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo V1"); });
            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
        }
    }
}