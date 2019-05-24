using Caliburn.Micro;
using mom.DAL;
using mom.Models;
using System.ComponentModel.Composition;

namespace mom.ViewModels
{
    [Export]
    public class MeterSettingsViewModel : Screen
    {
        private readonly Repository repository = Repository.Instance;
        private Tariff selectedTariff;

        public bool CanDeleteTariff { get { return selectedTariff != null; } }

        public char CurrencySymbol
        {
            get { return Properties.Settings.Default.CurrencySymbol; }
            set
            {
                if (char.Equals(Properties.Settings.Default.CurrencySymbol, value)) return;
                Properties.Settings.Default.CurrencySymbol = value;
                NotifyOfPropertyChange(() => CurrencySymbol);
            }
        }

        public Tariff SelectedTariff
        {
            get { return selectedTariff; }
            set
            {
                if (Equals(value, selectedTariff)) return;
                selectedTariff = value;
                NotifyOfPropertyChange(() => SelectedTariff);
            }
        }

        public BindableCollection<Tariff> Tariffs
        {
            get { return repository.Tariffs; }
        }

        public void AddTariff()
        {
            var tariff = new Tariff { Title = "Title", Amount = 100 };
            var index = selectedTariff == null
                ? Tariffs.Count
                : Tariffs.IndexOf(selectedTariff) + 1;
            repository.InsertTariff(index, tariff);
            SelectedTariff = tariff;
        }

        public void DeleteTariff()
        {
            repository.DeleteTariff(selectedTariff);
        }
    }
}