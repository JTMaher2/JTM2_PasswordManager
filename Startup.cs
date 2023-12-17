// MIT License
// Copyright JTMaher2

using DotNetNuke.DependencyInjection;
using DotNetNuke.Services.Localization;
using JTMaher.Modules.JTMPasswordsStencilJS.Data;
using JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities;
using JTMaher.Modules.JTMPasswordsStencilJS.Data.Repositories;
using JTMaher.Modules.JTMPasswordsStencilJS.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace JTMaher.Modules.JTMPasswordsStencilJS
{
    /// <summary>
    /// Implements the IDnnStartup interface to run at application start.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup : IDnnStartup
    {
        /// <summary>
        /// Registers the dependencies for injection.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ModuleDbContext, ModuleDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IItemService>(provider => new ItemService(provider.GetService<IRepository<Item>>()));
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
        }
    }
}