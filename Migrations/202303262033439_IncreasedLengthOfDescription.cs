// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Removed length limit from description column.
    /// </summary>
    public partial class IncreasedLengthOfDescription : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.AlterColumn("JTM_JTM2_PasswordsStencilJS_Items", "Description", c => c.String(nullable: false, maxLength: null));
        }

        /// <inheritdoc/>
        public override void Down()
        {
        }
    }
}
