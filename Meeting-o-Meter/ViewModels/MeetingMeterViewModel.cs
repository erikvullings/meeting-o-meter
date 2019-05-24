using Caliburn.Micro;
using mom.DAL;
using mom.Models;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;

namespace mom.ViewModels
{
    [Export]
    public class MeetingMeterViewModel : Screen
    {
        private readonly Repository repository = Repository.Instance;
        private readonly Timer timer = new Timer(1000) { AutoReset = true };
        private TimeSpan elapsedMeetingTime;
        private TimeSpan elapsedTime;
        private bool isPlaying;
        private DateTime lastElapsedTime;
        private double meetingCosts;
        private Tariff selectedTariff;

        private DateTime startTime;

        /// <summary>
        /// Creates an instance of the screen.
        /// </summary>
        public MeetingMeterViewModel()
        {
            timer.Elapsed += TimerOnElapsed;
        }

        public bool CanPauze { get { return IsPlaying; } }

        public bool CanPlay { get { return !IsPlaying; } }

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

        public TimeSpan ElapsedMeetingTime
        {
            get { return elapsedMeetingTime; }
            set
            {
                if (value.Equals(elapsedMeetingTime)) return;
                elapsedMeetingTime = value;
                NotifyOfPropertyChange(() => ElapsedMeetingTime);
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                if (value.Equals(isPlaying)) return;
                isPlaying = value;
                NotifyOfPropertyChange(() => IsPlaying);
                NotifyOfPropertyChange(() => CanPauze);
                NotifyOfPropertyChange(() => CanPlay);
            }
        }

        public double MeetingCosts
        {
            get { return meetingCosts; }
            set
            {
                if (value.Equals(meetingCosts)) return;
                meetingCosts = value;
                NotifyOfPropertyChange(() => MeetingCosts);
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

        public void Pause()
        {
            timer.Stop();
            elapsedTime += DateTime.Now - startTime;
            IsPlaying = false;
        }

        public void Play()
        {
            timer.Start();
            startTime = lastElapsedTime = DateTime.Now;
            IsPlaying = true;
        }

        public void Stop()
        {
            timer.Stop();
            ElapsedMeetingTime = new TimeSpan();
            elapsedTime = new TimeSpan();
            MeetingCosts = 0;
            IsPlaying = false;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var now = DateTime.Now;
            ElapsedMeetingTime = now - startTime;
            ElapsedMeetingTime += elapsedTime;
            MeetingCosts += Tariffs.Sum(tariff => tariff.CostsPerSecond) * (now - lastElapsedTime).TotalSeconds;
            lastElapsedTime = now;
        }
    }
}