using Caliburn.Micro;

namespace mom.Models
{
    public class Tariff : PropertyChangedBase
    {
        private double amount;
        private double participantsCount;
        private string title;

        /// <summary>
        /// Cost per hour.
        /// </summary>
        public double Amount
        {
            get { return amount; }
            set
            {
                if (value.Equals(amount)) return;
                amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }

        /// <summary>
        /// Costs per second for all participants.
        /// </summary>
        public double CostsPerSecond { get { return ParticipantsCount * Amount / 3600; } }

        /// <summary>
        /// Number of participants.
        /// </summary>
        public double ParticipantsCount
        {
            get { return participantsCount; }
            set
            {
                participantsCount = value;
                NotifyOfPropertyChange(() => ParticipantsCount);
            }
        }

        /// <summary>
        /// Name of the tariff.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (value == title) return;
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}