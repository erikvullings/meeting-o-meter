using Caliburn.Micro;
using FirstFloor.ModernUI.Windows;
using mom.ViewModels;
using System.Windows.Controls;

namespace mom.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl, IContent
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private HomeViewModel ViewModel { get { return (HomeViewModel)DataContext; } }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //OnActivate();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            ScreenExtensions.TryActivate(ViewModel);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            ScreenExtensions.TryDeactivate(ViewModel, false);
        }
    }
}