// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// test.
    /// </summary>
    public partial class A20230325 : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.DropColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_strMasterPassword");
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.AddColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_strMasterPassword", c => c.String());
        }
    }
}
