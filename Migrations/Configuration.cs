// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<JTMaher.Modules.JTMPasswordsStencilJS.Data.ModuleDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "JTMaher.Modules.JTMPasswordsStencilJS.Data.ModuleDbContext";
        }

        /// <inheritdoc/>
        protected override void Seed(JTMaher.Modules.JTMPasswordsStencilJS.Data.ModuleDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
