using Caliburn.Micro;

namespace mom.Services
{
    public class EventAggregationProvider
    {
        private static EventAggregator instance;

        public static EventAggregator Instance
        {
            get { return instance ?? (instance = new EventAggregator()); }
        }
    }
}