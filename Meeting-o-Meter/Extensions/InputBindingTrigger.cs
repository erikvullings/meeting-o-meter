﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace mom.Extensions
{
    public class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {
        public static readonly DependencyProperty InputBindingProperty = DependencyProperty.Register("InputBinding", typeof(InputBinding), typeof(InputBindingTrigger), new UIPropertyMetadata(null));

        public InputBinding InputBinding
        {
            get { return (InputBinding)GetValue(InputBindingProperty); }
            set { SetValue(InputBindingProperty, value); }
        }

        protected override void OnAttached()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = this;
                AssociatedObject.InputBindings.Add(InputBinding);
            }
            base.OnAttached();
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            // action is anyway blocked by Caliburn at the invoke level
            return true;
        }

        public void Execute(object parameter)
        {
            InvokeActions(parameter);
        }

        #endregion ICommand Members
    }
}