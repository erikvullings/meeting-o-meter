using Caliburn.Micro;
using mom.Models;
using System;
using System.Globalization;
using System.Text;

namespace mom.DAL
{
    public class Repository : PropertyChangedBase
    {
        private static Repository instance;

        private BindableCollection<Tariff> tariffs = new BindableCollection<Tariff>();

        private Repository()
        {
            ConvertStringToTariffs();
        }

        public static Repository Instance { get { return instance ?? (instance = new Repository()); } }

        public BindableCollection<Tariff> Tariffs
        {
            get { return tariffs; }
            set
            {
                if (Equals(value, tariffs)) return;
                tariffs = value;
                ConvertTariffsToString();
                NotifyOfPropertyChange(() => Tariffs);
            }
        }

        public void DeleteTariff(Tariff tariff)
        {
            Tariffs.Remove(tariff);
            ConvertTariffsToString();
        }

        /// <summary>
        /// Insert a tariff
        /// </summary>
        /// <param name="index">Index to insert</param>
        /// <param name="tariff">New tariff</param>
        public void InsertTariff(int index, Tariff tariff)
        {
            if (index >= tariffs.Count || index < 0)
                Tariffs.Add(tariff);
            else
                Tariffs.Insert(index, tariff);
            ConvertTariffsToString();
        }

        private void ConvertStringToTariffs()
        {
            var tariffGroups = Properties.Settings.Default.TariffGroups;
            if (string.IsNullOrEmpty(tariffGroups)) return;
            Tariffs.Clear();
            var tariffAmounts = tariffGroups.Split(new[] { ';' }, StringSplitOptions.None);
            for (var i = 0; i < tariffAmounts.Length; )
            {
                if (i >= tariffAmounts.Length - 1) break;
                var title = tariffAmounts[i++];
                var amountString = tariffAmounts[i++];
                double amount;
                if (double.TryParse(amountString, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount))
                    tariffs.Add(new Tariff { Title = title, Amount = amount });
            }
        }

        private void ConvertTariffsToString()
        {
            var sb = new StringBuilder();
            foreach (var tariff in tariffs)
            {
                sb.Append(string.Format(CultureInfo.InvariantCulture, "{0};{1:C};", tariff.Title, tariff.Amount));
            }
            Properties.Settings.Default.TariffGroups = sb.ToString();
        }
    }
}