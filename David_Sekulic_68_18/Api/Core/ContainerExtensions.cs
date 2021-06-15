using Application;
using Application.Commands;
using Application.Commands.Cart;
using Application.Commands.Category;
using Application.Commands.Product;
using Application.Email;
using Application.Queries;
using Application.Queries.Cart;
using Application.Queries.CategoryQ;
using DataAccess;
using Implementation.Commands;
using Implementation.Commands.CartC;
using Implementation.Commands.CategoryC;
using Implementation.Commands.ProductC;
using Implementation.Email;
using Implementation.Logging;
using Implementation.Queries;
using Implementation.Queries.CartQ;
using Implementation.Queries.CategoryQ;
using Implementation.Queries.OrderQ;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseExecutor>();
        
            services.AddTransient<IRegisterUser, RegisterUser>();         
            services.AddTransient<IGetLogs, GetLogs>();
            services.AddTransient<IUseCaseLogger, DatabaseLogger>();
            //products
            services.AddTransient<ICreateProduct, CreateProduct>();
            services.AddTransient<IGetProducts, GetProducts>();
            services.AddTransient<IDeleteProduct, DeleteProduct>();
            services.AddTransient<IGetProduct, GetProduct>();
            services.AddTransient<IUpdateProduct, UpdateProduct>();
            //orders
            services.AddTransient<ICreateOrder, CreateOrder>();
            services.AddTransient<IDeleteOrder, DeleteOrder>();
            services.AddTransient<IGetOrder, GetOrder>();
            services.AddTransient<IGetOrders, GetOrders>();
            //carts
            services.AddTransient<IGetCart, GetCart>();
            services.AddTransient<IGetCarts, GetCarts>();
            services.AddTransient<ICreateCart, CreateCart>();
            services.AddTransient<IDeleteCart, DeleteCart>();
            services.AddTransient<IUpdateCart, UpdateCart>();
            //categories
            services.AddTransient<IGetCategory, GetCategory>();
            services.AddTransient<IGetCategories, GetCategories>();
            services.AddTransient<ICreateCategory, CreateCategory>();
            services.AddTransient<IDeleteCategory, DeleteCategory>();
            services.AddTransient<IUpdateCategory, UpdateCategory>();
            //validatori--------
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<CreateCartValidator>();
            services.AddTransient<CreateCategoryValidator>();
        }

        public static void AddAppActor(this IServiceCollection services)
        {
            services.AddTransient<IActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                
                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new GuestActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });
        }

        public static void AddJwt(this IServiceCollection services,AppSettings appSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<Context>();

                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });
        }

        public static void AddSwagg(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projekat", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });
        }
    }
}
