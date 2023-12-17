// MIT License
// Copyright JTMaher2
using JTMaher.Modules.JTMPasswordsStencilJS.ViewModels;

namespace JTMaher.Modules.JTMPasswordsStencilJS.Services
{
    /// <summary>
    /// Provides strongly typed localization services for this module.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Gets viewmodel that strongly types all resource keys.
        /// </summary>
        LocalizationViewModel ViewModel { get; }
    }
}