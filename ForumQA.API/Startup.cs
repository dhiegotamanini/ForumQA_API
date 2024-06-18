using ForumQA.Domain.Abstration;
using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Utils;
using ForumQA.Infrastructure;
using ForumQA.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ForumQA.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddMvcCore().AddJsonFormatters();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<ISqlDatabaseCommands, SqlDatabaseCommands>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IAnswerPostService, AnswerPostService>();
            services.AddScoped<IAnswerPostRepository, AnswerPostRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(setting =>
            {
                var config = setting.GetRequiredService<IConfiguration>();
                var appSetting = config.GetSection("AppSettings").Get<AppSettings>();
                return appSetting;
            });



            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var key = Encoding.ASCII.GetBytes("your_secret_key"); 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            
            /*
            services.AddCors(x => x.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
            services.AddMvcCore().AddJsonFormatters();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<ISqlDatabaseCommands, SqlDatabaseCommands>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IAnswerPostService, AnswerPostService>();
            services.AddScoped<IAnswerPostRepository, AnswerPostRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            */
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseMvc(route =>
            {
                route.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
