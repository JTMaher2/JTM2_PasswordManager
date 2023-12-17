// MIT License
// Copyright JTMaher2

namespace JTMaher.Modules.JTMPasswordsStencilJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// InitialCreate.
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.CreateTable(
                "dbo.JTM_JTM2_PasswordsStencilJS_Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 250),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.DropTable("dbo.JTM_JTM2_PasswordsStencilJS_Items");
        }
    }
}
