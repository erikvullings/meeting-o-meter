using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using System;
using System.ComponentModel.Composition;

namespace mom.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SettingsViewModel : Screen
    {
        private Uri selectedSource = new Uri("/Views/MeterSettingsView.xaml", UriKind.Relative);

        public SettingsViewModel()
        {
            TabLinks = new LinkCollection(new[] {
                new Link {
                    DisplayName = "Meeting-O-Neter",
                    Source = new Uri("/Views/MeterSettingsView.xaml", UriKind.Relative)
                },
                new Link {
                    DisplayName = "appearance",
                    Source = new Uri("/Views/SettingsAppearanceView.xaml", UriKind.Relative)
                },
                new Link {
                    DisplayName = "about",
                    Source = new Uri("/Views/AboutView.xaml", UriKind.Relative)
                }
            });
        }

        public Uri SelectedSource
        {
            get { return selectedSource; }
            set
            {
                if (selectedSource == value) return;
                selectedSource = value;
                NotifyOfPropertyChange(() => SelectedSource);
            }
        }

        public LinkCollection TabLinks { get; set; }
    }
}