// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// _20230312.
    /// </summary>
    public partial class Migration_20230312 : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.AddColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_strMasterPassword", c => c.String());
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.DropColumn("dbo.JTM_JTM2_PasswordsStencilJS_Items", "M_strMasterPassword");
        }
    }
}
