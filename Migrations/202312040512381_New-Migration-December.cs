// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// test.
    /// </summary>
    public partial class NewMigrationDecember : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            // this.RenameTable(name: "dbo.JTM_JTM2_VueJSTest_Items", newName: "JTM_JTM2_PasswordsStencilJS_Items");
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.RenameTable(name: "dbo.JTM_JTM2_PasswordsStencilJS_Items", newName: "JTM_JTM2_VueJSTest_Items");
        }
    }
}
