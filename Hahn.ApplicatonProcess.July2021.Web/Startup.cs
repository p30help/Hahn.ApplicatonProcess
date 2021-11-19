using System.Reflection;
using FluentValidation;
using Hahn.ApplicationProcess.July2021.Data.Common;
using Hahn.ApplicationProcess.July2021.Data.Common.Events;
using Hahn.ApplicationProcess.July2021.Data.PipelineBehavior;
using Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Common;
using Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Users;
using Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Common;
using Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Users;
using Hahn.ApplicationProcess.July2021.Data.Users.Commands.AddUser;
using Hahn.ApplicationProcess.July2021.Data.Users.Services;
using Hahn.ApplicationProcess.July2021.Domain.Contracts;
using Hahn.ApplicationProcess.July2021.Domain.Contracts.Users;
using Hahn.ApplicationProcess.July2021.Domain.Users.DomainServices;
using Hahn.ApplicationProcess.July2021.Web.Middlewares;
using Hahn.ApplicationProcess.July2021.Web.Tools;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.July2021.Web
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
            services.AddDbContext<MainDbContext>(opt => opt.UseInMemoryDatabase("hahn"));
            services.AddDbContext<QueryDbContext>(opt => opt.UseInMemoryDatabase("hahn"));

            services.AddControllers();

            #region Swagger

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicationProcess.July2021.Web", Version = "v1" });

                // [SwaggerRequestExample] & [SwaggerResponseExample]
                // version < 3.0 like this: c.OperationFilter<ExamplesOperationFilter>(); 
                // version 3.0 like this: c.AddSwaggerExamples(services.BuildServiceProvider());
                // version > 4.0 like this:
                c.ExampleFilters();

            });

            #endregion

            #region MediatR

            services.AddMediatR(typeof(AddUserCommand));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #endregion

            #region Fluent Validation

            services.AddValidatorsFromAssembly(typeof(AddUserCommand).Assembly);

            #endregion


            services.AddSingleton<IMapper, Mapper>();
            services.AddSingleton<IIdGenerator, IdGenerator>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersReadonlyRepository, UsersReadonlyRepository>();
            services.AddScoped<IIsAssetValidated, IsAssetValidated>();

            services.AddScoped<IHahnUnitOfWork, HahnUnitOfWork>();

            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.July2021.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            // api error handler
            app.UseApiErrorHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
