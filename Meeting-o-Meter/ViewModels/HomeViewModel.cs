using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace mom.ViewModels
{
    [Export]
    public class HomeViewModel : Screen
    {
        public string AppDescription
        {
            get
            {
                return "Welcome to the [b]Meeting-o-Meter[/b].\r\n";
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }
    }
}