// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// This is a new migration.
    /// </summary>
    public partial class March262023 : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.AlterColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "Description", c => c.String());
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.AlterColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "Description", c => c.String(maxLength: 250));
        }
    }
}
