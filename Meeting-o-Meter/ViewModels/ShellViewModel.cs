using FirstFloor.ModernUI.Presentation;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace mom.ViewModels
{
    [InheritedExport]
    public interface IShellViewModel
    {
    }

    public class ShellViewModel : IShellViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ShellViewModel()
        {
            AppearanceManager.Current.AccentColor = Color.FromRgb(0xa2, 0x00, 0x25);
            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }
    }
}