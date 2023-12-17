// MIT License
// Copyright JTMaher2

using DotNetNuke.Entities.Portals;
using System.ComponentModel.DataAnnotations;

namespace JTMaher.Modules.JTMPasswordsStencilJS.DTO
{
    /// <summary>
    /// Data transfer object to create a new item.
    /// </summary>
    public class CreateItemDTO
    {
        /// <summary>
        /// Gets or sets the name for the item.
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the master password for the item.
        /// </summary>
        public string M_StrMasterPassword { get; set; }
    }
}
