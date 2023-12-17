// MIT License
// Copyright JTMaher2

using JTMaher.Modules.JTMPasswordsStencilJS.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities
{
    /// <summary>
    /// Represents an item entity.
    /// </summary>
    [Table(Globals.ModulePrefix + "Items")]
    public class Item : BaseEntity
    {
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the item description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the item master password.
        /// </summary>
        public string M_StrMasterPassword { get; set; }
    }
}