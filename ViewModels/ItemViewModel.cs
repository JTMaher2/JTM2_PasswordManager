// MIT License
// Copyright JTMaher2

using JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace JTMaher.Modules.JTMPasswordsStencilJS.ViewModels
{
    /// <summary>
    /// Represents the basic information about an item.
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemViewModel"/> class.
        /// </summary>
        public ItemViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemViewModel"/> class.
        /// </summary>
        /// <param name="item">An <see cref="Item"/> entity.</param>
        public ItemViewModel(Item item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Description = item.Description;
            this.M_StrMasterPassword = item.M_StrMasterPassword;
        }

        /// <summary>
        /// Gets or sets the id of the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [Required]
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
