// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// New migration.
    /// </summary>
    public partial class March2620232 : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.AddColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_StrMasterPassword", c => c.String());
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.DropColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_StrMasterPassword");
        }
    }
}
