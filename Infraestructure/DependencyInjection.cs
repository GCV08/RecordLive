using Application.Interfaces;
using Domain.CustomEntities;
using FluentValidation.AspNetCore;
using Infraestructure.Filters;
using Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Validators
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();


            //Context and Repositories
            ///PRODUCTION
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services

            //Pagination
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));

            //Loop Reference Handler
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            //Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;

        }
    }
}
